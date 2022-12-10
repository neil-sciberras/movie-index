using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.AppInfo;
using Movies.Infrastructure.File;
using Movies.Infrastructure.Redis;
using Movies.Server.CommandLineArgs;

namespace Movies.Server.DataSetup
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration, IAppInfo appInfo, Arguments commandLineArgs)
		{
			return services
				.ConfigureFile(configuration, commandLineArgs)
				.ConfigureRedis(configuration, appInfo, commandLineArgs)
				.AddSingleton<IDataHelper, DataHelper>();
		}

		private static IServiceCollection ConfigureFile(this IServiceCollection services, IConfiguration configuration, Arguments commandLineArgs)
		{
			var fileName = configuration.GetSection("moviesFileName").Value;
			var fileOptions = new FileStorageOptions(rootDirectory: commandLineArgs.StorageDir, fileName: fileName);

			return services.ConfigureFileReadingAndWriting(fileOptions);
		}

		private static IServiceCollection ConfigureRedis(this IServiceCollection services, IConfiguration configuration, IAppInfo appInfo, Arguments commandLineArgs)
		{
			var redisConfiguration = configuration.GetSection("redis");
			
			var redisSettings = new RedisSettings(
				endpoint: redisConfiguration.GetSection("endpoint").Value,
				password: redisConfiguration.GetSection("password").Value);

			return services.ConfigureRedis(redisSettings, appInfo);
		}
	}
}
