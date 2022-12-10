using Microsoft.Extensions.DependencyInjection;
using Movies.Grains.Clients.Interfaces;

namespace Movies.Grains.Clients
{
	public static class ServiceCollectionExtensions
	{
		public static void AddGrainClients(this IServiceCollection services)
		{
			services
				.AddSingleton<IAllMoviesGrainClient, AllMoviesGrainClient>()
				.AddSingleton<IMovieSearchGrainClient, MovieSearchGrainClient>()
				.AddSingleton<IMovieGrainClient, MovieGrainClient>()
				.AddSingleton<IGenreFilterGrainClient, GenreFilterGrainClient>()
				.AddSingleton<ITopRatedMoviesGrainClient, TopRatedMoviesGrainClient>()
				.AddSingleton<IAddMovieGrainClient, AddMovieGrainClient>()
				.AddSingleton<IUpdateMovieGrainClient, UpdateMovieGrainClient>()
				.AddSingleton<IDeleteMovieGrainClient, DeleteMovieGrainClient>();
		}
	}
}