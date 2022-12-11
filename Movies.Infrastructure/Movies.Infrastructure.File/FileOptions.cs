using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Movies.Infrastructure.File
{
	[ExcludeFromCodeCoverage]
	public class FileOptions
	{
		private readonly string _rootDirectory;
		private readonly string _fileName;

		public FileOptions(string rootDirectory, string fileName)
		{
			_rootDirectory = rootDirectory;
			_fileName = fileName;
		}

		public string FullFileName => Path.Combine(_rootDirectory, _fileName);
	}
}
