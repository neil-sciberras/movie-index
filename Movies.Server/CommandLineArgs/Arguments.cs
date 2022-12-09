namespace Movies.Server.CommandLineArgs
{
	public class Arguments
	{
		public Arguments(string storageDir)
		{
			StorageDir = storageDir;
		}

		public string StorageDir { get; }
	}
}