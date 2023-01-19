using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Contracts.Models
{
	[Serializable]
	public class Movies
	{
		[JsonProperty(propertyName: "movies")]
		public IEnumerable<Movie> MovieList { get; set; }
	}
}
