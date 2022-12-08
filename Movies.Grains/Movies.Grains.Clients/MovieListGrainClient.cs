using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class MovieListGrainClient : IMovieListGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public MovieListGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		//TODO: Check if it's necessary to await here (and other similar places), or simply return the original task
		public async Task<IEnumerable<Movie>> GetListAsync()
		{
			return await _grainFactory.GetGrain<IMovieListGrain>(GrainIds.MovieListGrainId).GetAllMoviesAsync();
		}
	}
}
