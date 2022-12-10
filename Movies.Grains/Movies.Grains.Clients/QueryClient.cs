using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class QueryClient : IQueryClient
	{
		private readonly IGrainFactory _grainFactory;

		public QueryClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
		{
			var allMoviesGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);
			return await allMoviesGrain.GetAllMoviesAsync();
		}

		public async Task<IEnumerable<Movie>> GetMoviesAsync(Genre genre)
		{
			var genreFilterGrain = _grainFactory.GetGrain<IGenreFilterGrain>(GrainIds.GenreFilterGrainId);
			return await genreFilterGrain.GetMoviesAsync(genre);
		}

		public async Task<Movie> GetMovieAsync(int id)
		{
			var movieSearchGrain = _grainFactory.GetGrain<IMovieGrain>(id);
			return await movieSearchGrain.GetMovieAsync();
		}

		public async Task<IEnumerable<Movie>> GetTopRatedMoviesAsync(int amountOfMovies)
		{
			var topRatedMoviesGrain = _grainFactory.GetGrain<ITopRatedMoviesGrain>(GrainIds.TopRatedMoviesGrainId);
			return await topRatedMoviesGrain.GetMoviesAsync(amountOfMovies);
		}
	}
}
