namespace Movies.Extensions
{
	public static class StringExtensions
	{
		/// <summary>
		/// Determine whether string is null or empty.
		/// </summary>
		/// <param name="value">Value to check.</param>
		/// <returns>Returns true when null or empty.</returns>
		public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

		/// <summary>
		/// Returns defaultValue when null/empty, else return original value.
		/// </summary>
		/// <param name="value">Value to check.</param>
		/// <param name="defaultValue">Value to return when null/empty.</param>
		public static string IfNullOrEmptyReturn(this string value, string defaultValue) => value.IsNullOrEmpty() ? defaultValue : value;
	}
}
