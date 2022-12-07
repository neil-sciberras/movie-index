using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IMovieProxyGrainClient
	{
		Task<Movie> GetMovieAsync(int id);
	}
}
