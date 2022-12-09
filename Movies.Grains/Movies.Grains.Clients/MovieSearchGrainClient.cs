using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.DataQueries.Supervisors;
using Orleans;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class MovieSearchGrainClient : IMovieSearchGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public MovieSearchGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> GetMovie(int id)
		{
			var supervisor = _grainFactory.GetGrain<IMovieSearchSupervisorGrain>(GrainIds.MovieSearchSupervisorGrainId);
			var grain = await supervisor.GetSupervisedGrainAsync(id);

			return await grain.GetMovieAsync();
		}
	}
}
