using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Movies.AppInfo;
using Orleans;
using System;

namespace Movies.Infrastructure.Orleans.Client
{
	public class ClientBuilderContext
	{
		public string ClusterId => AppInfo.ClusterId;
		public string ServiceId => AppInfo.Name;

		public ILogger Logger { get; set; }
		public IAppInfo AppInfo { get; set; }
		public IConfiguration Configuration { get; set; }
		public Action<IClientBuilder> ConfigureClientBuilder { get; set; }
	}
}