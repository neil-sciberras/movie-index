using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.Interfaces.Updates;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Updates
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

			var allMoviesGrain = _grainFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);

			await movieGrain.DeleteMovieAsync();
			await allMoviesGrain.ResetAsync();

			return movie;
		}
	}
}
