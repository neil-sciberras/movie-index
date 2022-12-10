using System.IO;

namespace Movies.Infrastructure.Orleans.Storage.File
{
	/// <summary>
	/// <see href="https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage"/>
	/// </summary>
	public class FileStorageOptions
	{
		public string FullFileName => Path.Combine(RootDirectory, FileName);
		public string RootDirectory { get; set; }
		public string FileName { get; set; }
	}
}
