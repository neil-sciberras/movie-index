using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.ContractExtensions;
using Movies.Grains.Interfaces.Redis;
using Movies.Grains.Interfaces.Redis.Updates;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Redis.Updates
{
	public class AddMovieGrain : Grain, IAddMovieGrain
	{
		private readonly IGrainFactory _grainFactory;

		public AddMovieGrain(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> AddMovieAsync(NewMovie newMovie)
		{
			var allMoviesGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
			var allMovies = await allMoviesGrain.GetAllMoviesAsync();
			var nextAvailableId = allMovies.Select(m => m.Id).Max() + 1;
			var movieToAdd = newMovie.ConvertToMovie(nextAvailableId);

			await _grainFactory.GetGrain<IMovieGrain>(movieToAdd.Id).SetMovieAsync(movieToAdd);
			await allMoviesGrain.ResetAsync();

			return movieToAdd;
		}
	}
}
