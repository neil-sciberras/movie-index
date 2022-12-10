using Movies.Contracts.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Redis
{
	public class RedisWriter : IRedisWriter
	{
		private readonly IConnectionMultiplexer _redisConnection;

		public RedisWriter(IConnectionMultiplexer redisConnection)
		{
			_redisConnection = redisConnection;
		}

		public async Task WriteMoviesAsync(ICollection<Movie> movies)
		{
			var db = _redisConnection.GetDatabase();
			var setTasks = movies.Select(m => WriteMovieAsync(id: m.Id.ToString(), movie: m, database: db));

			var counter = 0;

			foreach (var setTask in setTasks)
			{
				var success = await setTask;

				if (success)
				{
					counter++;
				}
			}

			if (counter != movies.Count)
			{
				throw new RedisLoadingException($"{movies.Count} were supposed to be loaded into Redis, but {counter} were successfully loaded");
			}
		}

		public async Task<bool> WriteMovieAsync(string id, object movie)
		{
			var db = _redisConnection.GetDatabase();
			return await WriteMovieAsync(id, movie, db);
		}

		public async Task DeleteAsync(string id)
		{
			var db = _redisConnection.GetDatabase();
			await db.KeyDeleteAsync(id);
		}

		private static async Task<bool> WriteMovieAsync(string id, object movie, IDatabaseAsync database)
		{
			return await database.StringSetAsync(key: id, value: JsonConvert.SerializeObject(movie));
		}
	}
}
