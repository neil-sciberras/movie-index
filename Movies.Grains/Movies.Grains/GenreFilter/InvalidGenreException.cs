using System;

namespace Movies.Grains.GenreFilter
{
	public class InvalidGenreException : Exception
	{
		public InvalidGenreException(string message) : base(message) { }
	}
}
