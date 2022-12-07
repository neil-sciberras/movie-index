using Microsoft.Extensions.DependencyInjection;
using Movies.Grains.Clients.Interfaces;

namespace Movies.Grains.Clients
{
	public static class ServiceCollectionExtensions
	{
		public static void AddGrainClients(this IServiceCollection services)
		{
			services
				.AddSingleton<ISampleGrainClient, SampleGrainClient>()
				.AddSingleton<IMovieGrainClient, MovieGrainClient>()
				.AddSingleton<IMovieProxyGrainClient, MovieProxyGrainClient>()
				.AddSingleton<ITopRatedMoviesGrainClient, TopRatedMoviesGrainClient>();
		}
	}
}