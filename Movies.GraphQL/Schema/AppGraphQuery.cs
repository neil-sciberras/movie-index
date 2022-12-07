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
		public AppGraphQuery(
			ISampleGrainClient sampleClient, 
			IMovieProxyGrainClient movieProxyGrainClient, 
			ITopRatedMoviesGrainClient topRatedMoviesGrainClient)
		{
			Name = "AppQueries";

			Field<MovieGraphType, Movie>(name: "movie")
				.Description("A movie")
				.Argument<IntGraphType>("Id", "Unique movie Id")
				.ResolveAsync(async context =>
				{
					var id = context.GetArgument<int>("Id");
					return await movieProxyGrainClient.GetMovieAsync(id);
				});

			Field<ListGraphType<MovieGraphType>, IEnumerable<Movie>>(name: "topRatedMovies")
				.Description("Top rated movies")
				.Argument<IntGraphType>("Amount", "The amount of movies to return from top of the list")
				.ResolveAsync(async context =>
				{
					var amount = context.GetArgument<int>("Amount");
					return await topRatedMoviesGrainClient.GetTopRatedMoviesAsync(amount);
				});

			Field<SampleDataGraphType>("sample",
				arguments: new QueryArguments(new QueryArgument<StringGraphType>
				{
					Name = "id"
				}),
				resolve: ctx => sampleClient.Get(ctx.Arguments["id"].ToString())
			);
		}
	}
}
