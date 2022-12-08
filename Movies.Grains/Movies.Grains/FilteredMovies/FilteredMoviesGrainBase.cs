using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.MovieList;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.FilteredMovies
{
	/// <summary>
	/// This type of grain is 'cached' on the state. If the state is not null, it will return the list of movies in the state.
	/// Else it will re-fetch the list from the <see cref="AllMoviesGrain"/>.
	/// This allows derived grains, to avoid always having to fetch the full list of movies and applying the filtering logic
	/// every time they're invoked.
	/// </summary>
	public abstract class FilteredMoviesGrainBase : Grain, IMovieListGrain
	{
		private readonly IPersistentState<MovieListState> _filteredMoviesState;
		private readonly IGrainFactory _grainFactory;

		protected abstract IEnumerable<Movie> FilterMovies(IEnumerable<Movie> allMovies);

		protected FilteredMoviesGrainBase(IPersistentState<MovieListState> filteredMoviesState, IGrainFactory grainFactory)
		{
			_filteredMoviesState = filteredMoviesState;
			_grainFactory = grainFactory;
		}

		public Task ResetStateAsync()
		{
			_filteredMoviesState.State = null;
			return Task.CompletedTask;
		}

		public async Task<IEnumerable<Movie>> GetMoviesAsync()
		{
			//TODO: Test that if the source database changes, and there is a new list that should be returned, then the correct list is returned
			if (_filteredMoviesState.State == null)
			{
				await FetchAndSetStateAsync();
			}

			return _filteredMoviesState.State!.Movies;
		}

		public override async Task OnActivateAsync()
		{
			await base.OnActivateAsync();
			await FetchAndSetStateAsync();
		}

		public override async Task OnDeactivateAsync()
		{
			await base.OnDeactivateAsync();
			await ResetStateAsync();
		}

		private async Task FetchAndSetStateAsync()
		{
			var allMoviesGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
			var allMovies = await allMoviesGrain.GetMoviesAsync();

			_filteredMoviesState.State.Movies = FilterMovies(allMovies);
		}
	}
}
