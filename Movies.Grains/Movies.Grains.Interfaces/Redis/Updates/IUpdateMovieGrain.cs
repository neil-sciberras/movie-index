using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.Redis.Updates
{
	public interface IUpdateMovieGrain : IGrainWithStringKey
	{
		Task<Movie> UpdateMovieAsync(Movie movie);
	}
}
