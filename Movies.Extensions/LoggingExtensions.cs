using Movies.AppInfo;
using Serilog;

namespace Movies.Extensions
{
	public static class LoggingExtensions
	{
		public static LoggerConfiguration WithAppInfo(this LoggerConfiguration builder, IAppInfo appInfo)
			=> builder.Enrich.WithProperty("appName", appInfo.ShortName)
				.Enrich.WithProperty("serviceType", appInfo.ServiceType)
				.Enrich.WithProperty("environment", appInfo.Environment)
				.Enrich.WithProperty("appVersion", appInfo.Version)
				.Enrich.WithProperty("gitCommit", appInfo.GitCommit);
	}
}