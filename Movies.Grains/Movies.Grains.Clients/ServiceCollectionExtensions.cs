using Microsoft.Extensions.DependencyInjection;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Clients.Interfaces.Redis;
using Movies.Grains.Clients.Redis;

namespace Movies.Grains.Clients
{
	public static class ServiceCollectionExtensions
	{
		public static void AddGrainClients(this IServiceCollection services)
		{
			services
				.AddSingleton<IAllMoviesGrainClient, AllMoviesGrainClient>()
				.AddSingleton<IMovieSearchGrainClient, MovieSearchGrainClient>()
				.AddSingleton<IGenreFilterGrainClient, GenreFilterGrainClient>()
				.AddSingleton<ITopRatedMoviesGrainClient, TopRatedMoviesGrainClient>()
				.AddSingleton<IAddMovieGrainClient, AddMovieGrainClient>()
				.AddSingleton<IUpdateMovieGrainClient, UpdateMovieGrainClient>()
				.AddSingleton<IDeleteMovieGrainClient, DeleteMovieGrainClient>()
				.AddSingleton<IQueryClient, QueryClient>()
				.AddSingleton<IUpdateClient, UpdateClient>();
		}
	}
}