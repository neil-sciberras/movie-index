using GraphQL.Types;
using Movies.Contracts.Models;
using System.Collections.Generic;

namespace Movies.GraphQL.Types
{
	public class MovieGraphType : ObjectGraphType<Movie>
	{
		public MovieGraphType()
		{
			Name = "Movie";
			Description = "A movie in the database";

			Field(d => d.Id).Description("The movie's unique Id");
			Field(d => d.Key).Description("A key representing the movie");
			Field(d => d.Name).Description("The movie's name");
			Field(d => d.Description).Description("The movie's description");
			Field(d => d.Rate).Description("The movie's rating");
			Field(d => d.Length).Description("The movie's length");
			Field(d => d.Image).Description("The movie's image file name");
			
			Field<ListGraphType<GenreGraphType>, IEnumerable<Genre>>("Genres")
				.Resolve(ctx => ctx.Source.Genres)
				.Description("The genres of the movie");
		}
	}
}
