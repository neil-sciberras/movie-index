using Movies.AppInfo;
using HostBuilderContext = Microsoft.Extensions.Hosting.HostBuilderContext;

namespace Movies.Server.Infrastructure.Silo
{
	public class AppSiloBuilderContext
	{
		public HostBuilderContext HostBuilderContext { get; set; }
		public IAppInfo AppInfo { get; set; }
		public AppSiloOptions SiloOptions { get; set; }
	}
}