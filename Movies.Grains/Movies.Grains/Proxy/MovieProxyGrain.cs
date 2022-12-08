using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Proxy
{
	/// <summary>
	/// Before it returns the <see cref="MovieGrain"/>, it checks if the movie grain has state, and if not searches for the movie from list of movies
	/// (provided by <see cref="IAllMoviesGrain"/> and sets it.
	/// (Inspired by: <see href='https://github.com/dotnet/orleans/issues/3462'></see>)
	/// </summary>
	public class MovieProxyGrain : Grain, IMovieProxyGrain
	{
		private readonly IGrainFactory _grainFactory;
		private IAllMoviesGrain DataSourceGrain => _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);

		public MovieProxyGrain(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> GetMovieAsync(int id)
		{
			var movieGrain = _grainFactory.GetGrain<IMovieGrain>(id);
			var movieGrainState = await movieGrain.GetAsync();

			if (movieGrainState != null)
			{
				return await movieGrain.GetAsync();
			}

			var movieList = await DataSourceGrain.GetMoviesAsync();

			var movie = movieList.SingleOrDefault(movie => movie.Id == id);

			if (movie == null)
			{
				throw new MovieNotFoundException($"Movie with Id '{id}' not found in the database");
			}

			await SetMovieGrainStateAsync(movie);

			return await movieGrain.GetAsync();
		}

		public override async Task OnActivateAsync()
		{
			try
			{
				await base.OnActivateAsync();

				var allMoviesGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
				var movieList = await allMoviesGrain.GetMoviesAsync();

				foreach (var movie in movieList)
				{
					await SetMovieGrainStateAsync(movie);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
		}

		private async Task SetMovieGrainStateAsync(Movie movie)
		{
			var movieGrain = _grainFactory.GetGrain<IMovieGrain>(movie.Id);
			await movieGrain.SetAsync(movie);
		}
	}
}
