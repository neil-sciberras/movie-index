using Microsoft.Extensions.DependencyInjection;
using Movies.AppInfo;
using Movies.Infrastructure.DataSource.Interfaces;
using Movies.Infrastructure.File;
using Movies.Infrastructure.Redis;
using System;

namespace Movies.Server.RedisBootstrap
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureDatabase(this IServiceCollection services, Configuration configuration, IAppInfo appInfo)
		{
			return services
				.ConfigureFile(configuration)
				.ConfigureRedis(configuration, appInfo)
				.ConfigureServiceResolvers()
				.AddSingleton<IRedisBootstrapper, RedisBootstrapper>();
		}

		private static IServiceCollection ConfigureFile(this IServiceCollection services, Configuration configuration)
		{
			var fileOptions = new FileOptions
			{
				RootDirectory = configuration.FileStore.MoviesFileDirectory,
				FileName = configuration.FileStore.MoviesFileName
			};

			return services.ConfigureFileReadingAndWriting(fileOptions);
		}

		private static IServiceCollection ConfigureRedis(this IServiceCollection services, Configuration configuration, IAppInfo appInfo)
		{
			var redisSettings = new RedisSettings(
				endpoint: configuration.Redis.Endpoint,
				password: configuration.Redis.Password);

			return services.ConfigureRedis(redisSettings, appInfo);
		}

		private static IServiceCollection ConfigureServiceResolvers(this IServiceCollection services)
		{
			services.AddSingleton<Func<MovieStorage, IMoviesWriter>>(implementationFactory: provider => movieStorage =>
			{
				switch (movieStorage)
				{
					case MovieStorage.File: return provider.GetService<FileMoviesWriter>();
					case MovieStorage.Redis: return provider.GetService<RedisWriter>();
					default: return null;
				}
			});

			services.AddSingleton<Func<MovieStorage, IMoviesReader>>(implementationFactory: provider => movieStorage =>
			{
				switch (movieStorage)
				{
					case MovieStorage.File: return provider.GetService<FileMoviesReader>();
					case MovieStorage.Redis: return provider.GetService<RedisReader>();
					default: return null;
				}
			});

			return services;
		}
	}
}
