using Microsoft.Extensions.DependencyInjection;
using Movies.Infrastructure.DataSource.Interfaces;

namespace Movies.Infrastructure.File
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureFileReadingAndWriting(this IServiceCollection services, FileOptions fileOptions)
		{
			services.AddSingleton(fileOptions);
			services.AddScoped<IFileRepository, FileRepository>();
			services.AddSingleton<FileMoviesReader>();
			services.AddSingleton<FileMoviesWriter>();

			return services;
		}
	}
}
