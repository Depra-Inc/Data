using System.IO;
using BenchmarkDotNet.Attributes;
using Depra.Data.Application.Storage;
using Depra.Data.Domain.Storage;
using Depra.Data.Module.IO.Mount;
using Depra.Data.Module.IO.Storage;
using Depra.Data.Storage.Benchmark.Types;
using Depra.Serialization.Application.Binary;

namespace Depra.Data.Storage.Benchmark;

public class LocalDataStorageBenchmarks
{
    private const string FILE_PATH = nameof(DataStorage);
    private const string FILE_FORMAT = ".test";
    private const string DIRECTORY_PATH = "Storage.IO.Tests";

    private IDataStorage _dataStorage;

    [GlobalSetup]
    public void GlobalSetup()
    {
        var serializer = new BinarySerializer();
        var directory = new LocalDirectory(Path.GetDirectoryName(DIRECTORY_PATH), DIRECTORY_PATH, SearchOption.TopDirectoryOnly);
        _dataStorage = new LocalFIleDataStorage(serializer, directory, FILE_FORMAT);
    }

    [Benchmark]
    public void SaveData()
    {
        var sourceData = new TestData();
        _dataStorage.SaveData(FILE_PATH, sourceData);
    }

    [Benchmark]
    public void LoadData()
    {
        var unused = _dataStorage.LoadData<TestData>(FILE_PATH, null);
    }
}