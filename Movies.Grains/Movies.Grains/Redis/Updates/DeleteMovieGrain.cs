using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces.Redis;
using Movies.Grains.Interfaces.Redis.Updates;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Redis.Updates
{
	public class DeleteMovieGrain : Grain, IDeleteMovieGrain
	{
		private readonly IGrainFactory _grainFactory;

		public DeleteMovieGrain(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> DeleteMovieAsync(int id)
		{
			var movieGrain = _grainFactory.GetGrain<IMovieGrain>(id);
			var movie = await movieGrain.GetMovieAsync();

			if (movie == null)
			{
				return null;
			}
			
			var allMovieGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);

			await movieGrain.DeleteMovieAsync();
			await allMovieGrain.ResetAsync();

			return movie;
		}
	}
}
