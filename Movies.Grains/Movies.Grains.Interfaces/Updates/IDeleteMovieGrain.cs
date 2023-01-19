﻿using Movies.Contracts.Models;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Interfaces.Updates
{
	public interface IDeleteMovieGrain : IGrainWithStringKey
	{
		Task<Movie> DeleteMovieAsync(int id);
	}
}
