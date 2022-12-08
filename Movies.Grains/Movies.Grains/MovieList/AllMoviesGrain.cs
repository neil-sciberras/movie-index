using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.Supervisors;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.MovieList
{
	/// <summary>
	/// The grain responsible for maintaining the whole list of movies. It reads and writes to/from the file data source.
	/// </summary>
	public class AllMoviesGrain : Grain, IAllMoviesGrain
	{
		private readonly IPersistentState<MovieListState> _movieListState;
		private readonly IGrainFactory _grainFactory;

		public AllMoviesGrain(
			[PersistentState(stateName: "movieListState", storageName: GrainStorageNames.FileStorage)] IPersistentState<MovieListState> movieListState,
			IGrainFactory grainFactory)
		{
			_movieListState = movieListState;
			_grainFactory = grainFactory;
		}

		public Task<IEnumerable<Movie>> GetMoviesAsync()
		{
			return Task.FromResult(_movieListState.State.Movies);
		}

		public async Task SetMovieListAsync(IEnumerable<Movie> movies)
		{
			_movieListState.State.Movies = movies;

			var topRatedMoviesSupervisorGrain = _grainFactory.GetGrain<ISupervisorGrain<IMovieListGrain>>(GrainIds.TopRatedMoviesSupervisorGrainId);
			await topRatedMoviesSupervisorGrain.ResetAllAsync();

			await _movieListState.WriteStateAsync();
		}
	}
}
