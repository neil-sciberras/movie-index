using Moq;
using Movies.Contracts.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Tests
{
	[TestFixture]
	internal class GenreFilterGrainTests
	{
		[Test]
		public async Task GivenAListOfMovies_ThenGetMoviesReturnsTheOnesMatchingTheGenreOnly()
		{
			var grainFactoryMock = GrainFactoryMock.Get(new List<Movie>()
			{
				new(){Id = 1, Genres = new List<Genre> { Genre.History }},
				new(){Id = 2, Genres = new List<Genre> { Genre.History, Genre.Sport }},
				new(){Id = 3, Genres = new List<Genre> { Genre.Drama }},
				new(){Id = 4, Genres = new List<Genre>() }
			});

			var grain = new GenreFilterGrain(grainFactoryMock.Object);

			// Act
			var result = (await grain.GetMoviesAsync(Genre.History)).ToList();

			// Assert
			Assert.NotNull(result);
			Assert.IsNotEmpty(result);
			Assert.AreEqual(2, result.Count());
			Assert.AreEqual(1, result[0].Id);
			Assert.AreEqual(2, result[1].Id);
		}
	}
}
