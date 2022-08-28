using System.IO;
using BenchmarkDotNet.Attributes;
using Depra.Data.Serialization.Binary;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Impl;
using Depra.Data.Storage.IO;

namespace Depra.Data.Storage.Benchmark
{
    [MemoryDiagnoser]
    public class DataStorageBenchmark
    {
        private const string FilePath = nameof(LocalDataStorage);
        private const string FileFormat = ".test";
        private const string Directory = "Storage.IO.Tests";

        private static readonly ILocationProvider Location =
            new LocalFileLocation(Directory, FileFormat, SearchOption.TopDirectoryOnly);

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

            _dataStorage = StandardDataStorageBuilder
                .Configure(Location, builder => builder
                    .AddSaver(saver => saver.AddWriter(new FileWriter<TestData>(Location, serializer)))
                    .AddLoader(loader => loader.AddReader(new FileReader<TestData>(Location, serializer))))
                .Build();
        }

        private void SaveInternal()
        {
            var sourceData = new TestData();
            _dataStorage.SaveData(FilePath, sourceData);
        }

        private void LoadInternal()
        {
            var unused = _dataStorage.LoadData<TestData>(FilePath, null);
        }
    }
}