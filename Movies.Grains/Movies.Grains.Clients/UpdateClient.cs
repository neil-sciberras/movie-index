using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.Updates;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class UpdateClient : IUpdateClient
	{
		private readonly IGrainFactory _grainFactory;

		public UpdateClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> AddMovieAsync(NewMovie newMovie)
		{
			var addMoviesGrain = _grainFactory.GetGrain<IAddMovieGrain>(GrainIds.AddMovieGrainId);
			return await addMoviesGrain.AddMovieAsync(newMovie);
		}

		public async Task<Movie> DeleteMovieAsync(int id)
		{
			var deleteMovieGrain = _grainFactory.GetGrain<IDeleteMovieGrain>(GrainIds.DeleteMovieGrainId);
			return await deleteMovieGrain.DeleteMovieAsync(id);
		}

		public async Task<Movie> UpdateMovieAsync(Movie movie)
		{
			var updateMovieGrain = _grainFactory.GetGrain<IUpdateMovieGrain>(GrainIds.UpdateMovieGrainId);
			return await updateMovieGrain.UpdateMovieAsync(movie);
		}
	}
}
