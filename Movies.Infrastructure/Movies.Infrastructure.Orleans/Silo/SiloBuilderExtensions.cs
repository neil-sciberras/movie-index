using Movies.Contracts.Grains;
using Movies.Extensions;
using Movies.Infrastructure.Orleans.Storage.File;
using Movies.Infrastructure.Orleans.Storage.Redis;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.IO;
using System.Net;

namespace Movies.Infrastructure.Orleans.Silo
{
	public static class SiloBuilderExtensions
	{
		private static StorageProviderType _defaultProviderType;

		public static ISiloBuilder ConfigureSilo(this ISiloBuilder siloBuilder, AppSiloBuilderContext context)
		{
			_defaultProviderType = context.SiloOptions.StorageProviderType ?? StorageProviderType.Memory;

			var appInfo = context.AppInfo;

			siloBuilder.AddMemoryGrainStorage(GrainStorageNames.MemoryStorage);

			siloBuilder.AddFileGrainStorage(GrainStorageNames.FileStorage, options =>
				{
					options.RootDirectory = context.SiloOptions.StorageFileDirectory;
					options.FileName = context.SiloOptions.StorageFileName;
				});

			siloBuilder.AddRedisGrainStorage(GrainStorageNames.RedisStorage);

			siloBuilder.Configure<ClusterOptions>(options =>
				{
					options.ClusterId = appInfo.ClusterId;
					options.ServiceId = appInfo.Name;
				});

			return siloBuilder
				.UseDevelopment(context)
				.UseDevelopmentClustering(context);
		}

		private static ISiloBuilder UseDevelopment(this ISiloBuilder siloHost, AppSiloBuilderContext context)
		{
			siloHost
				.ConfigureServices(services =>
				{
					//services.Configure<GrainCollectionOptions>(options => { options.CollectionAge = TimeSpan.FromMinutes(1.5); });
				});

			return siloHost;
		}

		private static ISiloBuilder UseDevelopmentClustering(this ISiloBuilder siloHost, AppSiloBuilderContext context)
		{
			var siloAddress = IPAddress.Loopback;
			var siloPort = context.SiloOptions.SiloPort;
			var gatewayPort = context.SiloOptions.GatewayPort;

			return siloHost
					.UseLocalhostClustering(siloPort: siloPort, gatewayPort: gatewayPort);
		}

		private static ISiloBuilder UseStorage(this ISiloBuilder siloBuilder, 
			string storeProviderName, 
			StorageProviderType? storageProvider = null, 
			string storeName = null,
			string storeDirectory = ".")
		{
			storeName = storeName.IfNullOrEmptyReturn(storeProviderName);
			storageProvider ??= _defaultProviderType;

			switch (storageProvider)
			{
				case StorageProviderType.Memory:
					siloBuilder.AddMemoryGrainStorage(storeProviderName);
					break;

				case StorageProviderType.File:
					siloBuilder.AddFileGrainStorage(storeProviderName, options =>
					{
						options.RootDirectory = storeDirectory;
						options.FileName = storeName;
					});
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(storageProvider), $"Storage provider '{storageProvider}' is not supported.");
			}

			return siloBuilder;
		}
	}
}