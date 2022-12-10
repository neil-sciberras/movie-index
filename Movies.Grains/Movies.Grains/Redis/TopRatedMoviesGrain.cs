using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces.Redis;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Redis
{
	public class TopRatedMoviesGrain : Grain, ITopRatedMoviesGrain
	{
		private readonly IGrainFactory _grainFactory;

		public TopRatedMoviesGrain(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<IEnumerable<Movie>> GetMoviesAsync(int amountOfMovies)
		{
			var allMovies = await _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId).GetAllMoviesAsync();
			
			return allMovies
				.OrderByDescending(m => m.Rate)
				.Take(amountOfMovies)
				.ToList();
		}
	}
}
