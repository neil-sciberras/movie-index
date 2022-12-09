using Movies.Contracts.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.DataQueries
{
	/// <summary>
	/// A grain which returns a list of movies. Used for multiple use cases; e.g. top rated movies, all movies list, filtered movies
	/// </summary>
	public interface IMovieListGrain : IResettableGrain, IGrainWithIntegerKey
	{
		Task<IEnumerable<Movie>> GetMoviesAsync();
	}
}
