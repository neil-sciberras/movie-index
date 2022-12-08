using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.Supervisors
{
	public interface ISupervisorGrain<TSupervisedGrainInterface> : IGrainWithStringKey
		where TSupervisedGrainInterface : IResettableGrain, IGrainWithIntegerKey
	{
		Task ResetAllAsync();
		Task<TSupervisedGrainInterface> GetSupervisedGrainAsync(int primaryKey);
	}
}
