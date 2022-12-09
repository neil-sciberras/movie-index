﻿using Movies.Contracts.Grains;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.DataUpdates;
using Movies.Grains.Interfaces.Exceptions;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Clients
{
	public class UpdateMovieGrainClient : IUpdateMovieGrainClient
	{
		private readonly IGrainFactory _grainFactory;

		public UpdateMovieGrainClient(IGrainFactory grainFactory)
		{
			_grainFactory = grainFactory;
		}

		public async Task<Movie> UpdateMovieAsync(Movie movie)
		{
			try
			{
				var updateMovieGrain = _grainFactory.GetGrain<IUpdateMovieGrain>(GrainIds.UpdateMovieGrainId);
				return await updateMovieGrain.UpdateAsync(movie);
			}
			catch (MovieNotFoundException)
			{
				return null;
			}
		}
	}
}