using Microsoft.Extensions.DependencyInjection;
using Movies.Grains.Clients.Interfaces.Redis;
using Movies.Grains.Clients.Redis;

namespace Movies.Grains.Clients
{
	public static class ServiceCollectionExtensions
	{
		public static void AddGrainClients(this IServiceCollection services)
		{
			services
				.AddSingleton<IQueryClient, QueryClient>()
				.AddSingleton<IUpdateClient, UpdateClient>();
		}
	}
}