using Movies.Grains.Interfaces.FilteredMovies;

namespace Movies.Grains.Interfaces.Supervisors
{
	public interface ITopRatedMoviesSupervisorGrain : ISupervisorGrain<ITopRatedMoviesGrain>
	{
	}
}
