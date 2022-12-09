using GraphQL.Types;
using Movies.Contracts.Models;
using System.Collections.Generic;

namespace Movies.GraphQL.Types.Input
{
	public class NewMovieInputType : InputObjectGraphType<NewMovie>
	{
		public NewMovieInputType()
		{
			Name = "NewMovie";
			Description = "A movie to be added in the database";

			Field(d => d.Key).Description("A key representing the movie");
			Field(d => d.Name).Description("The movie's name");
			Field(d => d.Description).Description("The movie's description");
			Field(d => d.Rate).Description("The movie's rating");
			Field(d => d.Length).Description("The movie's length");
			Field(d => d.Image).Description("The movie's image file name");

			Field<ListGraphType<GenreType>, IEnumerable<Genre>>("Genres")
				.Resolve(ctx => ctx.Source.Genres)
				.Description("The genres of the movie");
		}
	}
}
