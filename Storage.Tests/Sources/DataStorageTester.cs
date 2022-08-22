using System;
using System.Linq;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Extensions;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    internal class DataStorageTester
    {
        private readonly string _path;
        
        public void SaveDataTo(IDataStorage storage)
        {
            storage.Clear();

            var sourceData = new TestData();
            storage.SaveData(_path, sourceData);
            var allKeys = storage.GetAllKeys().ToArray();
            var savedData = allKeys.Single(path => path == _path);

            var isStorageContainsSavedData = allKeys.Contains(_path);
            
            Assert.IsTrue(isStorageContainsSavedData);
            Assert.AreEqual(1, allKeys.Length);
            Assert.AreEqual(_path, savedData);
        }

        public void SaveAndLoadData(IDataStorage storage)
        {
            storage.Clear();

            var sourceData = new TestData();
            storage.SaveData(_path, sourceData);
            var restoredData = storage.LoadData<TestData>(_path, null);

            Assert.AreEqual(1, storage.GetAllKeys().Count());
            Assert.AreEqual(sourceData.Ident, restoredData.Ident);
            Assert.AreEqual(sourceData.GetType(), restoredData.GetType());
        }

        public void ClearStorage(IDataStorage storage)
        {
            storage.Clear();
            storage.SaveData("Test1", Guid.NewGuid());
            storage.SaveData("Test2", Guid.NewGuid());
            storage.SaveData("Test3", Guid.NewGuid());

            Assert.AreEqual(3, storage.GetAllKeys().Count());
            storage.Clear();
            Assert.AreEqual(0, storage.GetAllKeys().Count());
        }

        public DataStorageTester(string testPath)
        {
            _path = testPath;
        }
    }
}