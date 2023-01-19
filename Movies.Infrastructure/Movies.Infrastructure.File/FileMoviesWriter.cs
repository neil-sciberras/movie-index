using Movies.Infrastructure.DataSource.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public class FileMoviesWriter : IMoviesWriter
	{
		private readonly IFileRepository _fileRepository;
		private readonly FileOptions _fileOptions;

		public FileMoviesWriter(IFileRepository fileRepository, FileOptions fileOptions)
		{
			_fileRepository = fileRepository;
			_fileOptions = fileOptions;
		}
		
		public async Task WriteMoviesAsync(Contracts.Models.Movies movies)
		{
			var serializedContents = JsonConvert.SerializeObject(movies, Formatting.Indented);

			await _fileRepository.WriteContentsAsync(_fileOptions, serializedContents);
		}
	}
}
