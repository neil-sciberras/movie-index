namespace Movies.AppInfo
{
	public interface IAppInfo
	{
		/// <summary>
		/// Get application name. e.g. '@odin/skeleton'.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the application short name. e.g. 'skeleton'.
		/// </summary>
		string ShortName { get; }

		string ClusterId { get; set; }

		/// <summary>
		/// Get environment. e.g. 'Development'. (based on ASPNET_ENVIRONMENT, which can be mapped).
		/// </summary>
		string Environment { get; }

		/// <summary>
		/// Get git short commit hash. e.g. 'b603d6'
		/// </summary>
		string GitCommit { get; }

		/// <summary>
		/// Get application version. e.g. '1.1.0-staging'
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Get whether the app is dockerized or not.
		/// </summary>
		bool IsDockerized { get; }

		/// <summary>
		/// Gets which service type is this app responsible of e.g. web, silo, etc...
		/// </summary>
		string ServiceType { get; set; }
	}
}
