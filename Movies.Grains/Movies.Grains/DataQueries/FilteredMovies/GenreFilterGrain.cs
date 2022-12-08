using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces.Exceptions;
using Movies.Grains.Interfaces.FilteredMovies;
using Movies.Grains.MovieList;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Grains.DataQueries.FilteredMovies
{
	/// <summary>
	/// <inheritdoc cref="FilteredMoviesGrainBase"/>
	/// <para />
	/// Keyed on the integer value of the Genre enum.
	/// </summary>
	public class GenreFilterGrain : FilteredMoviesGrainBase, IGenreFilterGrain
	{
		public GenreFilterGrain(
			[PersistentState(stateName: "genreFilteredMoviesState", storageName: GrainStorageNames.MemoryStorage)]
			IPersistentState<MovieListState> genreFilteredMoviesState,
			IGrainFactory grainFactory) : base(genreFilteredMoviesState, grainFactory)
		{
		}

		protected override IEnumerable<Movie> FilterMovies(IEnumerable<Movie> allMovies)
		{
			var genreIntValue = (int)this.GetPrimaryKeyLong();

			if (!Enum.IsDefined(typeof(Genre), genreIntValue))
			{
				throw new InvalidGenreException($"{genreIntValue} is not defined as a Genre");
			}

			var genre = (Genre)genreIntValue;

			return allMovies?.Where(m => m.Genres != null && m.Genres.Contains(genre)).ToList();
		}
	}
}
