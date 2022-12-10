using Movies.Infrastructure.File;
using Movies.Infrastructure.Redis;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Server.RedisBootstrap
{
	public class RedisBootstrapper : IRedisBootstrapper
	{
		private readonly IFileReader _fileReader;
		private readonly IFileWriter _fileWriter;
		private readonly IRedisReader _redisReader;
		private readonly IRedisWriter _redisWriter;

		public RedisBootstrapper(IFileReader fileReader, IFileWriter fileWriter, IRedisReader redisReader, IRedisWriter redisWriter)
		{
			_fileReader = fileReader;
			_fileWriter = fileWriter;
			_redisReader = redisReader;
			_redisWriter = redisWriter;
		}

		public async Task PreLoadRedisAsync()
		{
			var moviesFromFile = await _fileReader.ReadMoviesAsync();
			await _redisWriter.WriteMoviesAsync(moviesFromFile.MovieList.ToList());
		}

		public async Task SaveRedisDataAsync()
		{
			var moviesFromRedis = await _redisReader.ReadMoviesAsync();
			var moviesObj = new Infrastructure.File.Movies { MovieList = moviesFromRedis };
			
			await _fileWriter.WriteMoviesAsync(moviesObj);
		}
	}
}
