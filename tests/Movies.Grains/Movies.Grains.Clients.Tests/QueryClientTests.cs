using Moq;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces;
using NUnit.Framework;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Tests
{
	[TestFixture]
	internal class QueryClientTests
	{
		private Mock<IGrainFactory> _grainFactoryMock;
		private Mock<IAllMoviesGrain> _allMoviesGrainMock;
		private Mock<IGenreFilterGrain> _genreFilterGrainMock;
		private Mock<IMovieGrain> _movieGrainMock;
		private Mock<ITopRatedMoviesGrain> _topRatedMoviesGrainMock;

		private IQueryClient QueryClient => new QueryClient(_grainFactoryMock.Object);

		[SetUp]
		public void Setup()
		{
			_grainFactoryMock = new Mock<IGrainFactory>();
			_allMoviesGrainMock = new Mock<IAllMoviesGrain>();
			_genreFilterGrainMock = new Mock<IGenreFilterGrain>();
			_movieGrainMock = new Mock<IMovieGrain>();
			_topRatedMoviesGrainMock = new Mock<ITopRatedMoviesGrain>();
		}

		[Test]
		public async Task GetAllMoviesTest()
		{
			// Arrange
			_allMoviesGrainMock.Setup(m => m.GetAllMoviesAsync())
				.ReturnsAsync(new List<Movie>()).Verifiable();
			
			_grainFactoryMock
				.Setup(m => m.GetGrain<IAllMoviesGrain>(It.IsAny<string>(), null))
				.Returns(_allMoviesGrainMock.Object);

			// Act
			var result = await QueryClient.GetAllMoviesAsync();

			// Assert
			Assert.NotNull(result);
			_allMoviesGrainMock.Verify(m => m.GetAllMoviesAsync(), Times.Once);
		}

		[Test]
		public async Task GetMoviesByGenreTest()
		{
			// Arrange
			_genreFilterGrainMock.Setup(m => m.GetMoviesAsync(It.IsAny<Genre>()))
				.ReturnsAsync(new List<Movie>()).Verifiable();
			
			_grainFactoryMock
				.Setup(m => m.GetGrain<IGenreFilterGrain>(It.IsAny<string>(), null))
				.Returns(_genreFilterGrainMock.Object);

			// Act
			var result = await QueryClient.GetMoviesAsync(Genre.History);

			// Assert
			Assert.NotNull(result);
			_genreFilterGrainMock.Verify(m => m.GetMoviesAsync(It.Is<Genre>(g => g == Genre.History)), Times.Once);
		}

		[Test]
		public async Task GetMovieTest()
		{
			// Arrange
			_movieGrainMock.Setup(m => m.GetMovieAsync())
				.ReturnsAsync(new Movie()).Verifiable();
			
			_grainFactoryMock
				.Setup(m => m.GetGrain<IMovieGrain>(It.IsAny<long>(), null))
				.Returns(_movieGrainMock.Object);

			// Act
			var result = await QueryClient.GetMovieAsync(1);

			// Assert
			Assert.NotNull(result);
			_movieGrainMock.Verify(m => m.GetMovieAsync(), Times.Once);
		}

		[Test]
		public async Task GetTopRatedMoviesTest()
		{
			// Arrange
			_topRatedMoviesGrainMock.Setup(m => m.GetMoviesAsync(It.IsAny<int>()))
				.ReturnsAsync(new List<Movie>()).Verifiable();
			
			_grainFactoryMock
				.Setup(m => m.GetGrain<ITopRatedMoviesGrain>(It.IsAny<string>(), null))
				.Returns(_topRatedMoviesGrainMock.Object);

			// Act
			var result = await QueryClient.GetTopRatedMoviesAsync(2);

			// Assert
			Assert.NotNull(result);
			_topRatedMoviesGrainMock.Verify(m => m.GetMoviesAsync(2), Times.Once);
		}
	}
}
