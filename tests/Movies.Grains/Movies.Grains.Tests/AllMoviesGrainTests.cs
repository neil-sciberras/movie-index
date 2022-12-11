using Moq;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Movies.Infrastructure.Redis;
using NUnit.Framework;
using Orleans;
using Orleans.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Grains.Tests
{
	[TestFixture]
	internal class AllMoviesGrainTests
	{
		private Mock<IPersistentState<MovieListState>> _stateMock;
		private Mock<IGrainFactory> _grainFactoryMock;
		private Mock<IRedisReader> _redisReaderMock;
		private Mock<IMovieGrain> _movieGrainMock;

		[SetUp]
		public void Setup()
		{
			_stateMock = new Mock<IPersistentState<MovieListState>>();
			_grainFactoryMock = new Mock<IGrainFactory>();
			_redisReaderMock = new Mock<IRedisReader>();
			_movieGrainMock = new Mock<IMovieGrain>();
		}

		[Test]
		public async Task GivenANullState_WhenGetAllMoviesIsCalled_ThenStateIsRefetched()
		{
			// Arrange
			var grain = SetupForRefetch();

			// Act
			await grain.GetAllMoviesAsync();

			// Assert
			VerifyRefetch();
		}

		[Test]
		public async Task GivenGrainIsActivated_ThenStateIsRefetched()
		{
			// Arrange
			var grain = SetupForRefetch();

			// Act
			await grain.OnActivateAsync();

			// Assert
			VerifyRefetch();
		}

		[Test]
		public async Task GivenResetIsCalled_ThenStateIsSetToNull()
		{
			// Arrange
			var grain = SetupStateSetter();

			// Act
			await grain.ResetAsync();

			// Assert
			VerifyNullState();
		}

		[Test]
		public async Task GivenGrainIsDeactivated_ThenStateIsSetToNull()
		{
			// Arrange
			var grain = SetupStateSetter();

			// Act
			await grain.OnDeactivateAsync();

			// Assert
			VerifyNullState();
		}

		private AllMoviesGrain SetupForRefetch()
		{
			_stateMock.SetupSequence(mock => mock.State)
				.Returns((MovieListState)null)
				.Returns(new MovieListState { Movies = new List<Movie>() });

			_stateMock.SetupSet(mock => mock.State = It.IsAny<MovieListState>()).Verifiable();

			_redisReaderMock.Setup(mock => mock.GetAllIds())
				.Returns(new List<int> { 1, 2 }).Verifiable();
			_movieGrainMock.Setup(mock => mock.GetMovieAsync())
				.ReturnsAsync(new Movie()).Verifiable();

			_grainFactoryMock
				.Setup(mock => mock.GetGrain<IMovieGrain>(It.IsAny<long>(), null))
				.Returns(_movieGrainMock.Object).Verifiable();

			return new AllMoviesGrain(_stateMock.Object, _redisReaderMock.Object, _grainFactoryMock.Object);
		}

		private AllMoviesGrain SetupStateSetter()
		{
			_stateMock.SetupSet(mock => mock.State = It.IsAny<MovieListState>()).Verifiable();
			return new AllMoviesGrain(_stateMock.Object, _redisReaderMock.Object, _grainFactoryMock.Object);
		}

		private void VerifyRefetch()
		{
			_redisReaderMock.Verify(m => m.GetAllIds(), Times.Once);
			_grainFactoryMock.Verify(m => m.GetGrain<IMovieGrain>(It.Is<long>(id => id == 1), null), Times.Once);
			_grainFactoryMock.Verify(m => m.GetGrain<IMovieGrain>(It.Is<long>(id => id == 2), null), Times.Once);
			_movieGrainMock.Verify(m => m.GetMovieAsync(), Times.Exactly(2));
			_stateMock.VerifySet(m => m.State = It.Is<MovieListState>(mls => mls.Movies.Count() == 2));
		}

		private void VerifyNullState()
		{
			_stateMock.VerifySet(mock => mock.State = null);
		}
	}
}
