using System;

namespace Movies.Grains.Interfaces.GenreFilter
{
	public class InvalidGenreException : Exception
	{
		public InvalidGenreException(string message) : base(message) { }
	}
}
