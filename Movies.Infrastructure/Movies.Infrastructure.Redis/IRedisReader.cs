using Movies.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Redis
{
	public interface IRedisReader
	{
		Task<IEnumerable<Movie>> ReadMoviesAsync();
		Task<Movie> ReadMovieAsync(int id);
	}
}
