using Movies.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface ITopRatedMoviesGrainClient
	{
		Task<IEnumerable<Movie>> GetTopRatedMovies(int amount);
	}
}
