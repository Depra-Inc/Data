using Depra.Data.Storage.Api;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    internal abstract class DataStorageTestRunner
    {
        private IDataStorage _dataStorage;
        private DataStorageTester _dataStorageTester;

        [SetUp]
        public void SetUp()
        {
            _dataStorage = BuildDataStorage();
            _dataStorageTester = CreateTestClass(_dataStorage);
        }

        [TearDown]
        public void TearDown()
        {
            _dataStorage.Clear();
        }
        
        [Test]
        public void Save_Data() => _dataStorageTester.SaveDataTo();

        [Test]
        public void Save_And_Load_Data() => _dataStorageTester.SaveAndLoadData();

        [Test]
        public void Clear_Storage() => _dataStorageTester.ClearStorage();

        protected abstract IDataStorage BuildDataStorage();

        protected abstract DataStorageTester CreateTestClass(IDataStorage dataStorage);
    }
}