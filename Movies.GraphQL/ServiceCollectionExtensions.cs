using GraphQL.Server;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Movies.GraphQL.Schema;
using Movies.GraphQL.Types;

namespace Movies.GraphQL
{
	public static class ServiceCollectionExtensions
	{
		public static void AddAppGraphQL(this IServiceCollection services)
		{
			services.AddGraphQL(options =>
				{
					options.EnableMetrics = true;
					options.ExposeExceptions = true;
				})
				.AddNewtonsoftJson();

			services.AddSingleton<ISchema, AppSchema>();
			services.AddSingleton<AppGraphQuery>();
			services.AddSingleton<AppGraphMutation>();

			services.AddSingleton<SampleDataGraphType>();
			services.AddSingleton<GenreGraphType>();
			services.AddSingleton<MovieGraphType>();
		}
	}
}
