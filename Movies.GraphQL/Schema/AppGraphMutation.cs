using GraphQL;
using GraphQL.Types;
using Movies.Contracts.Models;
using Movies.Grains.Clients.Interfaces;
using Movies.GraphQL.Types;
using Movies.GraphQL.Types.Input;

namespace Movies.GraphQL.Schema
{
	public class AppGraphMutation : ObjectGraphType
	{
		private const string NewMovie = "NewMovie";
		private const string MovieUpdate = "MovieUpdate";

		public AppGraphMutation(IAddMovieGrainClient addMovieGrainClient, IUpdateMovieGrainClient updateMovieGrainClient)
		{
			Name = "MovieMutations";

			Field<MovieType, Movie>(name: "addMovie")
				.Description("Add a new movie to the list")
				.Argument<NewMovieInputType>(NewMovie, "The new movie")
				.ResolveAsync(async context =>
				{
					var newMovie = context.GetArgument<NewMovie>(NewMovie);
					return await addMovieGrainClient.AddMovieAsync(newMovie);
				});

			Field<MovieType, Movie>(name: "updateMovie")
				.Description("Update a movie in the list")
				.Argument<MovieUpdateInputType>(MovieUpdate, "The new movie")
				.ResolveAsync(async context =>
				{
					var movie = context.GetArgument<Movie>(MovieUpdate);
					return await updateMovieGrainClient.UpdateMovieAsync(movie);
				});
		}
	}
}