using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.DataUpdates;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class AddMovieGrainClient : IAddMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public AddMovieGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> AddMovieAsync(NewMovie newMovie)
		{
			var addMovieGrain = _grainFactory.GetGrain<IAddMovieGrain>(GrainIds.AddMovieGrainId);
			return await addMovieGrain.AddMovieAsync(newMovie);
		}
	}
}
