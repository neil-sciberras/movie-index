using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Grains.Updates
{
	public abstract class UpdateOrDeleteGrainBase : Grain
	{
		protected readonly IGrainFactory GrainsFactory;

		protected UpdateOrDeleteGrainBase(IGrainFactory grainsFactory)
		{
			GrainsFactory = grainsFactory;
		}

		public async Task<Movie> EditExistingMovieAsync(int id, Func<IMovieGrain, Task> editFunc)
		{
			var movieGrain = GrainsFactory.GetGrain<IMovieGrain>(id);
			var movie = await movieGrain.GetMovieAsync();

			if (movie == null)
			{
				return null;
			}

			var allMoviesGrain = GrainsFactory.GetGrain<IAllMoviesGrain>(GrainIds.AllMoviesGrainId);

			await editFunc(movieGrain);
			
			await allMoviesGrain.ResetAsync();

			return movie;
		}
	}
}
