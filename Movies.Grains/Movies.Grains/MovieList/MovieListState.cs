using Movies.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Grains.MovieList
{
	[Serializable]
	public class MovieListState
	{
		[JsonConstructor]
		public MovieListState() { }

		[JsonProperty(propertyName: "movies")]
		public IEnumerable<Movie> Movies { get; set; } = new List<Movie>();
	}
}
