using Newtonsoft.Json;
using System;

namespace Movies.Contracts.Models
{
	[Serializable]
	public class Movie : NewMovie
	{
		[JsonConstructor]
		public Movie(){}

		[JsonProperty(propertyName:"id")]
		public int Id { get; set; }
	}
}
