using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Infrastructure.Redis;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class AllMoviesGrain : Grain, IAllMoviesGrain
	{
		private readonly IPersistentState<MovieListState> _state;
		private readonly IGrainFactory _grainFactory;
		private readonly IRedisReader _redisReader;

		public AllMoviesGrain(
			[PersistentState(stateName: "moviesState", storageName: GrainStorageNames.MemoryStorage)] IPersistentState<MovieListState> state,
			IRedisReader redisReader,
			IGrainFactory grainFactory)
		{
			_state = state;
			_redisReader = redisReader;
			_grainFactory = grainFactory;
		}

		public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
		{
			if (_state.State == null)
			{
				await RefetchStateAsync();
			}

			return _state.State!.Movies.ToList();
		}

		public Task ResetAsync()
		{
			_state.State = null;
			return Task.CompletedTask;
		}

		public override async Task OnActivateAsync()
		{
			await base.OnActivateAsync();
			await RefetchStateAsync();
		}

		public override async Task OnDeactivateAsync()
		{
			await base.OnDeactivateAsync();

			_state.State = null;
		}

		private async Task RefetchStateAsync()
		{
			var ids = _redisReader.GetAllIds();
			var movieTasks = ids.Select(id => _grainFactory.GetGrain<IMovieGrain>(id).GetMovieAsync()).ToList();

			await Task.WhenAll(movieTasks);

			_state.State = new MovieListState { Movies = movieTasks.Select(t => t.Result) };
		}
	}
}
