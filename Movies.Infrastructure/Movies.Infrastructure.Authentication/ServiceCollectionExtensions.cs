using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Movies.Infrastructure.Authentication
{
	public static class ServiceCollectionExtensions
	{
		public static void AddCustomAuthentication(this IServiceCollection services)
		{
			services
				.AddAuthentication(CustomAuthenticationHandler.SecretKey)
				.AddScheme<JwtBearerOptions, CustomAuthenticationHandler>(CustomAuthenticationHandler.SecretKey, null);

			services.AddAuthorization(auth =>
			{
				auth.AddPolicy(CustomAuthenticationHandler.SecretKey, builder =>
				{
					builder
						.AddAuthenticationSchemes(CustomAuthenticationHandler.SecretKey)
						.RequireAuthenticatedUser();
				});
			});
		}
	}
}