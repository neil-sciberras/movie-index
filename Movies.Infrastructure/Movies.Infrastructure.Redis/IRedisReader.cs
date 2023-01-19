using Movies.Contracts.Models;
using Movies.Infrastructure.DataSource.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Redis
{
	public interface IRedisReader : IMoviesReader
	{
		Task<Movie> ReadMovieAsync(int id);
		IEnumerable<int> GetAllIds();
	}
}
