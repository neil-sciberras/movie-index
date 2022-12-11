using Moq;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Grains.Updates;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Tests.Updates
{
	[TestFixture]
	internal class AddMovieGrainTests
	{
		private Mock<IMovieGrain> _movieGrainMock;
		private Mock<IAllMoviesGrain> _allMoviesGrainMock;

		[SetUp]
		public void Setup()
		{
			_movieGrainMock = new Mock<IMovieGrain>();
			_allMoviesGrainMock = new Mock<IAllMoviesGrain>();
		}

		[Test]
		public async Task GivenAddMovieIsCalled_ThenMovieIsAddedAndMovieListReset()
		{
			// Arrange
			_allMoviesGrainMock.Setup(m => m.ResetAsync()).Verifiable();

			var grainFactoryMock = GrainFactoryMock.Get(
				mockedMovies: new List<Movie> { new() { Id = 1 }, new() { Id = 2 } },
				allMoviesGrainMock: _allMoviesGrainMock);

			_movieGrainMock.Setup(m => m.SetMovieAsync(It.IsAny<Movie>())).Verifiable();
			
			grainFactoryMock
				.Setup(m => m.GetGrain<IMovieGrain>(It.IsAny<long>(), null))
				.Returns(_movieGrainMock.Object);

			var grain = new AddMovieGrain(grainFactoryMock.Object);
			
			// Act
			var result = await grain.AddMovieAsync(new NewMovie());

			// Assert
			Assert.NotNull(result);
			Assert.AreEqual(3, result.Id);
			
			_movieGrainMock.Verify(m => m.SetMovieAsync(It.Is<Movie>(m => m.Id == 3)), Times.Once);
			_allMoviesGrainMock.Verify(m => m.ResetAsync(), Times.Once);
		}
	}
}
