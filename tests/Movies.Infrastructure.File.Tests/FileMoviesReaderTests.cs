using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File.Tests
{
	[TestFixture]
	internal class FileMoviesReaderTests
	{
		private const string SampleMoviesJson = 
			"{\"movies\":[{\"id\":13,\"key\":\"bridge-of-spies\"," + "\"name\":\"BridgeofSpies\"," +
			"\"description\":\"DuringtheColdWar,anAmericanlawyerisrecruitedtodefendanarrestedSovietspyin\"," +
			"\"genres\":[\"Biography\",\"Drama\",\"Thriller\"],\"rate\":\"7.7\",\"length\":\"2hr22mins\"," +
			"\"img\":\"bridge-of-spies.jpg\"},{\"id\":16,\"key\":\"tracers\",\"name\":\"Tracers\",\"description" +
			"\":\"WantedbytheChinesemafia,aNewYorkCitybikemessengerescapesintotheworldofparkouraftermeetingabe." +
			"\",\"genres\":[\"Action\",\"Crime\",\"Drama\"],\"rate\":\"5.6\",\"length\":\"1hr34mins\",\"img\":\"tracers.jpg\"}]}";

		private Mock<IFileRepository> _fileRepoMock;

		[SetUp]
		public void Setup()
		{
			_fileRepoMock = new Mock<IFileRepository>();
		}

		[Test]
		public async Task GivenSomeContents_ThenReadMoviesReturnsMovies()
		{
			// Arrange
			_fileRepoMock
				.Setup(m => m.GetContentsAsync(It.IsAny<FileOptions>()))
				.ReturnsAsync(SampleMoviesJson);

			var reader = new FileMoviesReader(_fileRepoMock.Object, new FileOptions("", ""));
			
			// Act
			var results = await reader.ReadMoviesAsync();

			// Assert
			Assert.NotNull(results);
			Assert.AreEqual(2, results.MovieList.Count());
		}
	}
}
