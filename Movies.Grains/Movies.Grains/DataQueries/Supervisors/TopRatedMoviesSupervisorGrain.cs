using Movies.Contracts.Grains;
using Movies.Grains.Interfaces.FilteredMovies;
using Movies.Grains.Interfaces.Supervisors;
using Orleans;
using Orleans.Runtime;

namespace Movies.Grains.DataQueries.Supervisors
{
	/// <inheritdoc cref="SupervisorGrainBase{TSupervisedGrainInterface}"/>
	public class TopRatedMoviesSupervisorGrain : SupervisorGrainBase<ITopRatedMoviesGrain>, ITopRatedMoviesSupervisorGrain
	{
		public TopRatedMoviesSupervisorGrain(
			[PersistentState(stateName: "topRatedMoviesSupervisorState", storageName: GrainStorageNames.MemoryStorage)]
			IPersistentState<SupervisorState> supervisorState,
			IGrainFactory grainFactory) : base(supervisorState, grainFactory)
		{
		}
	}
}
