using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Movies.Contracts.Models
{
	[Serializable]
	public class NewMovie
	{
		[JsonProperty(propertyName: "key")]
		public string Key { get; set; }

		[JsonProperty(propertyName: "name")]
		public string Name { get; set; }

		[JsonProperty(propertyName: "description")]
		public string Description { get; set; }

		[JsonProperty(propertyName: "genres")]
		public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();

		[JsonProperty(propertyName: "rate")]
		public string Rate { get; set; }

		[JsonProperty(propertyName: "length")]
		public string Length { get; set; }

		[JsonProperty(propertyName: "img")]
		public string Image { get; set; }
	}
}
