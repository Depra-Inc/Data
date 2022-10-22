using System;
using System.Collections.Generic;
using Depra.Data.Application.Cache;
using Depra.Data.Application.UnitTests.Extensions;
using Depra.Data.Application.UnitTests.Helpers;
using Depra.Data.Domain.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Depra.Data.Application.UnitTests;

[TestFixture(TestOf = typeof(CacheDataStorage))]
internal class CacheDataStorageTests
{
    private static readonly string[] FREE_DATA_NAMES = {"FileData_1", "FileData_2", "FileData_3"};
    private static readonly string[] EXISTED_DATA_NAMES = {"ExistedData_1", "ExistedData_2", "ExistedData_3"};

    private CacheDataStorage _cacheDataStorage;

    [SetUp]
    public void SetUp()
    {
        _cacheDataStorage = new CacheDataStorage(false);
        Helper.WarpUpStorage(_cacheDataStorage, EXISTED_DATA_NAMES, () => new TestData());
    }

    [TearDown]
    public void TearDown() { }

    [Test]
    public void WhenSavingData_AndStorageContainKey_ThenStorageBeganToContainKeyOfData()
    {
        // Arrange.
        var originalData = new TestData();
        var dataStorage = _cacheDataStorage;
        var randomExistedDataName = EXISTED_DATA_NAMES.Random();

        // Act.
        dataStorage.SaveData(randomExistedDataName, originalData);

        // Assert.
        dataStorage.GetAllKeys().Should().Contain(randomExistedDataName);
    }

    [Test]
    public void WhenSavingData_AndStorageDoesntContainKey_ThenStorageBeganToContainKeyOfNewData()
    {
        // Arrange.
        var originalData = new TestData();
        var dataStorage = _cacheDataStorage;
        var randomNonExistedDataName = FREE_DATA_NAMES.Random();

        // Act.
        dataStorage.SaveData(randomNonExistedDataName, originalData);

        // Assert.
        dataStorage.GetAllKeys().Should().Contain(randomNonExistedDataName);
    }

    [Test]
    public void WhenLoadingData_AndStorageContainKey_ThenLoadedDataTypeIsEqualToOriginal()
    {
        // Arrange.
        var originalData = TestData.Empty;
        var dataStorage = _cacheDataStorage;
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
        var dataStorage = _cacheDataStorage;
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
        var dataStorage = _cacheDataStorage;
        var randomExistedDataName = EXISTED_DATA_NAMES.Random();

        // Act.
        dataStorage.ClearData(randomExistedDataName);

        // Assert.
        dataStorage.GetAllKeys().Should().NotContain(randomExistedDataName);
    }

    [Test]
    public void WhenClearingDataByKey_AndStorageDoesNotContainThisKey_ThenAnInvalidPathExceptionIsThrown()
    {
        // Arrange.
        const string dataKey = "Non-existent data";
        var dataStorage = _cacheDataStorage;

        // Act.
        var act = () => dataStorage.ClearData(dataKey);

        // Assert.
        act.Should().Throw<InvalidPathException>();
    }

    [Test]
    public void WhenClearingAllData_AndStorageIsNotEmpty_ThenStorageIsEmpty()
    {
        // Arrange.
        var dataKeys = FREE_DATA_NAMES;
        var dataStorage = _cacheDataStorage;
            
        // Act.
        Helper.WarpUpStorage(dataStorage, dataKeys, () => new TestData());
        dataStorage.ClearData();

        // Assert.
        dataStorage.GetAllKeys().Should().BeEmpty();
    }
}