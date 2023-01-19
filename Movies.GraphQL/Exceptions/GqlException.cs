using System;
using System.Runtime.Serialization;

namespace Movies.GraphQL.Exceptions
{
	public class GqlException : Exception
	{
		public GqlRequest Request { get; }
		public string ErrorCode { get; }

		public GqlException()
		{
		}

		public GqlException(string message)
			: base(message)
		{
		}

		public GqlException(string message, Exception inner, GqlRequest request = null, string errorCode = null)
			: base(message, inner)
		{
			Request = request;
			ErrorCode = errorCode;
		}

		protected GqlException(
			SerializationInfo info,
			StreamingContext context
		) : base(info, context)
		{
		}
	}
}