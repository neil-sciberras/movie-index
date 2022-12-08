using System;

namespace Movies.Grains.Interfaces.Exceptions
{
	public class InvalidGenreException : Exception
	{
		public InvalidGenreException(string message) : base(message) { }
	}
}
