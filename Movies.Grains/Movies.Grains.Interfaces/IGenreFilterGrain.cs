using Movies.Contracts.Models;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	public interface IGenreFilterGrain : IGrainWithStringKey
	{
		Task<IEnumerable<Movie>> GetMoviesAsync(Genre genre);
	}
}
