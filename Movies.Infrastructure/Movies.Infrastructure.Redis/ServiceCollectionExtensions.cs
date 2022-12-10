using Microsoft.Extensions.DependencyInjection;
using Movies.AppInfo;
using StackExchange.Redis;

namespace Movies.Infrastructure.Redis
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureRedis(this IServiceCollection services, RedisSettings redisSettings, IAppInfo appInfo)
		{
			var configurationOptions = ConfigurationOptions.Parse(redisSettings.ConfigurationString);
			configurationOptions.ClientName = appInfo.Name;

			var multiplexer = ConnectionMultiplexer.Connect(configurationOptions);

			services.AddSingleton(redisSettings);
			services.AddSingleton<IConnectionMultiplexer>(multiplexer);
			services.AddSingleton<IRedisWriter, RedisWriter>();
			services.AddSingleton<IRedisReader, RedisReader>();

			return services;
		}
	}
}
