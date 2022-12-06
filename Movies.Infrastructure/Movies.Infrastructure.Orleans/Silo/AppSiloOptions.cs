using System.Diagnostics;

namespace Movies.Infrastructure.Orleans.Silo
{
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public class AppSiloOptions
	{
		private string DebuggerDisplay => $"GatewayPort: '{GatewayPort}', SiloPort: '{SiloPort}'";

		public int GatewayPort { get; set; } = 30000;
		public int SiloPort { get; set; } = 11111;
		public StorageProviderType? StorageProviderType { get; set; }
		public string StorageFileName { get; set; } 
	}
}