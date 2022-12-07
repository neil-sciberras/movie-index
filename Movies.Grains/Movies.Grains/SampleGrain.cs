using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using Orleans.Runtime;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class SampleGrain : Grain, ISampleGrain
	{
		private readonly IPersistentState<SampleDataModel> _state;

		public SampleGrain([PersistentState(stateName: "SampleDataModel", storageName: GrainStorageNames.MemoryStorage)]IPersistentState<SampleDataModel> state)
		{
			_state = state;
		}

		public Task<SampleDataModel> Get() => Task.FromResult(_state.State);

		public Task Set(string name)
		{
			_state.State = new SampleDataModel { Id = this.GetPrimaryKeyString(), Name = name };
			return Task.CompletedTask;
		}
	}
}