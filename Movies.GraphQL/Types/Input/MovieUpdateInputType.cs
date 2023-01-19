using GraphQL.Types;
using Movies.Contracts.Models;
using System.Collections.Generic;

namespace Movies.GraphQL.Types.Input
{
	public class MovieUpdateInputType : InputObjectGraphType<Movie>
	{
		public MovieUpdateInputType()
		{
			Name = "MovieUpdate";
			Description = "A movie to be updated in the database";

			Field(d => d.Id).Description("The movie's unique Id");
			Field(d => d.Key, nullable: true).Description("A key representing the movie");
			Field(d => d.Name, nullable: true).Description("The movie's name");
			Field(d => d.Description, nullable: true).Description("The movie's description");
			Field(d => d.Rate, nullable: true).Description("The movie's rating");
			Field(d => d.Length, nullable: true).Description("The movie's length");
			Field(d => d.Image, nullable: true).Description("The movie's image file name");

			Field<ListGraphType<GenreType>, IEnumerable<Genre>>("Genres")
				.Resolve(ctx => ctx.Source.Genres)
				.Description("The genres of the movie");
		}
	}
}
