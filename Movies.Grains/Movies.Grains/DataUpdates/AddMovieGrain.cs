using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.DataUpdates.ContractExtensions;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.DataUpdates;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.DataUpdates
{
	public class AddMovieGrain : Grain, IAddMovieGrain
	{
		private readonly IAllMoviesGrain _allMoviesGrain;

		public AddMovieGrain(IGrainFactory grainFactory)
		{
			_allMoviesGrain = grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
		}

		public async Task<Movie> AddMovieAsync(NewMovie newMovie)
		{
			var movies = (await _allMoviesGrain.GetMoviesAsync()).ToList();

			var nextAvailableId = movies.Max(m => m.Id) + 1;

			var movie = newMovie.ConvertToMovie(nextAvailableId);
			movies.Add(movie);

			await _allMoviesGrain.SetMovieListAsync(movies);

			return movie;
		}
	}
}
