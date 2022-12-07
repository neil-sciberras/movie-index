using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains
{
	/// <summary>
	/// The grain responsible for maintaining the whole list of movies. It reads and writes to/from the file data source.
	/// </summary>
	public class MovieListGrain : Grain, IMovieListGrain
	{
		private readonly IPersistentState<IEnumerable<Movie>> _movieListState;

		public MovieListGrain([PersistentState(stateName: "movieListState", storageName: GrainStorageNames.FileStorage)] IPersistentState<IEnumerable<Movie>> movieListState)
		{
			_movieListState = movieListState;
		}

		public Task<IEnumerable<Movie>> GetAllMovies()
		{
			return Task.FromResult(_movieListState.State);
		}

		public Task SetMovieList(IEnumerable<Movie> movies)
		{
			_movieListState.State = movies;
			_movieListState.WriteStateAsync();

			return Task.CompletedTask;
		}
	}
}
