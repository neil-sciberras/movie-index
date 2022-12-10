using Microsoft.Extensions.DependencyInjection;
using Movies.AppInfo;
using Movies.Infrastructure.File;
using Movies.Infrastructure.Redis;

namespace Movies.Server.RedisBootstrap
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, Configuration configuration, IAppInfo appInfo)
		{
			return services
				.ConfigureFile(configuration)
				.ConfigureRedis(configuration, appInfo)
				.AddSingleton<IRedisBootstrapper, RedisBootstrapper>();
		}

		private static IServiceCollection ConfigureFile(this IServiceCollection services, Configuration configuration)
		{
			var fileOptions = new FileStorageOptions(rootDirectory: configuration.FileStore.MoviesFileDirectory, fileName: configuration.FileStore.MoviesFileName);

			return services.ConfigureFileReadingAndWriting(fileOptions);
		}

		private static IServiceCollection ConfigureRedis(this IServiceCollection services, Configuration configuration, IAppInfo appInfo)
		{
			var redisSettings = new RedisSettings(
				endpoint: configuration.Redis.Endpoint,
				password: configuration.Redis.Password);

			return services.ConfigureRedis(redisSettings, appInfo);
		}
	}
}
