using Movies.Contracts.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Redis
{
	public class RedisReader : IRedisReader
	{
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly RedisSettings _redisSettings;

		public RedisReader(IConnectionMultiplexer redisConnection, RedisSettings redisSettings)
		{
			_redisConnection = redisConnection;
			_redisSettings = redisSettings;
		}

		public async Task<IEnumerable<Movie>> ReadMoviesAsync()
		{
			var server = _redisConnection.GetServer(hostAndPort: _redisSettings.Endpoint);
			var keys = server.Keys();

			var db = _redisConnection.GetDatabase();
			var movies = new List<Movie>();

			foreach (var key in keys)
			{
				var serializedMovie = await db.StringGetAsync(key);
				var movie = JsonConvert.DeserializeObject<Movie>(serializedMovie);
				
				movies.Add(movie);
			}

			return movies;
		}
	}
}
