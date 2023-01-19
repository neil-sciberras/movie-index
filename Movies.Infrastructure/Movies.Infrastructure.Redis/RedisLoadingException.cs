using System;

namespace Movies.Infrastructure.Redis
{
	public class RedisLoadingException : Exception
	{
		public RedisLoadingException(string message) : base(message) { }
	}
}
