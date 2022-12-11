using Movies.Infrastructure.DataSource.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public class FileMoviesReader : IMoviesReader
	{
		private readonly IFileRepository _fileRepository;
		private readonly FileOptions _fileOptions;

		public FileMoviesReader(IFileRepository fileRepository, FileOptions fileOptions)
		{
			_fileRepository = fileRepository;
			_fileOptions = fileOptions;
		}

		public async Task<Contracts.Models.Movies> ReadMoviesAsync()
		{
			var serializedContents = await _fileRepository.GetContentsAsync(_fileOptions);

			return JsonConvert.DeserializeObject<Contracts.Models.Movies>(serializedContents);
		}
	}
}
