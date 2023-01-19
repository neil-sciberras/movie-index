using Movies.Infrastructure.DataSource.Interfaces;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Redis
{
	public interface IRedisWriter : IMoviesWriter
	{
		Task<bool> WriteMovieAsync(string id, object movie);
		Task DeleteAsync(string id);
	}
}
