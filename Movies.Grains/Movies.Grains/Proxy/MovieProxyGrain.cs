using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.Exceptions;
using Orleans;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Proxy
{
	//TODO: refactor to reuse the filtered grain base
	/// <summary>
	/// Before it returns the <see cref="MovieGrain"/>, it checks if the movie grain has state, and if not searches for the movie from list of movies
	/// (provided by <see cref="IAllMoviesGrain"/> and sets it.
	/// (Inspired by: <see href='https://github.com/dotnet/orleans/issues/3462'></see>)
	/// </summary>
	public class MovieProxyGrain : Grain, IMovieProxyGrain
	{
		private readonly IGrainFactory _grainFactory;

		public MovieProxyGrain(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> GetMovieAsync(int id)
		{
			var movieGrain = _grainFactory.GetGrain<IMovieGrain>(id);
			var movie = await movieGrain.GetAsync();

			if (movie != null)
			{
				return await movieGrain.GetAsync();
			}

			var allMoviesGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
			var allMovies = await allMoviesGrain.GetMoviesAsync();

			movie = allMovies.SingleOrDefault(m => m.Id == id);

			if (movie == null)
			{
				throw new MovieNotFoundException($"Movie with Id '{id}' not found in the database");
			}

			await SetMovieGrainStateAsync(movie);

			return await movieGrain.GetAsync();
		}

		public override async Task OnActivateAsync()
		{
			await base.OnActivateAsync();

			var allMoviesGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
			var movieList = await allMoviesGrain.GetMoviesAsync();

			foreach (var movie in movieList)
			{
				await SetMovieGrainStateAsync(movie);
			}
		}

		private async Task SetMovieGrainStateAsync(Movie movie)
		{
			var movieGrain = _grainFactory.GetGrain<IMovieGrain>(movie.Id);
			await movieGrain.SetAsync(movie);
		}
	}
}
