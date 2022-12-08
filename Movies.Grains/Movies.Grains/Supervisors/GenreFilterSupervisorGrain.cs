using Movies.Grains.Interfaces.GenreFilter;
using Orleans;
using Orleans.Runtime;
namespace Movies.Grains.Supervisors
{
	/// <inheritdoc cref="SupervisorGrainBase{TSupervisedGrainInterface}"/>
	public class GenreFilterSupervisorGrain : SupervisorGrainBase<IGenreFilterGrain>
	{
		public GenreFilterSupervisorGrain(IPersistentState<SupervisorState> supervisorState, IGrainFactory grainFactory) : base(supervisorState, grainFactory)
		{
		}
	}
}
