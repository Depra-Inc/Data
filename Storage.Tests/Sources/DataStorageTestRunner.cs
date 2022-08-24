using System.Collections.Generic;
using System.Linq;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Internal.Exceptions;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    [TestFixture]
    internal abstract class DataStorageTestRunner
    {
        protected static readonly string[] ExistedDataNames = { "ExistedData_1", "ExistedData_2", "ExistedData_3" };

        private IDataStorage _dataStorage;

        protected abstract string[] FreeDataNames { get; }

        [SetUp]
        public void SetUp()
        {
            _dataStorage = BuildDataStorage();
            CreateResourcesForTest();
        }

        [TearDown]
        public void TearDown()
        {
            _dataStorage.Clear();
            FreeResources();
        }

        [Test]
        public void Save_New_Data()
        {
            var randomNonExistedDataName = FreeDataNames.Random();
            SaveData(randomNonExistedDataName);

            var isStorageContainsSavedData = _dataStorage.GetAllKeys().Contains(randomNonExistedDataName);
            Assert.IsTrue(isStorageContainsSavedData);

            SpecificDataExistenceCheck(randomNonExistedDataName);
        }

        [Test]
        public void Save_Existing_Data()
        {
            var randomExistingDataName = ExistedDataNames.Random();
            SaveData(randomExistingDataName);

            var isStorageContainsSavedData = _dataStorage.GetAllKeys().Contains(randomExistingDataName);
            Assert.IsTrue(isStorageContainsSavedData);

            SpecificDataExistenceCheck(randomExistingDataName);
        }

        [Test]
        public void Load_Existing_Data()
        {
            var randomExistingDataName = ExistedDataNames.Random();
            var restoredData = _dataStorage.LoadData(randomExistingDataName, TestData.Empty);

            Assert.AreNotEqual(null, restoredData);
        }

        [Test]
        public void Load_Non_Existent_Data()
        {
            var randomNonExistedDataName = FreeDataNames.Random();
            var restoredData = _dataStorage.LoadData(randomNonExistedDataName, TestData.Empty);

            Assert.AreEqual(TestData.Empty, restoredData);
        }

        [Test]
        public void Clear_Storage()
        {
            var existingDataCount = _dataStorage.GetAllKeys().Count();
            WarmUpData(FreeDataNames);

            var expectedCount = existingDataCount + FreeDataNames.Length;
            Assert.AreEqual(expectedCount, _dataStorage.GetAllKeys().Count());

            _dataStorage.Clear();

            Assert.AreEqual(0, _dataStorage.GetAllKeys().Count());
        }

        [Test]
        public void Error_Saving_Not_Supported_Type()
        {
            void SavingHandler() => _dataStorage.SaveData(FreeDataNames.Random(), "stringData");
            Assert.Throws<NotSupportedTypeException>(SavingHandler);
        }

        [Test]
        public void Error_Loading_Not_Supported_Type()
        {
            void LoadingHandler() => _dataStorage.LoadData(FreeDataNames.Random(), "stringData");
            Assert.Throws<NotSupportedTypeException>(LoadingHandler);
        }

        [Test]
        public void Error_Deleting_Non_Existent_Data()
        {
            void RemovingHandler() => _dataStorage.RemoveData("Non-existent data");
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

        protected void WarmUpData(IEnumerable<string> dataNames)
        {
            foreach (var dataName in dataNames)
            {
                _dataStorage.SaveData(dataName, new TestData());
            }
        }

        private void SaveData(string dataName)
        {
            var sourceData = new TestData();
            _dataStorage.SaveData(dataName, sourceData);
        }
    }
}