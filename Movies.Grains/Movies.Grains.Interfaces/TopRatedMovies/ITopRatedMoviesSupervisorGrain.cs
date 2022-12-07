using Orleans;

namespace Movies.Grains.Interfaces.TopRatedMovies
{
	public interface ITopRatedMoviesSupervisorGrain : IGrainWithStringKey
	{
		void ResetAll();
		void RegisterNewGrain(int amountOfMovies);
	}
}
