using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.DataUpdates
{
	public interface IUpdateMovieGrain : IGrainWithStringKey
	{
		Task<Movie> UpdateAsync(Movie movieUpdate);
	}
}
