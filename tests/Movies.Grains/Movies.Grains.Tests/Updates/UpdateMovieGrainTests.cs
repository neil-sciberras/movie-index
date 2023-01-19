using Moq;
using Movies.Contracts.Models;
using Movies.Grains.Updates;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.Grains.Tests.Updates
{
	[TestFixture]
	internal class UpdateMovieGrainTests
	{
		private UpdateTestContext _testContext;

		[SetUp]
		public void Setup()
		{
			_testContext = new UpdateTestContext();
		}

		[Test]
		public async Task GivenAMovieIsUpdated_ThenMovieGrainIsCalled_AndAllMoviesListReset()
		{
			// Arrange
			_testContext.SetupForValidUpdate();
			var grain = new UpdateMovieGrain(_testContext.GrainFactoryMock.Object);

			// Act
			var result = await grain.UpdateMovieAsync(new Movie());

			// Assert
			Assert.NotNull(result);
			_testContext.MovieGrainMock.Verify(m => m.SetMovieAsync(It.IsAny<Movie>()), Times.Once);
			_testContext.AllMoviesGrainMock.Verify(m => m.ResetAsync(), Times.Once);
		}

		[Test]
		public async Task GivenUpdateIsCalledForAMovieThatDoesNotExist_ThenMethodReturnsNull()
		{
			// Arrange
			_testContext.SetupInexistentMovie();
			var grain = new UpdateMovieGrain(_testContext.GrainFactoryMock.Object);
			
			// Act
			var result = await grain.UpdateMovieAsync(new Movie());

			// Assert
			Assert.Null(result);
		}
	}
}
