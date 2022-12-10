using Movies.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Grains
{
	[Serializable]
	public class MovieListState
	{
		[JsonConstructor]
		public MovieListState() { }

		[JsonProperty(propertyName: "movies")]
		public IEnumerable<Movie> Movies { get; set; }
	}
}
