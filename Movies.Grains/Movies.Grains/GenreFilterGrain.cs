using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class GenreFilterGrain : Grain, IGenreFilterGrain
	{
		private readonly IGrainFactory _grainFactory;

		public GenreFilterGrain(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<IEnumerable<Movie>> GetMoviesAsync(Genre genre)
		{
			var allMovies = await _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId).GetAllMoviesAsync();

			return allMovies.Where(m => m.Genres.Contains(genre)).ToList();
		}
	}
}
