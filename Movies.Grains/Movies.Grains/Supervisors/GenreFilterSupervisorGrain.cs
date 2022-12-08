using Movies.Grains.GenreFilter;
using Orleans;
using Orleans.Runtime;
namespace Movies.Grains.Supervisors
{
	/// <inheritdoc cref="SupervisorGrainBase{TSupervisedGrainInterface}"/>
	public class GenreFilterSupervisorGrain : SupervisorGrainBase<GenreFilterGrain>
	{
		public GenreFilterSupervisorGrain(IPersistentState<SupervisorState> supervisorState, IGrainFactory grainFactory) : base(supervisorState, grainFactory)
		{
		}
	}
}
