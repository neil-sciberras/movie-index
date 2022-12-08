using Microsoft.Extensions.DependencyInjection;
using System;

namespace Movies.GraphQL.Schema
{
	public class AppSchema : global::GraphQL.Types.Schema
	{
		public AppSchema(IServiceProvider provider) : base(provider)
		{
			Query = provider.GetRequiredService<AppGraphQuery>();
			Mutation = provider.GetRequiredService<AppGraphMutation>();
		}
	}
}