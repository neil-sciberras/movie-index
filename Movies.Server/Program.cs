using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.AppInfo;
using Movies.Extensions;
using Orleans;
using Orleans.Hosting;
using Serilog;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using Movies.Infrastructure.Orleans.Filters;
using Movies.Server.ApiHostedService;
using Movies.Infrastructure.Orleans.Silo;
using Movies.Grains.MovieList;
using Movies.Grains.DataQueries.FilteredMovies;
using Movies.Grains.DataQueries.Supervisors;
using Movies.Server.CommandLineArgs;

namespace Movies.Server
{
	public class Program
	{
		public static Task Main(string[] args)
		{
			var hostBuilder = new HostBuilder();
			var commandLineArguments = CommandLineArgHelper.ParseArguments(args);
			
			IAppInfo appInfo = null;

			hostBuilder
				.ConfigureHostConfiguration(cfg =>
				{
					cfg.SetBasePath(Directory.GetCurrentDirectory())
						.AddEnvironmentVariables("ASPNETCORE_")
						.AddCommandLine(args);
				})
				.ConfigureServices((ctx, services) =>
				{
					appInfo = new AppInfo.AppInfo(ctx.Configuration);
					Console.Title = $"{appInfo.Name} - {appInfo.Environment}";

					services.AddSingleton(appInfo);
					services.Configure<ApiHostedServiceOptions>(options =>
					{
						options.Port = GetAvailablePort(6600, 6699);
					});

					services.Configure<ConsoleLifetimeOptions>(options =>
					{
						options.SuppressStatusMessages = true;
					});
				})
				.ConfigureAppConfiguration((ctx, cfg) =>
				{
					var shortEnvName = AppInfo.AppInfo.MapEnvironmentName(ctx.HostingEnvironment.EnvironmentName);
					
					cfg.AddJsonFile("appsettings.json")
						.AddJsonFile($"appsettings.{shortEnvName}.json", optional: true)
						.AddJsonFile("app-info.json")
						.AddEnvironmentVariables()
						.AddCommandLine(args);

					appInfo = new AppInfo.AppInfo(cfg.Build());

					if (!appInfo.IsDockerized) return;

					cfg.Sources.Clear();

					cfg.AddJsonFile("appsettings.json")
						.AddJsonFile($"appsettings.{shortEnvName}.json", optional: true)
						.AddJsonFile("app-info.json")
						.AddEnvironmentVariables()
						.AddCommandLine(args);
				})
				.UseSerilog((ctx, loggerConfig) =>
				{
					loggerConfig
						.Enrich.FromLogContext()
						.ReadFrom.Configuration(ctx.Configuration)
						.Enrich.WithMachineName()
						.Enrich.WithDemystifiedStackTraces()
						.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext:l}] {Message:lj}{NewLine}{Exception}");

					loggerConfig.WithAppInfo(appInfo);
				})
				.UseOrleans((ctx, builder) =>
				{
					builder
						.ConfigureSilo(new AppSiloBuilderContext
						{
							AppInfo = appInfo,
							HostBuilderContext = ctx,
							SiloOptions = new AppSiloOptions
							{
								SiloPort = GetAvailablePort(11111, 12000),
								GatewayPort = 30001,
								StorageFileName = ctx.Configuration.GetSection("moviesFileName").Value,
								StorageFileDirectory = commandLineArguments.StorageDir
							}
						})
						.ConfigureApplicationParts(parts => parts
							.AddApplicationPart(typeof(AllMoviesGrain).Assembly).WithReferences()
							.AddApplicationPart(typeof(TopRatedMoviesGrain).Assembly).WithReferences()
							.AddApplicationPart(typeof(TopRatedMoviesSupervisorGrain).Assembly).WithReferences()
						)
						.AddIncomingGrainCallFilter<LoggingIncomingCallFilter>()
						.AddOutgoingGrainCallFilter<LoggingOutgoingCallFilter>();

				})
				.ConfigureServices((ctx, services) =>
				{
					services.AddHostedService<ApiHostedService.ApiHostedService>();
				})
				;

			return hostBuilder.RunConsoleAsync();
		}

		private static int GetAvailablePort(int start, int end)
		{
			for (var port = start; port < end; ++port)
			{
				var listener = TcpListener.Create(port);
				listener.ExclusiveAddressUse = true;
				try
				{
					listener.Start();
					return port;
				}
				catch (SocketException)
				{
				}
				finally
				{
					listener.Stop();
				}
			}

			throw new InvalidOperationException();
		}
	}
}