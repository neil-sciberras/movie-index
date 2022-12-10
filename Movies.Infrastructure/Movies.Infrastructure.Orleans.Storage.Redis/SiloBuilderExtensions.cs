using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;
using Orleans.Storage;

namespace Movies.Infrastructure.Orleans.Storage.Redis
{
	public static class SiloBuilderExtensions
	{
		public static ISiloBuilder AddRedisGrainStorage(this ISiloBuilder builder, string providerName)
		{
			return builder.ConfigureServices(services =>
			{
				services.AddRedisGrainStorage(providerName);
			});
		}

		private static void AddRedisGrainStorage(this IServiceCollection services, string providerName)
		{
			services
				.AddSingletonNamedService(name: providerName, factory: RedisGrainStorageFactory.Create)
				.AddSingletonNamedService(name: providerName, factory: (serviceProvider, name) =>
					(ILifecycleParticipant<ISiloLifecycle>)serviceProvider.GetRequiredServiceByName<IGrainStorage>(name));
		}
	}
}
