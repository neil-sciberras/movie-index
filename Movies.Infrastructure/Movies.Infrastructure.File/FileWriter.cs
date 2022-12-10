using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Movies.Infrastructure.File
{
	public class FileWriter : IFileWriter
	{
		private readonly FileInfo _fileInfo;

		public FileWriter(FileStorageOptions fileStorageOptions)
		{
			_fileInfo = new FileInfo(fileStorageOptions.FullFileName);

			if (!_fileInfo.Exists)
			{
				throw new FileNotFoundException($"File '{_fileInfo.FullName}' does not exist");
			}
		}
		
		public async Task WriteMoviesAsync(Movies movies)
		{
			var serializedContents = JsonConvert.SerializeObject(movies, Formatting.Indented);

			using (var streamWriter = new StreamWriter(_fileInfo.Open(FileMode.Create, FileAccess.Write)))
			{
				await streamWriter.WriteAsync(serializedContents);
			}

			_fileInfo.Refresh();
		}
	}
}
