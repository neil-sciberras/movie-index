using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class MovieGrainClient : IMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;
		
		public MovieGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> GetMovieAsync(int id)
		{
			return await _grainFactory.GetGrain<IMovieGrain>(id).GetAsync();
		}
	}
}
