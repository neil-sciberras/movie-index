using Movies.Grains.Interfaces.FilteredMovies;

namespace Movies.Grains.Interfaces.DataQueries.Supervisors
{
	public interface IGenreFilterSupervisorGrain : ISupervisorGrain<IGenreFilterGrain>
	{
	}
}
