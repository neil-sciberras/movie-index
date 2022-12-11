using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Movies.Infrastructure.File
{
	[ExcludeFromCodeCoverage]
	public class FileOptions
	{
		public string RootDirectory { get; set; }
		public string FileName { get; set; }
		public string FullFileName => Path.Combine(RootDirectory, FileName);
	}
}
