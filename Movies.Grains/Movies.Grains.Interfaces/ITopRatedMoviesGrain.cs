using Movies.Contracts.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	/// <summary>
	/// Top-rated-movies grain, keyed on the number of movies to return.
	/// </summary>
	public interface ITopRatedMoviesGrain : IGrainWithIntegerKey, IResettableGrain
	{
		Task<IEnumerable<Movie>> GetMoviesAsync();
	}
}
