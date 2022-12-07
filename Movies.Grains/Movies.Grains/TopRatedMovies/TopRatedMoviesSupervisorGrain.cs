using Movies.Contracts.Grains;
using Movies.Grains.Interfaces.TopRatedMovies;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Grains.TopRatedMovies
{
	/// <summary>
	/// This enables the resetting of all <see cref="ITopRatedMoviesGrain"/> states on the event of a file reload.
	/// </summary>
	public class TopRatedMoviesSupervisorGrain : ITopRatedMoviesSupervisorGrain
	{
		private readonly IPersistentState<IEnumerable<ITopRatedMoviesGrain>> _supervisorState;
		private readonly IGrainFactory _grainFactory;

		public TopRatedMoviesSupervisorGrain(
			[PersistentState(stateName: "supervisorState", storageName: GrainStorageNames.MemoryStorage)] IPersistentState<IEnumerable<ITopRatedMoviesGrain>> supervisorState, 
			IGrainFactory grainFactory)
		{
			_supervisorState = supervisorState;
			_grainFactory = grainFactory;
		}

		public void ResetAll()
		{
			foreach (var topRatedMoviesGrain in _supervisorState.State)
			{
				topRatedMoviesGrain.ResetState();
			}
		}

		public void RegisterNewGrain(int amountOfMovies)
		{
			List<ITopRatedMoviesGrain> grainList;

			grainList = _supervisorState?.State == null 
				? new List<ITopRatedMoviesGrain>() 
				: _supervisorState!.State!.ToList();

			var newGrain = _grainFactory.GetGrain<ITopRatedMoviesGrain>(amountOfMovies);

			grainList.Add(newGrain);

			_supervisorState.State = grainList;
		}
	}
}
