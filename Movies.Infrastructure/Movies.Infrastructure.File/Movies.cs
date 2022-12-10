using Movies.Contracts.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Infrastructure.File
{
	[Serializable]
	public class Movies
	{
		[JsonProperty(propertyName: "movies")]
		public IEnumerable<Movie> MovieList { get; set; }
	}
}
