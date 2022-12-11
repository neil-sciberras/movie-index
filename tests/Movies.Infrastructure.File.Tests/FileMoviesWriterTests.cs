using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File.Tests
{
	[TestFixture]
	internal class FileMoviesWriterTests
	{
		private Mock<IFileRepository> _fileRepoMock;

		[SetUp]
		public void Setup()
		{
			_fileRepoMock = new Mock<IFileRepository>();
		}

		[Test]
		public async Task GivenSomeMovies_ThenWriteMoviesWillCallRepository()
		{
			// Arrange
			_fileRepoMock.Setup(m => m.WriteContentsAsync(It.IsAny<FileOptions>(), It.IsAny<string>())).Verifiable();
			var writer = new FileMoviesWriter(_fileRepoMock.Object, new FileOptions());

			// Act
			await writer.WriteMoviesAsync(new Contracts.Models.Movies());

			// Assert
			_fileRepoMock.Verify(m => m.WriteContentsAsync(It.IsAny<FileOptions>(), It.IsAny<string>()), Times.Once);
		}
	}
}
