using Movies.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IGenreFilterGrainClient
	{
		Task<IEnumerable<Movie>> GetMoviesAsync(int genre);
	}
}
