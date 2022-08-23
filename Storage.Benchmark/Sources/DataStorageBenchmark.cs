using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Validators;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Impl;
using Depra.Data.Storage.IO;
using Depra.Data.Storage.Serialization;

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
        private const string Directory = "Storage.IO.Tests";

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

        private IDataStorage _dataStorage;

        [Benchmark]
        public void SaveAndLoad()
        {
            Save();
            Load();
        }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var location = new LocalFileLocation(Directory, FileFormat, SearchOption.TopDirectoryOnly);
            var serializer = new BinarySerializer();
            
            _dataStorage = new StandardDataStorageBuilder()
                .SetLocation(location)
                .SetSaver(saver => saver.AddWriter(new FileWriter<TestData>(serializer)))
                .SetLoader(loader => loader.AddReader(new FileReader<TestData>(serializer)))
                .Build();
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