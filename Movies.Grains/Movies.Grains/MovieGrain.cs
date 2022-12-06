using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Infrastructure.FileSystem;
using Orleans;
using Orleans.Runtime;
using System;
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

		public Task<Movie> Get(int id) => throw new NotImplementedException();

		public Task Set(Movie movie) => throw new NotImplementedException();
		
		
	}
}
