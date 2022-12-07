using Movies.Contracts.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	public interface IMovieListGrain : IGrainWithStringKey
	{
		Task<IEnumerable<Movie>> GetAllMovies();
		Task SetMovieList(IEnumerable<Movie> movies);
	}
}
