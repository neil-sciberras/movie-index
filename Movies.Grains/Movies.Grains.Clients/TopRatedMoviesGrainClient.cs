using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.TopRatedMovies;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class TopRatedMoviesGrainClient : ITopRatedMoviesGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public TopRatedMoviesGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync(int amount)
		{
			var supervisor = _grainFactory.GetGrain<ITopRatedMoviesSupervisorGrain>(GrainIds.TopRatedMoviesSupervisorGrainId);
			var grain = await supervisor.GetTopRatedMoviesGrainAsync(amount);

			return await grain.GetMoviesAsync();
		}
	}
}
