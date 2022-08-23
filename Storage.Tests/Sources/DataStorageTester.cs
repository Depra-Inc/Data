using System.Linq;
using Depra.Data.Storage.Api;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    internal class DataStorageTester
    {
        private readonly string _path;
        private readonly IDataStorage _dataStorage;

        public void SaveDataTo()
        {
            var sourceData = new TestData();
            _dataStorage.SaveData(_path, sourceData);
            var allKeys = _dataStorage.GetAllKeys().ToArray();
            var savedData = allKeys.Single(path => path == _path);

            var isStorageContainsSavedData = allKeys.Contains(_path);

            Assert.IsTrue(isStorageContainsSavedData);
            Assert.AreEqual(1, allKeys.Length);
            Assert.AreEqual(_path, savedData);
        }

        public void SaveAndLoadData()
        {
            var sourceData = new TestData();
            _dataStorage.SaveData(_path, sourceData);
            var restoredData = _dataStorage.LoadData(_path, TestData.Empty);

            Assert.AreEqual(1, _dataStorage.GetAllKeys().Count());
            Assert.AreEqual(sourceData.Ident, restoredData.Ident);
            Assert.AreEqual(sourceData.GetType(), restoredData.GetType());
        }

        public void ClearStorage()
        {
            _dataStorage.Clear();
            _dataStorage.SaveData("Test1", new TestData());
            _dataStorage.SaveData("Test2", new TestData());
            _dataStorage.SaveData("Test3", new TestData());

            Assert.AreEqual(3, _dataStorage.GetAllKeys().Count());
            _dataStorage.Clear();
            Assert.AreEqual(0, _dataStorage.GetAllKeys().Count());
        }

        public DataStorageTester(IDataStorage dataStorage, string testPath)
        {
            _path = testPath;
            _dataStorage = dataStorage;
        }
    }
}