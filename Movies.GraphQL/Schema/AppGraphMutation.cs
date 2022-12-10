using GraphQL;
using GraphQL.Types;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.Grains.Clients.Interfaces.Redis;
using Movies.GraphQL.Types;
using Movies.GraphQL.Types.Input;

namespace Movies.GraphQL.Schema
{
	public class AppGraphMutation : ObjectGraphType
	{
		private const string NewMovie = "NewMovie";
		private const string MovieUpdate = "MovieUpdate";
		private const string MovieId = "MovieId";

		public AppGraphMutation(IUpdateClient updateClient)
		{
			Name = "MovieMutations";

			Field<MovieType, Movie>(name: "addMovie")
				.Description("Add a new movie to the list")
				.Argument<NewMovieInputType>(NewMovie, "The new movie")
				.ResolveAsync(async context =>
				{
					var newMovie = context.GetArgument<NewMovie>(NewMovie);
					return await updateClient.AddMovieAsync(newMovie);
				});

			Field<MovieType, Movie>(name: "updateMovie")
				.Description("Update a movie in the list")
				.Argument<MovieUpdateInputType>(MovieUpdate, "The new movie")
				.ResolveAsync(async context =>
				{
					var movie = context.GetArgument<Movie>(MovieUpdate);
					return await updateClient.UpdateMovieAsync(movie);
				});

			Field<MovieType, Movie>(name: "deleteMovie")
				.Description("Delete a movie from the list")
				.Argument<IntGraphType>(MovieId, "The movie's Id")
				.ResolveAsync(async context =>
				{
					var movieId = context.GetArgument<int>(MovieId);
					return await updateClient.DeleteMovieAsync(movieId);
				});
		}
	}
}