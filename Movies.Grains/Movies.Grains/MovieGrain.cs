using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using Orleans.Runtime;
using System.Threading.Tasks;

namespace Movies.Grains
{
	public class MovieGrain : Grain, IMovieGrain
	{
		private readonly IPersistentState<Movie> _state;

		public MovieGrain([PersistentState(stateName: "Movie", storageName: GrainStorageNames.MemoryStorage)]IPersistentState<Movie> state)
		{
			_state = state;
		}

		public Task<Movie> GetAsync()
		{
			return Task.FromResult(_state.State);
		}

		public Task SetAsync(Movie movie)
		{
			_state.State = movie;
			return Task.CompletedTask;
		}
	}
}
