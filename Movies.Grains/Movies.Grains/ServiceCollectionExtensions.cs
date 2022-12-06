using Microsoft.Extensions.DependencyInjection;

namespace Movies.Grains
{
	public static class ServiceCollectionExtensions
	{

		public static IServiceCollection AddAppGrains(this IServiceCollection services)
		{
			return services;
		}

		public static IServiceCollection AddAppHotsGrains(this IServiceCollection services)
		{
			return services;
		}

		public static IServiceCollection AddAppLoLGrains(this IServiceCollection services)
		{
			return services;
		}
	}
}
