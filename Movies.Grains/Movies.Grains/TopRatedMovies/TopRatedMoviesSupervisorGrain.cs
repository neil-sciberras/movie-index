using Movies.Contracts.Grains;
using Movies.Grains.Interfaces.TopRatedMovies;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.TopRatedMovies
{
	/// <summary>
	/// This enables the resetting of all <see cref="ITopRatedMoviesGrain"/> states on the event of a file reload.
	/// </summary>
	public class TopRatedMoviesSupervisorGrain : Grain, ITopRatedMoviesSupervisorGrain
	{
		private readonly IPersistentState<TopRatedMoviesSupervisorState> _supervisorState;
		private readonly IGrainFactory _grainFactory;

		public TopRatedMoviesSupervisorGrain(
			[PersistentState(stateName: "supervisorState", storageName: GrainStorageNames.MemoryStorage)] IPersistentState<TopRatedMoviesSupervisorState> supervisorState, 
			IGrainFactory grainFactory)
		{
			_supervisorState = supervisorState;
			_grainFactory = grainFactory;
		}

		public async Task ResetAllAsync()
		{
			var resetTasks = _supervisorState.State.TopRatedMovieGrainPrimaryKeys
				.Select(pk => _grainFactory.GetGrain<ITopRatedMoviesGrain>(pk).ResetStateAsync()).ToList();

			await Task.WhenAll(resetTasks);
		}

		public Task<ITopRatedMoviesGrain> GetTopRatedMoviesGrainAsync(int amountOfMovies)
		{
			_supervisorState.State.TopRatedMovieGrainPrimaryKeys.Add(amountOfMovies);

			var topRatedMoviesGrain = _grainFactory.GetGrain<ITopRatedMoviesGrain>(amountOfMovies);

			return Task.FromResult(topRatedMoviesGrain);
		}

		public override Task OnActivateAsync()
		{
			base.OnActivateAsync();

			_supervisorState.State.TopRatedMovieGrainPrimaryKeys ??= new HashSet<int>();

			return Task.CompletedTask;
		}
	}
}
