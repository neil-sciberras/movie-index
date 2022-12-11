using Movies.Contracts.Models;
using Movies.Grains.ContractExtensions;
using Movies.Grains.Interfaces.Updates;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Updates
{
	public class UpdateMovieGrain : UpdateOrDeleteGrainBase, IUpdateMovieGrain
	{
		public UpdateMovieGrain(IGrainFactory grainFactory) : base(grainFactory) { }

		public async Task<Movie> UpdateMovieAsync(Movie movieUpdate)
		{
			return await EditExistingMovieAsync(movieUpdate.Id, editFunc: async movieGrain =>
				{
					var currentMovie = await movieGrain.GetMovieAsync();
					var updatedMovie = currentMovie.UpdateMovie(movieUpdate);
					await movieGrain.SetMovieAsync(updatedMovie);
				});
		}
	}
}
