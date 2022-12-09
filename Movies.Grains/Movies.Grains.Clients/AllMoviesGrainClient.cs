using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class AllMoviesGrainClient : IAllMoviesGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public AllMoviesGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<IEnumerable<Movie>> GetListAsync()
		{
			return await _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId).GetMoviesAsync();
		}
	}
}
