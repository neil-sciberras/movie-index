using Movies.Contracts.Models;

namespace Movies.Grains.DataUpdates.ContractExtensions
{
	public static class NewMovieExtensions
	{
		public static Movie ConvertToMovie(this NewMovie newMovie, int id)
		{
			return new Movie
			{
				Id = id,
				Key = newMovie.Key,
				Name = newMovie.Name,
				Description = newMovie.Description,
				Genres = newMovie.Genres,
				Rate = newMovie.Rate,
				Length = newMovie.Length,
				Image = newMovie.Image
			};
		}
	}
}
