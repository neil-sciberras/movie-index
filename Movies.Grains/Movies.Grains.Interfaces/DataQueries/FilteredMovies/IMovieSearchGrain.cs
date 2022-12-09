using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.DataQueries.FilteredMovies
{
	public interface IMovieSearchGrain : IResettableGrain, IGrainWithIntegerKey
	{
		Task<Movie> GetMovieAsync();
	}
}
