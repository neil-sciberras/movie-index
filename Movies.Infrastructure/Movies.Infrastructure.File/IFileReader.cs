using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public interface IFileReader
	{
		Task<Movies> ReadMoviesAsync();
	}
}
