using Movies.Contracts.Models;

namespace Movies.Grains.ContractExtensions
{
	public static class MovieExtensions
	{
		public static Movie UpdateMovie(this Movie existingMovie, Movie changes)
		{
			return new Movie
			{
				Id = changes.Id,
				Key = changes.Key ?? existingMovie.Key,
				Name = changes.Name ?? existingMovie.Name,
				Description = changes.Description ?? existingMovie.Description,
				Genres = changes.Genres ?? existingMovie.Genres,
				Rate = changes.Rate ?? existingMovie.Rate,
				Length = changes.Length ?? existingMovie.Length,
				Image = changes.Image ?? existingMovie.Image
			};
		}
	}
}
