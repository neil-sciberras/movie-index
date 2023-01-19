namespace Movies.Contracts.Grains
{
	public static class GrainStorageNames
	{
		public const string FileStorage = nameof(FileStorage);
		public const string MemoryStorage = nameof(MemoryStorage);
		public const string RedisStorage = nameof(RedisStorage);
	}
}
