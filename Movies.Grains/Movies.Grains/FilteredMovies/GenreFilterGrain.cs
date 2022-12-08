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

namespace Movies.Grains.FilteredMovies
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
			var primaryKey = (int)this.GetPrimaryKeyLong();

			if (!Enum.IsDefined(typeof(Genre), primaryKey))
			{
				throw new InvalidGenreException($"{primaryKey} is not defined as a Genre");
			}

			var genre = (Genre)primaryKey;

			return allMovies?.Where(m => m.Genres != null && m.Genres.Contains(genre)).ToList() ?? new List<Movie>();
		}
	}
}
