using GraphQL;
using GraphQL.Types;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Clients.Interfaces.Redis;
using Movies.GraphQL.Types;
using System.Collections.Generic;

namespace Movies.GraphQL.Schema
{
	public class AppGraphQuery : ObjectGraphType
	{
		private const string Id = "Id";
		private const string Genre = "Genre";
		private const string Amount = "Amount";

		public AppGraphQuery(
			IMovieSearchGrainClient movieSearchGrain,
			IAllMoviesGrainClient allMoviesGrainClient,
			IGenreFilterGrainClient genreFilterGrainClient,
			ITopRatedMoviesGrainClient topRatedMoviesGrainClient,
			IQueryClient queryClient)
		{
			Name = "MovieQueries";

			Field<MovieType, Movie>(name: "movieRedis")
				.Description("A movie with the given Id (Redis)")
				.Argument<IntGraphType>(Id, "Unique movie Id")
				.ResolveAsync(async context =>
				{
					var id = context.GetArgument<int>(Id);
					return await queryClient.GetMovieAsync(id);
				});
			
			Field<MovieType, Movie>(name: "movie")
				.Description("A movie with the given Id")
				.Argument<IntGraphType>(Id, "Unique movie Id")
				.ResolveAsync(async context =>
				{
					var id = context.GetArgument<int>(Id);
					return await movieSearchGrain.GetMovie(id);
				});

			Field<ListGraphType<MovieType>, IEnumerable<Movie>>(name: "moviesListRedis")
				.Description("List of all movies (Redis)")
				.ResolveAsync(async _ => await queryClient.GetAllMoviesAsync());

			Field<ListGraphType<MovieType>, IEnumerable<Movie>>(name: "moviesList")
				.Description("List of all movies")
				.ResolveAsync(async _ => await allMoviesGrainClient.GetListAsync());

			Field<ListGraphType<MovieType>, IEnumerable<Movie>>(name: "moviesWithGenreRedis")
				.Description("List of movies with a given genre (Redis)")
				.Argument<GenreType>(Genre, "The genre to filter by")
				.ResolveAsync(async context =>
				{
					var genre = context.GetArgument<Genre>(Genre);
					return await queryClient.GetMoviesAsync(genre);
				});
			
			Field<ListGraphType<MovieType>, IEnumerable<Movie>>(name: "moviesWithGenre")
				.Description("List of movies with a given genre")
				.Argument<GenreType>(Genre, "The genre to filter by")
				.ResolveAsync(async context =>
				{
					var genre = context.GetArgument<Genre>(Genre);
					return await genreFilterGrainClient.GetMoviesAsync((int)genre);
				});

			Field<ListGraphType<MovieType>, IEnumerable<Movie>>(name: "topRatedMoviesRedis")
				.Description("Top rated movies (Redis)")
				.Argument<IntGraphType>(Amount, "The amount of movies to return from top of the list")
				.ResolveAsync(async context =>
				{
					var amount = context.GetArgument<int>(Amount);
					return await queryClient.GetTopRatedMoviesAsync(amount);
				});
			
			Field<ListGraphType<MovieType>, IEnumerable<Movie>>(name: "topRatedMovies")
				.Description("Top rated movies")
				.Argument<IntGraphType>(Amount, "The amount of movies to return from top of the list")
				.ResolveAsync(async context =>
				{
					var amount = context.GetArgument<int>(Amount);
					return await topRatedMoviesGrainClient.GetTopRatedMoviesAsync(amount);
				});
		}
	}
}
