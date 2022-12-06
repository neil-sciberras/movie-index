using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Movies.GraphQL.App
{
	public class AppSchema : Schema
	{
		public AppSchema(IServiceProvider provider) : base(provider)
		{
			Query = provider.GetRequiredService<AppGraphQuery>();
			//Mutation = provider.GetRequiredService<AppGraphMutation>();
		}
	}
}