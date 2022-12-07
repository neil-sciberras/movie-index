using Microsoft.Extensions.DependencyInjection;
using Movies.Grains.Clients.Interfaces;

namespace Movies.Grains.Clients
{
	public static class ServiceCollectionExtensions
	{
		public static void AddAppClients(this IServiceCollection services)
		{
			services.AddSingleton<ISampleGrainClient, SampleGrainClient>();
		}
	}
}