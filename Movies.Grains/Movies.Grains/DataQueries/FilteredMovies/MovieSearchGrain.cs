using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces.DataQueries.FilteredMovies;
using Movies.Grains.MovieList;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
			var movie = allMovies.SingleOrDefault(m => m.Id == movieId);

			return movie != null 
				? new List<Movie> { movie }
				: new List<Movie>();
		}

		public async Task<Movie> GetMovieAsync()
		{
			var underlyingList = (await GetMoviesAsync()).ToList();

			return underlyingList.Any()
				? underlyingList.Single()
				: null;
		}
	}
}
