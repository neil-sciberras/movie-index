using Movies.Infrastructure.DataSource.Interfaces;
using System;
using System.Threading.Tasks;

namespace Movies.Server.RedisBootstrap
{
	public class RedisBootstrapper : IRedisBootstrapper
	{
		private readonly IMoviesReader _fileReader; 
		private readonly IMoviesReader _redisReader;
		private readonly IMoviesWriter _fileWriter;
		private readonly IMoviesWriter _redisWriter;

		public RedisBootstrapper(Func<MovieStorage, IMoviesReader> readerResolver, Func<MovieStorage, IMoviesWriter> writerResolver)
		{
			_fileReader = readerResolver(MovieStorage.File);
			_fileWriter = writerResolver(MovieStorage.File);
			_redisReader = readerResolver(MovieStorage.Redis);
			_redisWriter = writerResolver(MovieStorage.Redis);
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
