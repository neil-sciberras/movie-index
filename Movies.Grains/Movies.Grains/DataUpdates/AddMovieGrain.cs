using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.DataUpdates;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.DataUpdates
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

			var movies = (await allMoviesGrain.GetMoviesAsync()).ToList();

			var nextAvailableId = movies.Max(m => m.Id) + 1;

			var movie = ConvertToMovie(newMovie, nextAvailableId);
			movies.Add(movie);

			await allMoviesGrain.SetMovieListAsync(movies);

			return movie;
		}

		//TODO: replace with automapper
		private static Movie ConvertToMovie(NewMovie newMovie, int newId)
		{
			return new Movie
			{
				Id = newId,
				Key = newMovie.Key,
				Name = newMovie.Name,
				Description = newMovie.Description,
				Genres = newMovie.Genres,
				Rate = newMovie.Rate,
				Length = newMovie.Length,
				Image = newMovie.Image
			};
		}
	}
}
