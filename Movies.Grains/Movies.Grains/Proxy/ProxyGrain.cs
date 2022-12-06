using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using Orleans.Runtime;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Proxy
{
	public class ProxyGrain : Grain, IProxyGrain
	{
		private readonly IPersistentState<ProxyState> _state;
		private readonly IGrainFactory _grainFactory;

		public ProxyGrain(
			[PersistentState(stateName: "proxyState", storageName: GrainStorageNames.FileStorage)] IPersistentState<ProxyState> state, 
			IGrainFactory grainFactory)
		{
			_state = state;
			_grainFactory = grainFactory;
		}

		public async Task<IMovieGrain> GetMovieGrainAsync(int id)
		{
			var grain = _grainFactory.GetGrain<IMovieGrain>(id);
			var grainState = await grain.GetAsync();

			if (grainState != null) return grain;

			var movie = _state.State.Movies.SingleOrDefault(movie => movie.Id == id);

			if (movie == null)
			{
				throw new MovieNotFoundException($"Movie with Id '{id}' was not in the database");
			}

			await SetGrainStateAsync(movie);

			return grain;
		}

		public override async Task OnActivateAsync()
		{
			await base.OnActivateAsync();

			foreach (var movie in _state.State.Movies)
			{
				await SetGrainStateAsync(movie);
			}
		}

		private async Task SetGrainStateAsync(Movie movie)
		{
			var movieGrain = _grainFactory.GetGrain<IMovieGrain>(movie.Id);
			await movieGrain.SetAsync(movie);
		}
	}
}
