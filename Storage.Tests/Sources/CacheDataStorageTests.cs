using System;
using System.Collections.Generic;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Caching.Impl;
using Depra.Data.Storage.Impl;
using Depra.Data.Storage.Internal.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    [TestFixture]
    internal class CacheDataStorageTests
    {
        private static readonly string[] FreeDataNames = { "FileData_1", "FileData_2", "FileData_3" };
        private static readonly string[] ExistedDataNames = { "ExistedData_1", "ExistedData_2", "ExistedData_3" };

        private IDataStorage _cacheDataStorage;

        [SetUp]
        public void SetUp()
        {
            _cacheDataStorage = BuildDataStorage();
            CreateResourcesForTest();
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void WhenSavingData_AndStorageContainKey_ThenSuccess()
        {
            // Arrange.
            var data = new TestData();
            var randomExistedDataName = ExistedDataNames.Random();

            // Act.
            _cacheDataStorage.SaveData(randomExistedDataName, data);

            // Assert.
            _cacheDataStorage.GetAllKeys().Should().Contain(randomExistedDataName);
        }

        [Test]
        public void WhenSavingData_AndStorageDoesntContainKey_ThenThrowError()
        {
            // Arrange.
            var data = new TestData();
            var randomNonExistedDataName = FreeDataNames.Random();

            // Act.
            _cacheDataStorage.SaveData(randomNonExistedDataName, data);

            // Assert.
            _cacheDataStorage.GetAllKeys().Should().Contain(randomNonExistedDataName);
        }

        [Test]
        public void WhenLoadingData_AndStorageContainKey_ThenSuccess()
        {
            // Arrange.
            var data = TestData.Empty;
            var randomExistingDataName = ExistedDataNames.Random();

            // Act.
            var restoredData = _cacheDataStorage.LoadData(randomExistingDataName, data);

            // Assert.
            restoredData.Should().NotBeNull();
        }

        [Test]
        public void WhenLoadingData_AndStorageDoesntContainKey_ThenGetDefault()
        {
            // Arrange.
            var data = TestData.Empty;
            var randomNonExistedDataName = FreeDataNames.Random();

            // Act.
            var restoredData = _cacheDataStorage.LoadData(randomNonExistedDataName, data);

            // Assert.
            restoredData.Should().Be(TestData.Empty);
        }

        [Test]
        public void WhenDeletingData_AndStorageContainKey_ThenStorageDoesntContainKey()
        {
            // Arrange.
            var randomExistedDataName = ExistedDataNames.Random();

            // Act.
            _cacheDataStorage.DeleteData(randomExistedDataName);

            // Assert.
            _cacheDataStorage.GetAllKeys().Should().NotContain(randomExistedDataName);
        }

        [Test]
        public void WhenClearStorage_AndStorageNonEmpty_ThenStorageEmpty()
        {
            // Act.
            WarmUpData(FreeDataNames);

            // Assert.
            _cacheDataStorage.Clear();
            _cacheDataStorage.GetAllKeys().Should().BeEmpty();
        }

        [Test]
        public void WhenSavingType_AndTypeNonSupportedByStorage_ThenThrowError()
        {
            // Arrange.
            const string data = "stringData";

            // Act.
            void SavingHandler() => _cacheDataStorage.SaveData(FreeDataNames.Random(), data);

            // Assert.
            Assert.Throws<NotSupportedTypeException>(SavingHandler);
        }

        [Test]
        public void WhenLoadingType_AndTypeNonSupportedByStorage_ThenThrowError()
        {
            // Arrange.
            const string data = "stringData";

            // Act.
            void LoadingHandler() => _cacheDataStorage.LoadData(FreeDataNames.Random(), data);

            // Assert.
            Assert.Throws<NotSupportedTypeException>(LoadingHandler);
        }

        [Test]
        public void WhenDeletingData_AndStorageDoesntContainData_ThenThrowError()
        {
            // Arrange.
            const string dataKey = "Non-existent data";

            // Act.
            void RemovingHandler() => _cacheDataStorage.DeleteData(dataKey);

            // Assert.
            Assert.Throws<InvalidPathException>(RemovingHandler);
        }

        private static IDataStorage BuildDataStorage()
        {
            var cache = new ThreadSafeCacheDictionary();
            var storage = StandardDataStorageBuilder
                .Configure(new CacheLocation(cache), builder => builder
                    .AddSaver(saver => saver.AddWriter(new CacheWriter<TestData>(cache)))
                    .AddLoader(loader => loader.AddReader(new CacheReader<TestData>(cache))))
                .Build();

            return storage;
        }

        private void WarmUpData(IEnumerable<string> dataNames) =>
            Array.ForEach((string[])dataNames, dataName => _cacheDataStorage.SaveData(dataName, new TestData()));

        private void CreateResourcesForTest() =>
            Array.ForEach(ExistedDataNames, fileName => _cacheDataStorage.SaveData(fileName, new TestData()));
    }
}