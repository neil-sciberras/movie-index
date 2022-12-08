using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces.FilteredMovies;
using Movies.Grains.MovieList;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Grains.DataQueries.FilteredMovies
{
	/// <summary>
	/// <inheritdoc cref="FilteredMoviesGrainBase"/>
	/// <para />
	/// Keyed on the Id of the searched movie
	/// </summary>
	public class MovieSearchGrain : FilteredMoviesGrainBase, IMovieSearchGrain
	{
		public MovieSearchGrain(
			[PersistentState(stateName: "movieSearchState", storageName: GrainStorageNames.MemoryStorage)]
			IPersistentState<MovieListState> filteredMoviesState,
			IGrainFactory grainFactory) : base(filteredMoviesState, grainFactory)
		{
		}

		protected override IEnumerable<Movie> FilterMovies(IEnumerable<Movie> allMovies)
		{
			var movieId = this.GetPrimaryKeyLong();
			return new List<Movie> { allMovies.SingleOrDefault(m => m.Id == movieId) };
		}
	}
}
