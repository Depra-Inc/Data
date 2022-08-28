using System;
using System.Collections.Generic;
using System.Linq;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Internal.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    [TestFixture]
    internal abstract class DataStorageTestsBase
    {
        protected static readonly string[] ExistedDataNames = { "ExistedData_1", "ExistedData_2", "ExistedData_3" };

        private IDataStorage _dataStorage;

        protected abstract string[] FreeDataNames { get; }

        [SetUp]
        public void SetUp()
        {
            _dataStorage = BuildDataStorage();
            CreateResourcesForTest();
            WarmUpData(ExistedDataNames);
        }

        [TearDown]
        public void TearDown()
        {
            _dataStorage.Clear();
            FreeResources();
        }

        [Test]
        public void WhenSavingData_AndStorageContainKey_ThenSuccess()
        {
            // Arrange.
            var randomExistedDataName = ExistedDataNames.Random();

            // Act.
            SaveDataInternal(randomExistedDataName, Handler);

            // Assert.
            void Handler(bool saved)
            {
                saved.Should().BeTrue();
                SpecificDataExistenceCheck(randomExistedDataName);
            }
        }

        [Test]
        public void WhenSavingData_AndStorageDoesntContainKey_ThenThrowError()
        {
            // Arrange.
            var randomNonExistedDataName = FreeDataNames.Random();

            // Act.
            SaveDataInternal(randomNonExistedDataName, Handler);

            // Assert.
            void Handler(bool saved)
            {
                saved.Should().BeTrue();
                SpecificDataExistenceCheck(randomNonExistedDataName);
            }
        }

        [Test]
        public void WhenLoadingData_AndStorageContainKey_ThenSuccess()
        {
            // Arrange.
            var randomExistingDataName = ExistedDataNames.Random();

            // Act.
            var restoredData = _dataStorage.LoadData(randomExistingDataName, TestData.Empty);

            // Assert.
            restoredData.Should().NotBeNull();
        }

        [Test]
        public void WhenLoadingData_AndStorageDoesntContainKey_ThenGetDefault()
        {
            // Arrange.
            var randomNonExistedDataName = FreeDataNames.Random();

            // Act.
            var restoredData = _dataStorage.LoadData(randomNonExistedDataName, TestData.Empty);

            // Assert.
            restoredData.Should().Be(TestData.Empty);
        }

        [Test]
        public void WhenDeletingData_AndStorageContainKey_ThenStorageDoesntContainKey()
        {
            // Arrange.
            var randomExistedDataName = ExistedDataNames.Random();
            
            // Act.
            _dataStorage.DeleteData(randomExistedDataName);

            // Assert.
            _dataStorage.GetAllKeys().Should().NotContain(randomExistedDataName);
        }

        [Test]
        public void WhenClearStorage_AndStorageNonEmpty_ThenStorageEmpty()
        {
            // Act.
            WarmUpData(FreeDataNames);
            
            // Assert.
            _dataStorage.Clear();
            _dataStorage.GetAllKeys().Should().BeEmpty();
        }

        [Test]
        public void WhenSavingType_AndTypeNonSupportedByStorage_ThenThrowError()
        {
            // Arrange.
            const string data = "stringData";
            
            // Act.
            void SavingHandler() => _dataStorage.SaveData(FreeDataNames.Random(), data);
            
            // Assert.
            Assert.Throws<NotSupportedTypeException>(SavingHandler);
        }

        [Test]
        public void WhenLoadingType_AndTypeNonSupportedByStorage_ThenThrowError()
        {
            // Arrange.
            const string data = "stringData";   
            
            // Act.
            void LoadingHandler() => _dataStorage.LoadData(FreeDataNames.Random(), data);
            
            // Assert.
            Assert.Throws<NotSupportedTypeException>(LoadingHandler);
        }

        [Test]
        public void WhenDeletingData_AndStorageDoesntContainData_ThenThrowError()
        {
            // Arrange.
            const string dataKey = "Non-existent data";
            
            // Act.
            void RemovingHandler() => _dataStorage.DeleteData(dataKey);
            
            // Assert.
            Assert.Throws<InvalidPathException>(RemovingHandler);
        }

        protected abstract IDataStorage BuildDataStorage();

        protected virtual void SpecificDataExistenceCheck(string dataName)
        {
        }

        protected virtual void CreateResourcesForTest()
        {
        }

        protected virtual void FreeResources()
        {
        }

        private void WarmUpData(IEnumerable<string> dataNames)
        {
            foreach (var dataName in dataNames)
            {
                _dataStorage.SaveData(dataName, new TestData());
            }
        }

        private void SaveDataInternal(string dataName, Action<bool> saving)
        {
            _dataStorage.SaveData(dataName, new TestData());
            var isStorageContainsSavedData = _dataStorage.GetAllKeys().Contains(dataName);
            saving?.Invoke(isStorageContainsSavedData);
        }
    }
}