using Movies.Contracts.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.GenreFilter
{
	/// <summary>
	/// Genre-filter grain, keyed on the genre
	/// </summary>
	public interface IGenreFilterGrain : IGrainWithIntegerKey, IResettableGrain
	{
		Task<IEnumerable<Movie>> GetMoviesAsync();
	}
}
