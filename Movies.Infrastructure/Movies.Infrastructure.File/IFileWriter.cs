using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public interface IFileWriter
	{
		Task WriteMoviesAsync(Movies movies);
	}
}
