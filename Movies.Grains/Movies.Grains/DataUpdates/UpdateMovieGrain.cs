using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.ContractExtensions;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.DataQueries.Supervisors;
using Movies.Grains.Interfaces.DataUpdates;
using Movies.Grains.Interfaces.Exceptions;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.DataUpdates
{
	public class UpdateMovieGrain : Grain, IUpdateMovieGrain
	{
		private readonly IMovieSearchSupervisorGrain _movieSearchSupervisorGrain;
		private readonly IAllMoviesGrain _allMoviesGrain;

		public UpdateMovieGrain(IGrainFactory grainFactory)
		{
			_movieSearchSupervisorGrain = grainFactory.GetGrain<IMovieSearchSupervisorGrain>(GrainIds.MovieSearchSupervisorGrainId);
			_allMoviesGrain = grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
		}

		public async Task<Movie> UpdateAsync(Movie movieUpdate)
		{
			var movieSearchGrain = await _movieSearchSupervisorGrain.GetSupervisedGrainAsync(movieUpdate.Id);

			var existingMovie = await movieSearchGrain.GetMovieAsync();

			if (existingMovie == null)
			{
				throw new MovieNotFoundException($"Movie with Id '{movieUpdate.Id}' was not found in the database");
			}

			var allMovies = (await _allMoviesGrain.GetMoviesAsync()).ToList();

			return await UpdateAndGetUpdatedMovieAsync(allMovies, existingMovie: existingMovie, changes: movieUpdate);
		}

		private async Task<Movie> UpdateAndGetUpdatedMovieAsync(ICollection<Movie> movieList, Movie existingMovie, Movie changes)
		{
			movieList = movieList.Where(m => m.Id != changes.Id).ToList();

			var movieToInsert = existingMovie.UpdateMovie(changes);

			movieList.Add(movieToInsert);

			await _allMoviesGrain.SetMovieListAsync(movieList);

			return movieToInsert;
		}
	}
}
