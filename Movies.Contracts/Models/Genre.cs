using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace Movies.Contracts.Models
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum Genre
	{
		Action,
		Adventure,
		Comedy,
		Crime,
		Biography,
		Drama,
		History,
		Sport,
		Mystery,
		Thriller,
		SciFi
	}
}
