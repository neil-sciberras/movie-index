using System.Collections.Concurrent;

namespace Movies.Utils.StringTokenParser
{
	public class StringTokenParserFactory : IStringTokenParserFactory
	{
		private static readonly ConcurrentDictionary<string, StringTokenParser> TemplateParserCache
			= new ConcurrentDictionary<string, StringTokenParser>();

		/// <inheritdoc />
		public StringTokenParser Get(string template)
			=> TemplateParserCache.GetOrAdd(template, arg => new StringTokenParser(arg));
	}
}
