using Movies.Contracts.Grains;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.Supervisors;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.DataQueries.Supervisors
{
	/// <summary>
	/// This enables the resetting of all the supervised grains' states on the event of a file reload.
	/// </summary>
	public abstract class SupervisorGrainBase<TSupervisedGrainInterface> : Grain,
		ISupervisorGrain<TSupervisedGrainInterface> where TSupervisedGrainInterface : IResettableGrain, IGrainWithIntegerKey
	{
		private readonly IPersistentState<SupervisorState> _supervisorState;
		private readonly IGrainFactory _grainFactory;

		protected SupervisorGrainBase(IPersistentState<SupervisorState> supervisorState, IGrainFactory grainFactory)
		{
			_supervisorState = supervisorState;
			_grainFactory = grainFactory;
		}

		public async Task ResetAllAsync()
		{
			var resetTasks = _supervisorState.State.SupervisedGrainsPrimaryKeys
				.Select(pk => _grainFactory.GetGrain<TSupervisedGrainInterface>(pk).ResetStateAsync()).ToList();

			await Task.WhenAll(resetTasks);
		}

		public Task<TSupervisedGrainInterface> GetSupervisedGrainAsync(int primaryKey)
		{
			_supervisorState.State.SupervisedGrainsPrimaryKeys.Add(primaryKey);

			var grain = _grainFactory.GetGrain<TSupervisedGrainInterface>(primaryKey);

			return Task.FromResult(grain);
		}

		public override async Task OnActivateAsync()
		{
			await base.OnActivateAsync();

			_supervisorState.State.SupervisedGrainsPrimaryKeys ??= new HashSet<int>();
		}
	}
}
