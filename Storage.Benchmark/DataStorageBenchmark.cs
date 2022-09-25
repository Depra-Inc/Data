using System.IO;
using BenchmarkDotNet.Attributes;
using Depra.Data.Operations.Impl;
using Depra.Data.Operations.IO.Bridge;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Benchmark.Types;
using Depra.Data.Storage.Impl;
using Depra.Serialization.Application.Binary;

namespace Depra.Data.Storage.Benchmark
{
    [MemoryDiagnoser]
    public class DataStorageBenchmark
    {
        private const string FILE_PATH = nameof(LocalDataStorage);
        private const string FILE_FORMAT = ".test";
        private const string DIRECTORY = "Storage.IO.Tests";

        private IDataStorage _dataStorage;

        [Benchmark]
        public void SaveAndLoad()
        {
            SaveInternal();
            LoadInternal();
        }

        [Benchmark]
        public void Save() => SaveInternal();

        [Benchmark]
        public void Load() => LoadInternal();

        [GlobalSetup]
        public void GlobalSetup()
        {
            var serializer = new BinarySerializer();
            var location = new LocalFileBridge(DIRECTORY, FILE_FORMAT, serializer, SearchOption.TopDirectoryOnly);
            _dataStorage = StandardDataStorageBuilder
                .Configure(location, builder => builder
                    .AddSaver(saver => saver.AddWriter(new DataWriter<TestData>(location)))
                    .AddLoader(loader => loader.AddReader(new DataReader<TestData>(location))))
                .Build();
        }

        private void SaveInternal()
        {
            var sourceData = new TestData();
            _dataStorage.SaveData(FILE_PATH, sourceData);
        }

        private void LoadInternal()
        {
            var unused = _dataStorage.LoadData<TestData>(FILE_PATH, null);
        }
    }
}