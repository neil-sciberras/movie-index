using System.Collections.Generic;

namespace Movies.Grains.TopRatedMovies
{
	public class TopRatedMoviesSupervisorState
	{
		public HashSet<int> TopRatedMovieGrainPrimaryKeys { get; set;}
	}
}
