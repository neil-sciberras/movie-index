using Movies.Contracts.Models;
using Movies.Grains.Interfaces.Updates;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Updates
{
	public class DeleteMovieGrain : UpdateOrDeleteGrainBase, IDeleteMovieGrain
	{
		public DeleteMovieGrain(IGrainFactory grainFactory) : base(grainFactory)
		{
		}

		public async Task<Movie> DeleteMovieAsync(int id)
		{
			return await EditExistingMovieAsync(id, editFunc: async movieGrain => 
				await movieGrain.DeleteMovieAsync());
		}
	}
}
