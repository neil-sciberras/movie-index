using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IDeleteMovieGrainClient
	{
		Task<Movie> DeleteMovieAsync(int id);
	}
}
