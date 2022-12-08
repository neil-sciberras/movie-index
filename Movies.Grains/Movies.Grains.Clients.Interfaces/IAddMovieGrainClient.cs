using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IAddMovieGrainClient
	{
		Task<Movie> AddMovieAsync(NewMovie newMovie);
	}
}
