using Movies.Grains.TopRatedMovies;
using Orleans;
using Orleans.Runtime;

namespace Movies.Grains.Supervisors
{
	/// <inheritdoc cref="SupervisorGrainBase{TSupervisedGrainInterface}"/>
	public class TopRatedMoviesSupervisorGrain : SupervisorGrainBase<TopRatedMoviesGrain>
	{
		public TopRatedMoviesSupervisorGrain(IPersistentState<SupervisorState> supervisorState, IGrainFactory grainFactory) : base(supervisorState, grainFactory)
		{
		}
	}
}
