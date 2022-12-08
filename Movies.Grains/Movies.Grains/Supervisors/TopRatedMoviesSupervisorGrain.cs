using Movies.Grains.Interfaces;
using Orleans;
using Orleans.Runtime;
namespace Movies.Grains.Supervisors
{
	/// <inheritdoc cref="SupervisorGrainBase{TSupervisedGrainInterface}"/>
	public class TopRatedMoviesSupervisorGrain : SupervisorGrainBase<ITopRatedMoviesGrain>
	{
		public TopRatedMoviesSupervisorGrain(IPersistentState<SupervisorState> supervisorState, IGrainFactory grainFactory) : base(supervisorState, grainFactory)
		{
		}
	}
}
