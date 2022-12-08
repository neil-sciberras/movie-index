using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces.FilteredMovies;
using Movies.Grains.MovieList;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Grains.FilteredMovies
{
	/// <summary>
	/// <inheritdoc cref="FilteredMoviesGrainBase"/>
	/// <para />
	/// Keyed on the number of top rated movies to return.
	/// </summary>
	public class TopRatedMoviesGrain : FilteredMoviesGrainBase, ITopRatedMoviesGrain
	{
		public TopRatedMoviesGrain(
			[PersistentState(stateName: "topRatedMoviesState", storageName: GrainStorageNames.MemoryStorage)]
			IPersistentState<MovieListState> topRatedMoviesState,
			IGrainFactory grainFactory) : base(topRatedMoviesState, grainFactory)
		{
		}

		protected override IEnumerable<Movie> FilterMovies(IEnumerable<Movie> allMovies)
		{
			var amount = (int)this.GetPrimaryKeyLong();

			return allMovies.OrderByDescending(m => m.Rate).ToList().Take(amount);
		}
	}
}
