using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class MovieProxyGrainClient : IMovieProxyGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public MovieProxyGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> GetMovieAsync(int id)
		{
			return await _grainFactory.GetGrain<IMovieProxyGrain>(GrainIds.MovieProxyGrainId).GetMovieAsync(id);
		}
	}
}
