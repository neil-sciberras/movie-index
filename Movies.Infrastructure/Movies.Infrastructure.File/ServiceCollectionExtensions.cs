using Microsoft.Extensions.DependencyInjection;

namespace Movies.Infrastructure.File
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection ConfigureFileReadingAndWriting(this IServiceCollection services, FileStorageOptions fileStorageOptions)
		{
			services.AddSingleton(fileStorageOptions);
			services.AddSingleton<IFileReader, FileReader>();
			services.AddSingleton<IFileWriter, FileWriter>();

			return services;
		}
	}
}
