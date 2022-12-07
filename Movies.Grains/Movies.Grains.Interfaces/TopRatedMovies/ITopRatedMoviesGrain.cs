using Movies.Contracts.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.TopRatedMovies
{
	/// <summary>
	/// Top-rated-movies grain, keyed on the number of movies to return.
	/// </summary>
	public interface ITopRatedMoviesGrain : IGrainWithIntegerKey
	{
		Task<IEnumerable<Movie>> GetMoviesAsync();
		Task ResetStateAsync();
	}
}
