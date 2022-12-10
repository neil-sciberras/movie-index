using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.ContractExtensions;
using Movies.Grains.Interfaces.Redis;
using Movies.Grains.Interfaces.Redis.Updates;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Redis.Updates
{
	public class UpdateMovieGrain : Grain, IUpdateMovieGrain
	{
		private readonly IGrainFactory _grainFactory;

		public UpdateMovieGrain(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> UpdateMovieAsync(Movie movieUpdate)
		{
			var movieGrain = _grainFactory.GetGrain<IMovieGrain>(movieUpdate.Id);
			var currentMovie = await movieGrain.GetMovieAsync();

			if (currentMovie == null)
			{
				return null;
			}
			
			var updatedMovie = currentMovie.UpdateMovie(movieUpdate);

			var allMovieGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);

			await movieGrain.SetMovieAsync(updatedMovie);
			await allMovieGrain.ResetAsync();

			return updatedMovie;
		}
	}
}
