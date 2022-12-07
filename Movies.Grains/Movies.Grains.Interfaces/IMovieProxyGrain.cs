using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces
{
	public interface IMovieProxyGrain : IGrainWithStringKey
	{
		Task<Movie> GetMovieAsync(int id);
	}
}
