using Orleans;

namespace Movies.Grains.Interfaces.TopRatedMovies
{
	public interface ITopRatedMoviesSupervisorGrain : IGrainWithStringKey
	{
		void ResetAll();
		ITopRatedMoviesGrain RegisterNewGrain(int amountOfMovies);
	}
}
