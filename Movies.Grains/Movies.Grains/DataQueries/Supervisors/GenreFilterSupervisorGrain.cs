using Movies.Contracts.Grains;
using Movies.Grains.Interfaces.DataQueries.FilteredMovies;
using Movies.Grains.Interfaces.DataQueries.Supervisors;
using Orleans;
using Orleans.Runtime;
namespace Movies.Grains.DataQueries.Supervisors
{
	/// <inheritdoc cref="SupervisorGrainBase{TSupervisedGrainInterface}"/>
	public class GenreFilterSupervisorGrain : SupervisorGrainBase<IGenreFilterGrain>, IGenreFilterSupervisorGrain
	{
		public GenreFilterSupervisorGrain(
			[PersistentState(stateName: "genreFilterSupervisorState", storageName: GrainStorageNames.MemoryStorage)]
			IPersistentState<SupervisorState> supervisorState,
			IGrainFactory grainFactory) : base(supervisorState, grainFactory)
		{
		}
	}
}
