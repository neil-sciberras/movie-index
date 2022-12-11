using Moq;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;
using System.Collections.Generic;

namespace Movies.Grains.Tests
{
	internal static class GrainFactoryMock
	{
		public static Mock<IGrainFactory> Get(IEnumerable<Movie> mockedMovies, Mock<IAllMoviesGrain> allMoviesGrainMock = null)
		{
			var grainFactoryMock = new Mock<IGrainFactory>();
			allMoviesGrainMock ??= new Mock<IAllMoviesGrain>();

			allMoviesGrainMock
				.Setup(m => m.GetAllMoviesAsync())
				.ReturnsAsync(mockedMovies);

			grainFactoryMock
				.Setup(m => m.GetGrain<IAllMoviesGrain>(It.IsAny<string>(), null))
				.Returns(allMoviesGrainMock.Object);

			return grainFactoryMock;
		}
	}
}
