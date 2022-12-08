using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.MovieList;
using Orleans;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.GenreFilter
{
	/// <summary>
	/// Genre-filter grain, keyed on the genre
	/// </summary>
	public class GenreFilterGrain : Grain, IMoviesListGrain, IResettableGrain, IGrainWithIntegerKey
	{
		private readonly IPersistentState<MovieListState> _filteredMoviesState;
		private readonly IGrainFactory _grainFactory;

		public GenreFilterGrain(
			[PersistentState(stateName: "filteredMoviesState", storageName: GrainStorageNames.MemoryStorage)] IPersistentState<MovieListState> filteredMoviesState,
			IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
			_filteredMoviesState = filteredMoviesState;
		}

		//TODO: check whether state is available before going to 
		public async Task<IEnumerable<Movie>> GetMoviesAsync()
		{
			var primaryKey = this.GetPrimaryKeyLong();

			if (!Enum.IsDefined(typeof(Genre), primaryKey))
			{
				throw new InvalidGenreException($"{primaryKey} is not defined as a Genre");
			}

			var genre = (Genre)primaryKey;
			var moviesListGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
			var allMovies = await moviesListGrain.GetMoviesAsync();

			return allMovies?.Where(m => m.Genres != null && m.Genres.Contains(genre)) ?? new List<Movie>();
		}

		public Task ResetStateAsync()
		{

		}
	}
}
