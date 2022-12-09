using System;
using System.Linq;

namespace Movies.Server.CommandLineArgs
{
	public static class CommandLineArgHelper
	{
		private const string StorageDir = "-storageDir";

		public static Arguments ParseArguments(string[] args)
		{
			if (!args.Contains(StorageDir))
			{
				throw new ArgumentException($"Please provide '{StorageDir}' as a command line argument. This is required to find 'movies.json' file.");
			}

			var storageDirIndex = Array.IndexOf(args, StorageDir) + 1;
			var arguments = new Arguments(storageDir: args[storageDirIndex]);

			return arguments;
		}
	}
}