namespace Movies.Infrastructure.Redis
{
	public class RedisSettings
	{
		private readonly string _password;

		public RedisSettings(string endpoint, string password)
		{
			Endpoint = endpoint;
			_password = password;
		}

		public string ConfigurationString => string.Join(",", Endpoint, _password);
		public string Endpoint { get; }
	}
}
