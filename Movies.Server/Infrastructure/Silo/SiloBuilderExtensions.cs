using Movies.AppInfo;
using Movies.Extensions;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;

namespace Movies.Server.Infrastructure.Silo
{
	public static class SiloBuilderExtensions
	{
		private static StorageProviderType _defaultProviderType;

		public static ISiloBuilder UseAppConfiguration(this ISiloBuilder siloHost, AppSiloBuilderContext context)
		{
			_defaultProviderType = context.SiloOptions.StorageProviderType ?? StorageProviderType.Memory;

			var appInfo = context.AppInfo;

			siloHost
				.AddMemoryGrainStorageAsDefault()
				.Configure<ClusterOptions>(options =>
				{
					options.ClusterId = appInfo.ClusterId;
					options.ServiceId = appInfo.Name;
				});

			siloHost.UseDevelopment(context);
			siloHost.UseDevelopmentClustering(context);

			return siloHost;
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

		public static ISiloBuilder UseStorage(this ISiloBuilder siloBuilder, string storeProviderName, IAppInfo appInfo, StorageProviderType? storageProvider = null, string storeName = null)
		{
			storeName = storeName.IfNullOrEmptyReturn(storeProviderName);
			storageProvider ??= _defaultProviderType;

			switch (storageProvider)
			{
				case StorageProviderType.Memory:
					siloBuilder.AddMemoryGrainStorage(storeProviderName);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(storageProvider), $"Storage provider '{storageProvider}' is not supported.");
			}

			return siloBuilder;
		}
	}
}