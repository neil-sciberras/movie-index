using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.Updates
{
	public interface IUpdateMovieGrain : IGrainWithStringKey
	{
		Task<Movie> UpdateMovieAsync(Movie movie);
	}
}
