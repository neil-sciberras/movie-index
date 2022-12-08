using Movies.Grains.Interfaces.FilteredMovies;

namespace Movies.Grains.Interfaces.Supervisors
{
	public interface IGenreFilterSupervisorGrain : ISupervisorGrain<IGenreFilterGrain>
	{
	}
}
