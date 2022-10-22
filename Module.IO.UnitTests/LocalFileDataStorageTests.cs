using Depra.Data.Domain.Exceptions;
using Depra.Data.Module.IO.Mount;
using Depra.Data.Module.IO.Scanning;
using Depra.Data.Module.IO.Storage;
using Depra.Data.Module.IO.UnitTests.Extensions;
using Depra.Data.Module.IO.UnitTests.Helpers;
using Depra.Serialization.Application.Binary;
using FluentAssertions;
using NUnit.Framework;

namespace Depra.Data.Module.IO.UnitTests;

[TestFixture(TestOf = typeof(LocalFIleDataStorage))]
internal class LocalFileDataStorageTests
{
    private const string FILE_FORMAT = ".test";
    private const string FOLDER_NAME = "Test";

    private static readonly string[] FREE_DATA_NAMES = {"FileData_1", "FileData_2", "FileData_3"};
    private static readonly string[] EXISTED_DATA_NAMES = {"ExistedData_1", "ExistedData_2", "ExistedData_3"};
    private static readonly string DIRECTORY_PATH = Path.Combine(Environment.CurrentDirectory, FOLDER_NAME);

    private LocalDirectory _localDirectory = null!;
    private LocalFIleDataStorage _fileDataStorage = null!;
    private LocalDirectoryScanner _directoryScanner = null!;

    [SetUp]
    public void SetUp()
    {
        _localDirectory = new LocalDirectory(FOLDER_NAME, DIRECTORY_PATH, SearchOption.TopDirectoryOnly);
        _directoryScanner = new LocalDirectoryScanner(_localDirectory, FILE_FORMAT);
        var serializer = new BinarySerializer();
        _fileDataStorage = new LocalFIleDataStorage(serializer, _localDirectory, FILE_FORMAT);

        FreeResources();
        Helper.WarpUpStorage(_fileDataStorage, EXISTED_DATA_NAMES, () => new TestData());
    }

    [TearDown]
    public void TearDown()
    {
        FreeResources();
            
        if (IsDirectoryEmpty(DIRECTORY_PATH))
        {
            Directory.Delete(DIRECTORY_PATH);
        }
    }

    [Test]
    public void WhenSavingData_AndStorageContainKey_ThenStorageBeganToContainKeyOfData()
    {
        // Arrange.
        var data = new TestData();
        var dataStorage = _fileDataStorage;
        var randomExistedDataName = EXISTED_DATA_NAMES.Random();

        // Act.
        dataStorage.SaveData(randomExistedDataName, data);

        // Assert.
        dataStorage.GetAllKeys().Should().Contain(randomExistedDataName);
    }

    [Test]
    public void WhenSavingData_AndStorageContainKey_ThenFileIsFoundInLocalFolder()
    {
        // Arrange.
        var data = new TestData();
        var dataStorage = _fileDataStorage;
        var randomExistedDataName = EXISTED_DATA_NAMES.Random();
        var fullFilePath = _directoryScanner.GetFullFilePath(randomExistedDataName);

        // Act.
        dataStorage.SaveData(randomExistedDataName, data);

        // Assert.
        fullFilePath.File().Should().Exist();
    }

    [Test]
    public void WhenSavingData_AndStorageDoesntContainKey_ThenStorageBeganToContainKeyOfNewData()
    {
        // Arrange.
        var data = new TestData();
        var dataStorage = _fileDataStorage;
        var randomNonExistedDataName = FREE_DATA_NAMES.Random();

        // Act.
        dataStorage.SaveData(randomNonExistedDataName, data);

        // Assert.
        dataStorage.GetAllKeys().Should().Contain(randomNonExistedDataName);
    }
        
    [Test]
    public void WhenSavingData_AndStorageDoesntContainKey_ThenFileIsFoundInLocalFolder()
    {
        // Arrange.
        var data = new TestData();
        var dataStorage = _fileDataStorage;
        var randomNonExistedDataName = FREE_DATA_NAMES.Random();
        var fullFilePath = _directoryScanner.GetFullFilePath(randomNonExistedDataName);

        // Act.
        dataStorage.SaveData(randomNonExistedDataName, data);

        // Assert.
        fullFilePath.File().Should().Exist();
    }

    [Test]
    public void WhenLoadingData_AndStorageContainKey_ThenLoadedDataTypeIsEqualToOriginal()
    {
        // Arrange.
        var originalData = TestData.Empty;
        var dataStorage = _fileDataStorage;
        var originalDataType = originalData.GetType();
        var randomExistingDataName = EXISTED_DATA_NAMES.Random();

        // Act.
        var loadedData = dataStorage.LoadData(randomExistingDataName, originalData);

        // Assert.
        loadedData.Should().BeOfType(originalDataType);
    }

    [Test]
    public void WhenLoadingDataWithoutDefaultValue_AndStorageDoesntContainKey_ThenAKeyNotFoundExceptionIsThrown()
    {
        // Arrange.
        var dataStorage = _fileDataStorage;
        var randomNonExistedDataName = FREE_DATA_NAMES.Random();

        // Act.
        Action act = () => dataStorage.LoadData<TestData>(randomNonExistedDataName, null);

        // Assert.
        act.Should().Throw<KeyNotFoundException>();
    }

    [Test]
    public void WhenClearingDataByKey_AndStorageContainKey_ThenDataKeyIsNotFoundInStorage()
    {
        // Arrange.
        var dataStorage = _fileDataStorage;
        var randomExistedDataName = EXISTED_DATA_NAMES.Random();

        // Act.
        dataStorage.ClearData(randomExistedDataName);

        // Assert.
        dataStorage.GetAllKeys().Should().NotContain(randomExistedDataName);
    }

    [Test]
    public void WhenClearingAllData_AndStorageIsNotEmpty_ThenStorageIsEmpty()
    {
        // Arrange.
        var dataKeys = FREE_DATA_NAMES;
        var dataStorage = _fileDataStorage;
            
        // Act.
        Helper.WarpUpStorage(dataStorage, dataKeys, () => new TestData());
        dataStorage.ClearData();

        // Assert.
        dataStorage.GetAllKeys().Should().BeEmpty();
    }

    [Test]
    public void WhenClearingDataByKey_AndStorageDoesNotContainKey_ThenAnInvalidOperationExceptionIsThrown()
    {
        // Arrange.
        const string dataKey = "Non-existent data";
        var dataStorage = _fileDataStorage;

        // Act.
        var act = () => dataStorage.ClearData(dataKey);

        // Assert.
        act.Should().Throw<InvalidPathException>();
    }

    private static LocalSystemFileDto CreateFileDto(string fileName) =>
        new LocalSystemFileDto(fileName, Path.Combine(DIRECTORY_PATH, fileName), FILE_FORMAT);
    
    private void FreeResources()
    {
        var filesForDelete = new List<string>();
        filesForDelete.AddRange(FREE_DATA_NAMES);
        filesForDelete.AddRange(EXISTED_DATA_NAMES);

        foreach (var fullFilePath in
                 filesForDelete.Select(fileName => _directoryScanner.GetFullFilePath(fileName)))
        {
            File.Delete(fullFilePath);
        }
    }
        
    private static bool IsDirectoryEmpty(string path)
    {
        return !Directory.EnumerateFileSystemEntries(path).Any();
    }
}