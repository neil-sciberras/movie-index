using Microsoft.Extensions.DependencyInjection;

namespace Movies.GrainClients
{
	public static class ServiceCollectionExtensions
	{
		public static void AddAppClients(this IServiceCollection services)
		{
			services.AddSingleton<ISampleGrainClient, SampleGrainClient>();
		}
	}
}