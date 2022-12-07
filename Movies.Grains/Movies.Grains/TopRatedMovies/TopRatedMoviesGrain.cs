using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.TopRatedMovies;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.TopRatedMovies
{
	//TODO: implement a check, where this grain checks whether the list of movies has changed from the last time it was called
	public class TopRatedMoviesGrain : Grain, ITopRatedMoviesGrain
	{
		private readonly IPersistentState<IEnumerable<Movie>> _topRatedMoviesState;
		private readonly IGrainFactory _grainFactory;

		public TopRatedMoviesGrain(
			[PersistentState(stateName: "topRatedMoviesState", storageName: GrainStorageNames.FileStorage)] IPersistentState<IEnumerable<Movie>> state,
			IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
			_topRatedMoviesState = state;
		}

		public async Task<IEnumerable<Movie>> GetMovies()
		{
			//TODO: Test that if the source database changes, and there is a new top list, then the correct list is returned
			if (_topRatedMoviesState.State == null)
			{
				await FetchAndSetStateAsync();
			}

			return _topRatedMoviesState.State;
		}

		public void ResetState()
		{
			_topRatedMoviesState.State = null;
		}

		public override async Task OnActivateAsync()
		{
			await base.OnActivateAsync();
			await FetchAndSetStateAsync();
		}

		public override async Task OnDeactivateAsync()
		{
			await base.OnDeactivateAsync();
			ResetState();
		}

		private async Task FetchAndSetStateAsync()
		{
			var amount = this.GetPrimaryKeyLong();
			var movieListGrain = _grainFactory.GetGrain<IMovieListGrain>(GrainIds.MovieListGrainId);
			var allMovies = await movieListGrain.GetAllMovies();

			_topRatedMoviesState.State = allMovies.OrderBy(m => m.Rate).Take((int)amount);
		}
	}
}
