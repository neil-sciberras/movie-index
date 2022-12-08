using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IMovieSearchGrainClient
	{
		Task<Movie> GetMovie(int id);
	}
}
