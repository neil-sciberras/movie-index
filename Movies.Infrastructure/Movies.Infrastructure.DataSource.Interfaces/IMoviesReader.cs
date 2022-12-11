using System.Threading.Tasks;

namespace Movies.Infrastructure.DataSource.Interfaces
{
	public interface IMoviesReader
	{
		Task<Contracts.Models.Movies> ReadMoviesAsync();
	}
}
