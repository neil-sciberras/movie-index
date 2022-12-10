using Movies.Contracts.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.Redis
{
	public interface ITopRatedMoviesGrain : IGrainWithStringKey
	{
		Task<IEnumerable<Movie>> GetMoviesAsync(int amountOfMovies);
	}
}
