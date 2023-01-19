using Moq;
using Movies.Grains.Updates;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.Grains.Tests.Updates
{
	[TestFixture]
	internal class DeleteMovieGrainTests
	{
		private UpdateTestContext _testContext;

		[SetUp]
		public void Setup()
		{
			_testContext = new UpdateTestContext();
		}

		[Test]
		public async Task GivenAMovieIsDeleted_ThenMovieGrainIsCalled_AndAllMoviesListReset()
		{
			// Arrange
			_testContext.SetupForValidDelete();

			var grain = new DeleteMovieGrain(_testContext.GrainFactoryMock.Object);

			// Act
			var result = await grain.DeleteMovieAsync(1);

			// Assert
			Assert.NotNull(result);
			_testContext.MovieGrainMock.Verify(m => m.DeleteMovieAsync(), Times.Once);
			_testContext.AllMoviesGrainMock.Verify(m => m.ResetAsync(), Times.Once);
		}

		[Test]
		public async Task GivenDeleteIsCalledForAMovieThatDoesNotExist_ThenMethodReturnsNull()
		{
			// Arrange
			_testContext.SetupInexistentMovie();
			var grain = new DeleteMovieGrain(_testContext.GrainFactoryMock.Object);
			
			// Act
			var result = await grain.DeleteMovieAsync(1);

			// Assert
			Assert.Null(result);
		}
	}
}
