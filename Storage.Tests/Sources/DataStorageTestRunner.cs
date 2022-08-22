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
            _dataStorageTester = CreateTestClass();
        }
        
        [Test]
        public void Save_Data() => _dataStorageTester.SaveDataTo(_dataStorage);

        [Test]
        public void Save_And_Load_Data() => _dataStorageTester.SaveAndLoadData(_dataStorage);

        [Test]
        public void Clear_Storage() => _dataStorageTester.ClearStorage(_dataStorage);

        protected abstract IDataStorage BuildDataStorage();

        protected abstract DataStorageTester CreateTestClass();
    }
}