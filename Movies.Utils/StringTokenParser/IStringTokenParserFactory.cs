namespace Movies.Utils.StringTokenParser
{
	public interface IStringTokenParserFactory
	{
		/// <summary>
		/// Get or create and cache the string token parser for the specified template.
		/// </summary>
		/// <param name="template">Template to get parser for.</param>
		/// <returns></returns>
		StringTokenParser Get(string template);
	}
}
