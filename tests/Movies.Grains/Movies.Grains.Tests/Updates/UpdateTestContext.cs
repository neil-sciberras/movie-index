using Moq;
using Movies.Contracts.Models;
using Movies.Grains.Interfaces;
using Orleans;

namespace Movies.Grains.Tests.Updates
{
	internal class UpdateTestContext
	{
		public Mock<IGrainFactory> GrainFactoryMock { get; }
		public Mock<IMovieGrain> MovieGrainMock { get; }
		public Mock<IAllMoviesGrain> AllMoviesGrainMock { get; }

		public UpdateTestContext()
		{
			GrainFactoryMock = new Mock<IGrainFactory>();
			MovieGrainMock = new Mock<IMovieGrain>();
			AllMoviesGrainMock = new Mock<IAllMoviesGrain>();
		}

		public void SetupForValidDelete()
		{
			MovieGrainMock.Setup(m => m.DeleteMovieAsync()).Verifiable();

			ValidSetup();
		}

		public void SetupForValidUpdate()
		{
			MovieGrainMock.Setup(m => m.SetMovieAsync(It.IsAny<Movie>())).Verifiable();

			ValidSetup();
		}

		public void SetupInexistentMovie()
		{
			MovieGrainMock.Setup(m => m.GetMovieAsync()).ReturnsAsync((Movie)null);

			GrainFactoryMock
				.Setup(m => m.GetGrain<IMovieGrain>(It.IsAny<long>(), null))
				.Returns(MovieGrainMock.Object);
		}

		private void ValidSetup()
		{
			MovieGrainMock.Setup(m => m.GetMovieAsync()).ReturnsAsync(new Movie());
			AllMoviesGrainMock.Setup(m => m.ResetAsync()).Verifiable();

			GrainFactoryMock
				.Setup(m => m.GetGrain<IMovieGrain>(It.IsAny<long>(), null))
				.Returns(MovieGrainMock.Object);

			GrainFactoryMock
				.Setup(m => m.GetGrain<IAllMoviesGrain>(It.IsAny<string>(), null))
				.Returns(AllMoviesGrainMock.Object);
		}
	}
}
