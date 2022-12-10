using Microsoft.Extensions.DependencyInjection;
using Movies.Infrastructure.Redis;
using Orleans.Configuration.Overrides;
using Orleans.Storage;
using System;

namespace Movies.Infrastructure.Orleans.Storage.Redis
{
	public class RedisGrainStorageFactory
	{
		public static IGrainStorage Create(IServiceProvider serviceProvider, string name)
		{
			var redisReader = serviceProvider.GetRequiredService<IRedisReader>();
			var redisWriter = serviceProvider.GetRequiredService<IRedisWriter>();

			return ActivatorUtilities.CreateInstance<RedisGrainStorage>(provider: serviceProvider,
				redisReader, redisWriter, name, serviceProvider.GetProviderClusterOptions(name).Value);
		}
	}
}
