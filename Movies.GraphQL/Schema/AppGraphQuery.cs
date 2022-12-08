using GraphQL;
using GraphQL.Types;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
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
			IMovieProxyGrainClient movieProxyGrainClient,
			IAllMoviesGrainClient allMoviesGrainClient,
			IGenreFilterGrainClient genreFilterGrainClient,
			ITopRatedMoviesGrainClient topRatedMoviesGrainClient)
		{
			Name = "AppQueries";

			Field<MovieGraphType, Movie>(name: "movie")
				.Description("A movie")
				.Argument<IntGraphType>(Id, "Unique movie Id")
				.ResolveAsync(async context =>
				{
					var id = context.GetArgument<int>(Id);
					return await movieProxyGrainClient.GetMovieAsync(id);
				});

			Field<ListGraphType<MovieGraphType>, IEnumerable<Movie>>(name: "moviesList")
				.Description("List of all movies")
				.ResolveAsync(async _ => await allMoviesGrainClient.GetListAsync());

			Field<ListGraphType<MovieGraphType>, IEnumerable<Movie>>(name: "moviesWithGenre")
				.Description("List of movies with a given genre")
				.Argument<GenreGraphType>(Genre, "The genre to filter by")
				.ResolveAsync(async context =>
				{
					var genre = context.GetArgument<Genre>(Genre);
					return await genreFilterGrainClient.GetMoviesAsync((int)genre);
				});

			Field<ListGraphType<MovieGraphType>, IEnumerable<Movie>>(name: "topRatedMovies")
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
