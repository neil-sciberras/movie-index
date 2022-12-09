using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.DataQueries.Supervisors;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class GenreFilterGrainClient : IGenreFilterGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public GenreFilterGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<IEnumerable<Movie>> GetMoviesAsync(int genre)
		{
			var supervisor = _grainFactory.GetGrain<IGenreFilterSupervisorGrain>(GrainIds.GenreFilterSupervisorGrainId);
			var grain = await supervisor.GetSupervisedGrainAsync(genre);

			return await grain.GetMoviesAsync();
		}
	}
}
