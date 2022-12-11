using System.Threading.Tasks;

namespace Movies.Infrastructure.DataSource.Interfaces
{
	public interface IMoviesWriter
	{
		Task WriteMoviesAsync(Contracts.Models.Movies movies);
	}
}
