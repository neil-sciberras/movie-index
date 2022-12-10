using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public class FileReader : IFileReader
	{
		private readonly FileInfo _fileInfo;

		public FileReader(FileStorageOptions fileStorageOptions)
		{
			_fileInfo = new FileInfo(fileStorageOptions.FullFileName);

			if (!_fileInfo.Exists)
			{
				throw new FileNotFoundException($"File '{_fileInfo.FullName}' does not exist");
			}
		}

		public async Task<Movies> ReadMoviesAsync()
		{
			string serializedContents;

			using (var stream = _fileInfo.OpenText())
			{
				serializedContents = await stream.ReadToEndAsync();
			}

			return JsonConvert.DeserializeObject<Movies>(serializedContents);
		}
	}
}
