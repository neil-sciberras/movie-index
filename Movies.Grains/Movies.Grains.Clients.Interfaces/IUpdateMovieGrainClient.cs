using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IUpdateMovieGrainClient
	{
		public Task<Movie> UpdateMovieAsync(Movie movie);
	}
}
