using Moq;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Interfaces.Updates;
using NUnit.Framework;
using Orleans;
using System.Threading.Tasks;

namespace Movies.Grains.Clients.Tests
{
	[TestFixture]
	internal class UpdateClientTests
	{
		private Mock<IGrainFactory> _grainFactoryMock;
		private Mock<IAddMovieGrain> _addMovieGrainMock;
		private Mock<IDeleteMovieGrain> _deleteMovieGrainMock;
		private Mock<IUpdateMovieGrain> _updateMovieGrainMock;

		private IUpdateClient UpdateClient => new UpdateClient(_grainFactoryMock.Object);

		[SetUp]
		public void Setup()
		{
			_grainFactoryMock = new Mock<IGrainFactory>();
			_addMovieGrainMock = new Mock<IAddMovieGrain>();
			_deleteMovieGrainMock = new Mock<IDeleteMovieGrain>();
			_updateMovieGrainMock = new Mock<IUpdateMovieGrain>();
		}

		[Test]
		public async Task AddMovieTest()
		{
			// Arrange
			_addMovieGrainMock.Setup(m => m.AddMovieAsync(It.IsAny<NewMovie>()))
				.ReturnsAsync(new Movie()).Verifiable();
			_grainFactoryMock
				.Setup(m => m.GetGrain<IAddMovieGrain>(It.IsAny<string>(), null))
				.Returns(_addMovieGrainMock.Object);

			// Act
			var result = await UpdateClient.AddMovieAsync(new NewMovie());

			// Assert
			Assert.NotNull(result);
			_addMovieGrainMock.Verify(m => m.AddMovieAsync(It.IsAny<NewMovie>()), Times.Once);
		}

		[Test]
		public async Task DeleteMovieTest()
		{
			// Arrange
			_deleteMovieGrainMock.Setup(m => m.DeleteMovieAsync(It.IsAny<int>()))
				.ReturnsAsync(new Movie()).Verifiable();
			_grainFactoryMock
				.Setup(m => m.GetGrain<IDeleteMovieGrain>(It.IsAny<string>(), null))
				.Returns(_deleteMovieGrainMock.Object);

			// Act
			var result = await UpdateClient.DeleteMovieAsync(1);

			// Assert
			Assert.NotNull(result);
			_deleteMovieGrainMock.Verify(m => m.DeleteMovieAsync(1), Times.Once);
		}

		[Test]
		public async Task UpdateMovieTest()
		{
			// Arrange
			_updateMovieGrainMock.Setup(m => m.UpdateMovieAsync(It.IsAny<Movie>()))
				.ReturnsAsync(new Movie()).Verifiable();
			_grainFactoryMock
				.Setup(m => m.GetGrain<IUpdateMovieGrain>(It.IsAny<string>(), null))
				.Returns(_updateMovieGrainMock.Object);

			// Act
			var result = await UpdateClient.UpdateMovieAsync(new Movie());

			// Assert
			Assert.NotNull(result);
			_updateMovieGrainMock.Verify(m => m.UpdateMovieAsync(It.IsAny<Movie>()), Times.Once);
		}
	}
}
