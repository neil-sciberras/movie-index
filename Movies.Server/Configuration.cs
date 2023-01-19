using Microsoft.Extensions.Configuration;

namespace Movies.Server
{
	public class Configuration
	{
		public Configuration(IConfiguration config)
		{
			FileStore = new FileStore(
				moviesFileName: config.GetSection("fileStore").GetSection("moviesFileName").Value,
				moviesFileDirectory: config.GetSection("fileStore").GetSection("moviesFileDirectory").Value);
			Redis = new Redis(
				endpoint: config.GetSection("redis").GetSection("endpoint").Value,
				password: config.GetSection("redis").GetSection("password").Value);
		}

		public FileStore FileStore { get; }
		public Redis Redis { get; }
	}

	public class FileStore
	{
		public FileStore(string moviesFileName, string moviesFileDirectory)
		{
			MoviesFileName = moviesFileName;
			MoviesFileDirectory = moviesFileDirectory;
		}

		public string MoviesFileName { get; }
		public string MoviesFileDirectory { get; }
	}

	public class Redis
	{
		public Redis(string endpoint, string password)
		{
			Endpoint = endpoint;
			Password = password;
		}

		public string Endpoint { get; }
		public string Password { get; }
	}
}