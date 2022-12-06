using System;

namespace Movies.Grains.Proxy
{
	public class MovieNotFoundException : Exception
	{
		public MovieNotFoundException(string message) : base(message) { }
	}
}
