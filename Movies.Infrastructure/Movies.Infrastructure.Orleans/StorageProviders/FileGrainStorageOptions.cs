using System.IO;

namespace Movies.Infrastructure.Orleans.StorageProviders
{
	/// <summary>
	/// <see href="https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage"/>
	/// </summary>
	public class FileGrainStorageOptions
	{
		public string FullFileName => Path.Combine(RootDirectory, FileName);
		public string RootDirectory { get; set; }
		public string FileName { get; set; }
	}
}
