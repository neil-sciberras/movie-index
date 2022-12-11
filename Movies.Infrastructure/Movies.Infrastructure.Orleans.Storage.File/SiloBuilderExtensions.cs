using Microsoft.Extensions.DependencyInjection;
using Movies.Infrastructure.File;
using Orleans;
using Orleans.Hosting;
using Orleans.Runtime;
using Orleans.Storage;
using System;

namespace Movies.Infrastructure.Orleans.Storage.File
{
	/// <summary>
	/// <see href="https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage"/>
	/// </summary>
	public static class SiloBuilderExtensions
	{
		public static ISiloBuilder AddFileGrainStorage(this ISiloBuilder builder, string providerName, Action<FileOptions> options)
		{
			return builder.ConfigureServices(services =>
				{
					services.AddFileGrainStorage(providerName, options);
				});
		}

		private static IServiceCollection AddFileGrainStorage(this IServiceCollection services, string providerName, Action<FileOptions> options)
		{
			services.AddOptions<FileOptions>(providerName).Configure(options);

			return services
				.AddSingletonNamedService(name: providerName, factory: FileGrainStorageFactory.Create)
				.AddSingletonNamedService(name: providerName, factory: (serviceProvider, name) =>
					(ILifecycleParticipant<ISiloLifecycle>)serviceProvider.GetRequiredServiceByName<IGrainStorage>(name));
		}
	}
}
