using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Validators;
using Depra.Data.Storage.Extensions;
using Depra.Data.Storage.Loading.Impl;
using Depra.Data.Storage.Local;
using Depra.Data.Storage.Middleware.Impl;
using Depra.Data.Storage.Saving.Impl;

namespace Depra.Data.Storage.Benchmark
{
    [InProcess]
    [MemoryDiagnoser]
    [StopOnFirstError]
    [Config(typeof(Config))]
    public class DataStorageBenchmark
    {
        private const string FilePath = nameof(LocalDataStorage);
        private const string FileFormat = ".test";
        private const string Directory = "Storage.Tests";

        private class Config : ManualConfig
        {
            public Config()
            {
                WithOptions(ConfigOptions.Default)
                    .AddValidator(JitOptimizationsValidator.FailOnError)
                    .AddLogger(ConsoleLogger.Default)
                    .AddColumnProvider(DefaultColumnProviders.Instance);
            }
        }

        private LocalDataStorage _dataStorage;

        [Benchmark]
        public void SaveAndLoad()
        {
            Save();
            Load();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var serializer = new BinarySerializer();
            var dataReader = new FileReader(serializer);
            var dataWriter = new FileWriter(serializer);
            var location = new LocalFileLocation(Directory, FileFormat, SearchOption.TopDirectoryOnly);
            var dataSaver = new DataSaver(dataWriter, location);
            var dataLoader = new DataLoader(dataReader, location);
            _dataStorage = new LocalDataStorage(location, dataSaver, dataLoader);
        }

        private void Save()
        {
            var sourceData = new TestData();
            _dataStorage.SaveData(FilePath, sourceData);
        }

        private void Load()
        {
            var unused = _dataStorage.LoadData<TestData>(FilePath, null);
        }
    }
}