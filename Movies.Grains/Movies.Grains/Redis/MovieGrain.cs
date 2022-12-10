using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces.Redis;
using Orleans;
using Orleans.Runtime;
using System.Threading.Tasks;

namespace Movies.Grains.Redis
{
	public class MovieGrain : Grain, IMovieGrain
	{
		private readonly IPersistentState<Movie> _state;

		public MovieGrain(
			[PersistentState(stateName: "movieState", storageName: GrainStorageNames.RedisStorage)]
			IPersistentState<Movie> state)
		{
			_state = state;
		}

		public Task<Movie> GetMovieAsync()
		{
			return Task.FromResult(_state.State);
		}

		public async Task SetMovieAsync(Movie movie)
		{
			_state.State = movie;
			await _state.WriteStateAsync();
		}

		public async Task DeleteMovieAsync()
		{
			_state.State = null;
			await _state.ClearStateAsync();
		}
	}
}
