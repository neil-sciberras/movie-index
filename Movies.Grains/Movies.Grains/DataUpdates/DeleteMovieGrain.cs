using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.DataQueries.Supervisors;
using Movies.Grains.Interfaces.DataUpdates;
using Movies.Grains.Interfaces.Exceptions;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.DataUpdates
{
	public class DeleteMovieGrain : Grain, IDeleteMovieGrain
	{
		private readonly IMovieSearchSupervisorGrain _movieSearchSupervisorGrain;
		private readonly IAllMoviesGrain _allMoviesGrain;

		public DeleteMovieGrain(IGrainFactory grainFactory)
		{
			_movieSearchSupervisorGrain = grainFactory.GetGrain<IMovieSearchSupervisorGrain>(GrainIds.MovieSearchSupervisorGrainId);
			_allMoviesGrain = grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
		}

		public async Task<Movie> DeleteMovieAsync(int id)
		{
			var searchGrain = await _movieSearchSupervisorGrain.GetSupervisedGrainAsync(id);
			var movie = await searchGrain.GetMovieAsync();
			
			if (movie == null)
			{
				throw new MovieNotFoundException($"Movie with Id '{id}' was not found in the database");
			}

			var movies = (await _allMoviesGrain.GetMoviesAsync()).ToList();

			movies = movies.Where(m => m.Id != movie.Id).ToList();
			await _allMoviesGrain.SetMovieListAsync(movies);

			return movie;
		}
	}
}
