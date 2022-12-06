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

		public MovieGrain(IPersistentState<Movie> state)
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
