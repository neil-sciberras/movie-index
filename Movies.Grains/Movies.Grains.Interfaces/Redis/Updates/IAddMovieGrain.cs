using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.Redis.Updates
{
	public interface IAddMovieGrain : IGrainWithStringKey
	{
		Task<Movie> AddMovieAsync(NewMovie newMovie);
	}
}
