using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.TopRatedMovies
{
	public interface ITopRatedMoviesSupervisorGrain : IGrainWithStringKey
	{
		Task ResetAllAsync();
		Task<ITopRatedMoviesGrain> RegisterNewGrainAsync(int amountOfMovies);
	}
}
