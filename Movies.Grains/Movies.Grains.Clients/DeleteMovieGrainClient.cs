using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.DataUpdates;
using Movies.Grains.Interfaces.Exceptions;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class DeleteMovieGrainClient : IDeleteMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public DeleteMovieGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> DeleteMovieAsync(int id)
		{
			try
			{
				var deleteMovieGrain = _grainFactory.GetGrain<IDeleteMovieGrain>(GrainIds.DeleteMovieGrainId);
				return await deleteMovieGrain.DeleteMovieAsync(id);
			}
			catch (MovieNotFoundException)
			{
				return null;
			}
		}
	}
}
