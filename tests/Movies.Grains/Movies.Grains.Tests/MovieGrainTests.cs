using Moq;
using Movies.Contracts.Models;
using NUnit.Framework;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Tests
{
	[TestFixture]
	internal class MovieGrainTests
	{
		private Mock<IPersistentState<Movie>> _stateMock;

		[SetUp]
		public void Setup()
		{
			_stateMock = new Mock<IPersistentState<Movie>>();
		}

		private static IEnumerable<object[]> MovieStateScenarios()
		{
			yield return new object[] { new Movie(), false };
			yield return new object[] { null, true };
		}

		[TestCaseSource(nameof(MovieStateScenarios))]
		public async Task GivenAState_ThenGetMovieReturnsNotNull(Movie movie, bool isNullExpected)
		{
			// Arrange
			_stateMock.Setup(mock => mock.State).Returns(movie);
			var grain = new MovieGrain(_stateMock.Object);

			// Act
			var result = await grain.GetMovieAsync();

			// Assert
			Assert.AreEqual(isNullExpected, result == null);
		}

		[Test]
		public async Task GivenSetMovieIsCalled_ThenStateIsSet()
		{
			// Arrange
			_stateMock.SetupSet(mock => mock.State = It.IsAny<Movie>()).Verifiable();
			var grain = new MovieGrain(_stateMock.Object);

			// Act
			await grain.SetMovieAsync(new Movie());

			// Assert
			_stateMock.VerifySet(mock => mock.State = It.IsAny<Movie>());
		}

		[Test]
		public async Task GivenDeleteMovieIsCalled_ThenStateIsSetToNull()
		{
			// Arrange
			_stateMock.SetupSet(mock => mock.State = It.IsAny<Movie>()).Verifiable();
			var grain = new MovieGrain(_stateMock.Object);

			// Act
			await grain.DeleteMovieAsync();

			// Assert
			_stateMock.VerifySet(mock => mock.State = null);
		}
	}
}
