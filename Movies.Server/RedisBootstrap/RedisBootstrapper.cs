using Movies.Infrastructure.DataSource.Interfaces;
using Movies.Infrastructure.Redis;
using System.Threading.Tasks;

namespace Movies.Server.RedisBootstrap
{
	public class RedisBootstrapper : IRedisBootstrapper
	{
		private readonly IMoviesReader _fileReader;
		private readonly IMoviesWriter _fileWriter;
		private readonly IRedisReader _redisReader;
		private readonly IRedisWriter _redisWriter;

		public RedisBootstrapper(IMoviesReader fileReader, IMoviesWriter fileWriter, IRedisReader redisReader, IRedisWriter redisWriter)
		{
			_fileReader = fileReader;
			_fileWriter = fileWriter;
			_redisReader = redisReader;
			_redisWriter = redisWriter;
		}

		public async Task PreLoadRedisAsync()
		{
			var moviesFromFile = await _fileReader.ReadMoviesAsync();
			await _redisWriter.WriteMoviesAsync(moviesFromFile);
		}

		public async Task SaveRedisDataAsync()
		{
			var moviesFromRedis = await _redisReader.ReadMoviesAsync();
			await _fileWriter.WriteMoviesAsync(moviesFromRedis);
		}
	}
}
