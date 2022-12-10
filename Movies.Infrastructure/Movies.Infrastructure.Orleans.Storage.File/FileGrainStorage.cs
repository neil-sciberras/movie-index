using Newtonsoft.Json;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using Orleans.Serialization;
using Orleans.Storage;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Orleans.Storage.File
{
	/// <summary>
	/// From: <see href="https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage"/>.
	/// <para></para>
	/// The only difference is that this does not use <see cref="JsonSerializerSettings"/> from <see cref="OrleansJsonSerializer"/>.
	/// It passes no settings object to <see cref="JsonConvert.SerializeObject"/> and <see cref="JsonConvert.DeserializeObject"/>.
	/// <para></para>
	/// An <see cref="InvalidCastException"/> was being thrown when deserializing the json text using the <see cref="JsonSerializerSettings"/> from Orleans.
	/// </summary>
	public class FileGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
	{
		private readonly string _storageName;
		private readonly FileStorageOptions _fileStorageOptions;
		private readonly ClusterOptions _clusterOptions;

		private FileInfo _fileInfo;

		public FileGrainStorage(string storageName, FileStorageOptions fileStorageOptions, ClusterOptions clusterOptions)
		{
			_storageName = storageName;
			_fileStorageOptions = fileStorageOptions;
			_clusterOptions = clusterOptions;
		}

		public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			string storedData;

			using (var stream = _fileInfo.OpenText())
			{
				storedData = await stream.ReadToEndAsync();
			}

			grainState.State = JsonConvert.DeserializeObject(storedData, grainState.Type);
			grainState.ETag = _fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture);
		}

		public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			var serializedState = JsonConvert.SerializeObject(grainState.State, Formatting.Indented);

			if (_fileInfo.Exists && _fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture) != grainState.ETag)
			{
				throw new InconsistentStateException(
					$"Version conflict (WriteState): " +
					$"ServiceId={_clusterOptions.ServiceId} " +
					$"ProviderName={_storageName} " +
					$"GrainType={grainType} " +
					$"GrainReference={grainReference.ToKeyString()}.");
			}

			using (var streamWriter = new StreamWriter(_fileInfo.Open(FileMode.Create, FileAccess.Write)))
			{
				await streamWriter.WriteAsync(serializedState);
			}

			_fileInfo.Refresh();
			grainState.ETag = _fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture);
		}

		public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			throw new NotImplementedException();
		}

		public void Participate(ISiloLifecycle lifecycle)
		{
			lifecycle.Subscribe(
				observerName: OptionFormattingUtilities.Name<FileGrainStorage>(_storageName),
				stage: ServiceLifecycleStage.ApplicationServices,
				onStart: Init);
		}

		private Task Init(CancellationToken ct)
		{
			_fileInfo = new FileInfo(_fileStorageOptions.FullFileName);

			if (!_fileInfo.Exists)
			{
				throw new FileNotFoundException($"File '{_fileInfo.FullName}' does not exist");
			}

			return Task.CompletedTask;
		}
	}
}
