using System.IO;

namespace Movies.Infrastructure.File
{
	public class FileStorageOptions
	{
		private readonly string _rootDirectory;
		private readonly string _fileName;

		public FileStorageOptions(string rootDirectory, string fileName)
		{
			_rootDirectory = rootDirectory;
			_fileName = fileName;
		}

		public string FullFileName => Path.Combine(_rootDirectory, _fileName);
	}
}
