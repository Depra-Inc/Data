using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Validators;
using Depra.Data.Persistent.Extensions;
using Depra.Data.Persistent.Locations;
using Depra.Data.Persistent.Readers;
using Depra.Data.Persistent.Serializers;
using Depra.Data.Persistent.Storages;
using Depra.Data.Persistent.Writers;

namespace Depra.Data.Persistent.Benchmark
{
    [InProcess]
    [MemoryDiagnoser]
    [StopOnFirstError]
    [Config(typeof(Config))]
    public class DataStorageBenchmark
    {
        private const string FilePath = nameof(PersistentLocalStorage);
        private const string FileFormat = ".test";
        private const string Directory = "Persistent.Tests";

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

        private PersistentLocalStorage _storage;

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
            var location = new LocalFileLocation(Directory, FileFormat);
            _storage = new PersistentLocalStorage(location, dataReader, dataWriter);
        }

        private void Save()
        {
            var sourceData = new TestData();
            _storage.SaveData(FilePath, sourceData);
        }

        private void Load()
        {
            var unused = _storage.LoadData<TestData>(FilePath, null);
        }
    }
}