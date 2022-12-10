using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IMovieGrainClient
	{
		Task<Movie> GetMovieAsync(int id);
		Task SetMovieAsync(Movie movie);
	}
}
