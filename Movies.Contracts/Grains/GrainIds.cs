namespace Movies.Contracts.Grains
{
	public static class GrainIds
	{
		public const string MovieProxyGrainId = nameof(MovieProxyGrainId);
		public const string MovieListGrainId = nameof(MovieListGrainId);
		public const string TopRatedMoviesSupervisorGrainId = nameof(TopRatedMoviesSupervisorGrainId);
		public const string GenreFilterSupervisorGrainId = nameof(GenreFilterSupervisorGrainId);
	}
}
