using System;

namespace Movies.Grains.Interfaces.Exceptions
{
	public class MovieNotFoundException : Exception
	{
		public MovieNotFoundException(string message) : base(message) { }
	}
}
