using Movies.Contracts.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
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
			var ids = GetAllIds();
			var db = _redisConnection.GetDatabase();
			var movies = new List<Movie>();

			foreach (var id in ids)
			{
				movies.Add(await ReadMovieAsync(id.ToString(), db));
			}

			return movies;
		}

		public async Task<Movie> ReadMovieAsync(int id)
		{
			var db = _redisConnection.GetDatabase();
			return await ReadMovieAsync(id.ToString(), db);
		}

		public IEnumerable<int> GetAllIds()
		{
			var server = _redisConnection.GetServer(hostAndPort: _redisSettings.Endpoint);
			var keys = server.Keys();

			return keys.Select(k => int.Parse(k));
		}

		private static async Task<Movie> ReadMovieAsync(string id, IDatabaseAsync database)
		{
			var serializedMovie = await database.StringGetAsync(id);

			return serializedMovie == RedisValue.Null 
				? null 
				: JsonConvert.DeserializeObject<Movie>(serializedMovie);
		}
	}
}
