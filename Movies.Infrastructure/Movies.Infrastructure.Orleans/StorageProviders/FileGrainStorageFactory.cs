using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Configuration.Overrides;
using Orleans.Runtime;
using Orleans.Storage;
using System;

namespace Movies.Infrastructure.Orleans.StorageProviders
{
	/// <summary>
	/// <see href="https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage"/>
	/// </summary>
	public class FileGrainStorageFactory
	{
		public static IGrainStorage Create(IServiceProvider serviceProvider, string name)
		{
			var optionsSnapshot = serviceProvider.GetRequiredService<IOptionsSnapshot<FileStorageOptions>>();

			return ActivatorUtilities.CreateInstance<FileGrainStorage>(
				provider: serviceProvider,
				name,
				optionsSnapshot.Get(name),
				serviceProvider.GetProviderClusterOptions(name).Value);
		}
	}
}
