using Movies.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IQueryClient
	{
		Task<IEnumerable<Movie>> GetAllMoviesAsync();
		Task<IEnumerable<Movie>> GetMoviesAsync(Genre genre);
		Task<Movie> GetMovieAsync(int id);
		Task<IEnumerable<Movie>> GetTopRatedMoviesAsync(int amountOfMovies);
	}
}
