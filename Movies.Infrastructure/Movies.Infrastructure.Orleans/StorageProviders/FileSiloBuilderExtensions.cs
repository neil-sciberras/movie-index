using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;
using Orleans.Storage;
using System;

namespace Movies.Infrastructure.Orleans.StorageProviders
{
	/// <summary>
	/// <see href="https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage"/>
	/// </summary>
	public static class FileSiloBuilderExtensions
	{
		public static ISiloBuilder AddFileGrainStorage(this ISiloBuilder builder, string providerName, Action<FileStorageOptions> options)
		{
			return builder.ConfigureServices(services =>
				{
					services.AddFileGrainStorage(providerName, options);
				});
		}

		private static IServiceCollection AddFileGrainStorage(this IServiceCollection services, string providerName, Action<FileStorageOptions> options)
		{
			services.AddOptions<FileStorageOptions>(providerName).Configure(options);

			return services
				.AddSingletonNamedService(name: providerName, factory: FileGrainStorageFactory.Create)
				.AddSingletonNamedService(name: providerName, factory: (serviceProvider, name) =>
					(ILifecycleParticipant<ISiloLifecycle>)serviceProvider.GetRequiredServiceByName<IGrainStorage>(name));
		}
	}
}
