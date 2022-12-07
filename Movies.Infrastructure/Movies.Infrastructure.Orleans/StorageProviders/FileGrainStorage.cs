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

namespace Movies.Infrastructure.Orleans.StorageProviders
{
	/// <summary>
	/// <see href="https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/custom-grain-storage"/>
	/// </summary>
	public class FileGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
	{
		private readonly string _storageName;
		private readonly FileGrainStorageOptions _fileGrainStorageOptions;
		private readonly ClusterOptions _clusterOptions;
		private readonly IGrainFactory _grainFactory;
		private readonly ITypeResolver _typeResolver;
		private JsonSerializerSettings _jsonSerializerSettings;

		private FileInfo _fileInfo;

		public FileGrainStorage(string storageName, FileGrainStorageOptions fileGrainStorageOptions, ClusterOptions clusterOptions, IGrainFactory grainFactory, ITypeResolver typeResolver)
		{
			_storageName = storageName;
			_fileGrainStorageOptions = fileGrainStorageOptions;
			_clusterOptions = clusterOptions;
			_grainFactory = grainFactory;
			_typeResolver = typeResolver;
		}

		public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			using var stream = _fileInfo.OpenText();
			var storedData = await stream.ReadToEndAsync();
			grainState.State = JsonConvert.DeserializeObject(storedData, _jsonSerializerSettings);

			grainState.ETag = _fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture);
		}

		public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
		{
			var serializedState = JsonConvert.SerializeObject(grainState.State, _jsonSerializerSettings);

			if (_fileInfo.Exists && _fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture) != grainState.ETag)
			{
				throw new InconsistentStateException(
					$"Version conflict (WriteState): " +
					$"ServiceId={_clusterOptions.ServiceId} " +
					$"ProviderName={_storageName} " +
					$"GrainType={grainType} " +
					$"GrainReference={grainReference.ToKeyString()}.");
			}

			using var streamWriter = new StreamWriter(_fileInfo.Open(FileMode.Create, FileAccess.Write));
			await streamWriter.WriteAsync(serializedState);

			_fileInfo.Refresh();
			grainState.ETag = _fileInfo.LastWriteTimeUtc.ToString(CultureInfo.InvariantCulture);
		}

		public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState) => throw new NotImplementedException();

		public void Participate(ISiloLifecycle lifecycle) 
			=> lifecycle.Subscribe(
				observerName: OptionFormattingUtilities.Name<FileGrainStorage>(_storageName), 
				stage: ServiceLifecycleStage.ApplicationServices,
				onStart: Init);

		private Task Init(CancellationToken ct)
		{
			_jsonSerializerSettings = OrleansJsonSerializer.UpdateSerializerSettings(
				settings: OrleansJsonSerializer.GetDefaultSerializerSettings(_typeResolver, _grainFactory),
				useFullAssemblyNames: false,
				indentJson: false,
				typeNameHandling: null);

			_fileInfo = new FileInfo(_fileGrainStorageOptions.FullFileName);

			if (!_fileInfo.Exists)
			{
				throw new FileNotFoundException($"File '{_fileInfo.FullName}' does not exist");
			}

			return Task.CompletedTask;
		}
	}
}
