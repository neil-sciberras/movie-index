using Movies.Infrastructure.Redis;
using Newtonsoft.Json;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Storage;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Orleans.Storage.Redis
{
	public class RedisGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
	{
		private readonly string _storageName;
		private readonly ClusterOptions _clusterOptions;
		private readonly IRedisReader _redisReader;
		private readonly IRedisWriter _redisWriter;

		public RedisGrainStorage(IRedisReader redisReader, IRedisWriter redisWriter, ClusterOptions clusterOptions, string storageName)
		{
			_redisReader = redisReader;
			_redisWriter = redisWriter;
			_clusterOptions = clusterOptions;
			_storageName = storageName;
		}

		public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			var movieId = (int)grainReference.GetPrimaryKeyLong();
			grainState.State = await _redisReader.ReadMovieAsync(movieId);
			grainState.ETag = grainState.State == null ? null : JsonConvert.SerializeObject(grainState.State);
		}

		public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			var movieId = (int)grainReference.GetPrimaryKeyLong();
			var currentMovie = await _redisReader.ReadMovieAsync(movieId);
			var expectedEtag = currentMovie == null ? null : JsonConvert.SerializeObject(currentMovie);

			if (!string.Equals(grainState.ETag , expectedEtag))
			{
				throw new InconsistentStateException(
					"Version conflict (WriteState): " +
					$"ServiceId={_clusterOptions.ServiceId} " +
					$"ProviderName={_storageName} " +
					$"GrainType={grainType} " +
					$"GrainReference={grainReference.ToKeyString()}.");
			}

			await _redisWriter.WriteMovieAsync(movieId.ToString(), grainState.State);
		}

		public async Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			var movieId = grainReference.GetPrimaryKeyLong();
			await _redisWriter.DeleteAsync(movieId.ToString());
		}

		public void Participate(ISiloLifecycle lifecycle)
		{
			
		}
	}
}
