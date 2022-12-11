using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Movies.AppInfo;
using Movies.Server.RedisBootstrap;
using Orleans;
using Serilog;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Movies.Server.ApiHostedService
{
	public class ApiHostedService : IHostedService
	{
		private readonly IAppInfo _appInfo;
		private readonly ILogger _logger;
		private readonly IWebHost _host;
		private readonly IRedisBootstrapper _redisBootstrapper;

		public ApiHostedService(
			IOptions<ApiHostedServiceOptions> options,
			IClusterClient client,
			IGrainFactory grainFactory,
			IConfiguration configuration,
			IAppInfo appInfo,
			ILogger<ApiHostedService> logger, 
			IRedisBootstrapper redisBootstrapper)
		{
			_appInfo = appInfo;
			_logger = logger;
			_redisBootstrapper = redisBootstrapper;

			logger.LogInformation("Initializing api {appName} ({version}) [{env}] on port {apiPort}...",
				appInfo.Name, appInfo.Version, appInfo.Environment, options.Value.Port);
			
			ConsoleTitleBuilder.Append(() => $"(Api port: {options.Value.Port} | pid: {Process.GetCurrentProcess().Id})");

			_host = WebHost.CreateDefaultBuilder()
				.UseSerilog()
				.UseConfiguration(configuration)
				.ConfigureAppConfiguration(cfg =>
				{
					cfg.Sources.Clear();
					cfg.AddConfiguration(configuration);
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(appInfo);
					services.AddSingleton(client);
					services.AddSingleton(grainFactory);
				})
				.UseStartup<ApiStartup>()
				.UseUrls($"http://*:{options.Value.Port}")
				.Build();
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Loading movies from file to Redis");

			await _redisBootstrapper.PreLoadRedisAsync();

			_logger.LogInformation("App started successfully {appName} ({version}) [{env}]", _appInfo.Name, _appInfo.Version, _appInfo.Environment);

			await _host.StartAsync(cancellationToken);
			
			ConsoleTitleBuilder.Append("- Api status: running 🚀");
		}

		public async Task StopAsync(CancellationToken cancellationToken)
		{
			_logger.LogInformation("Saving movies to file from Redis");

			await _redisBootstrapper.SaveRedisDataAsync();

			await _host.StopAsync(cancellationToken);
		}
	}
}
