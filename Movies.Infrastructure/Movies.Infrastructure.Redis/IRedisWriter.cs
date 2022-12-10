using Movies.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Redis
{
	public interface IRedisWriter
	{
		Task WriteMoviesAsync(ICollection<Movie> movies);
		Task<bool> WriteMovieAsync(string id, object movie);
		Task DeleteAsync(string id);
	}
}
