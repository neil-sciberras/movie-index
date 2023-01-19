using Movies.Contracts.Models;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Interfaces
{
	public interface IUpdateClient
	{
		Task<Movie> AddMovieAsync(NewMovie newMovie);
		Task<Movie> DeleteMovieAsync(int id);
		Task<Movie> UpdateMovieAsync(Movie movie);
	}
}
