using Movies.Contracts.Models;
using System.Collections.Generic;

namespace Movies.Grains.Proxy
{
	public class ProxyState
	{
		public IEnumerable<Movie> Movies { get; set; }
	}
}
