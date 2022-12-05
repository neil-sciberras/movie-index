using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.AppInfo
{
	// TODO: Move to a more appropriate project
	public class AppInfo : IAppInfo
	{
		public string Name { get; set; }
		public string ShortName { get; }
		public string ClusterId { get; set; }
		public string Environment { get; set; }
		public string GitCommit { get; set; }
		public string Version { get; set; }
		public bool IsDockerized { get; set; }
		public string ServiceType { get; set; }

		private static readonly Dictionary<string, string> EnvironmentMapping = new Dictionary<string, string>
		{
			["Development"] = "dev",
			["Staging"] = "staging",
			["Production"] = "prod",
		};

		public AppInfo()
		{
		}

		/// <summary>
		/// Resolve from <see cref="IConfiguration"/>.
		/// </summary>
		/// <param name="config"></param>
		public AppInfo(IConfiguration config)
		{
			Name = config.GetValue("appName", "app");
			Version = config.GetValue("version", "local");
			GitCommit = config.GetValue("gitCommit", "-");
			Environment = config.GetValue<string>("ASPNETCORE_ENVIRONMENT");
			IsDockerized = config.GetValue<bool>("DOCKER");
			ServiceType = config.GetValue("serviceType", "dotnet");
			ShortName = Name.Split('/').Last();

			if (string.IsNullOrEmpty(Environment))
				throw new InvalidOperationException("Environment is not set. Please specify the environment via 'ASPNETCORE_ENVIRONMENT'");

			ClusterId = $"{Name}-{Version}";

			Environment = MapEnvironmentName(Environment);
		}

		public static string MapEnvironmentName(string environment)
		{
			if (environment == null) throw new ArgumentNullException(nameof(environment));

			EnvironmentMapping.TryGetValue(environment, out var env);
			return env;
		}
	}
}
