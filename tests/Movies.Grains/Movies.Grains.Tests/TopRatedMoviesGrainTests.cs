using Movies.Contracts.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Tests
{
	[TestFixture]
	internal class TopRatedMoviesGrainTests
	{
		[Test]
		public async Task GivenAListOfMovies_ThenGetMoviesReturnsTheRatedOnly()
		{
			// Arrange
			var grainFactoryMock = GrainFactoryMock.Get(new List<Movie>()
			{
				new() { Id = 1, Rate = "1" }, 
				new() { Id = 2, Rate = "2" }, 
				new() { Id = 3, Rate = "3" }
			});

			var grain = new TopRatedMoviesGrain(grainFactoryMock.Object);

			// Act
			var result = (await grain.GetMoviesAsync(2)).ToList();

			// Assert
			Assert.NotNull(result);
			Assert.IsNotEmpty(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(3, result[0].Id);
			Assert.AreEqual(2, result[1].Id);
		}
	}
}
