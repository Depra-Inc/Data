using System;
using System.Linq;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Extensions;
using Depra.Data.Storage.Middleware.Api;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    public abstract class LocaleStorageTests
    {
        private IDataStorage _storage;
        private ILocationProvider _location;

        protected abstract string DataUri { get; }

        [SetUp]
        public void SetUp()
        {
            _location = CreateLocation();
            _storage = CreateStorage(_location);
        }

        [TearDown]
        public void TearDown()
        {
            _storage.Clear();
        }

        protected abstract ILocationProvider CreateLocation();

        protected abstract IDataStorage CreateStorage(ILocationProvider location);

        [Test]
        public void Save_Data()
        {
            _storage.Clear();

            var sourceData = new TestData();
            _storage.SaveData(DataUri, sourceData);
            var allKeys = _storage.GetAllKeys().ToArray();
            var savedData = allKeys.Single(path => path == DataUri);

            Assert.IsTrue(allKeys.Contains(DataUri));
            Assert.AreEqual(1, allKeys.Length);
            Assert.AreEqual(DataUri, savedData);
        }

        [Test]
        public void Save_And_Load_Data()
        {
            _storage.Clear();

            var sourceData = new TestData();
            _storage.SaveData(DataUri, sourceData);
            var restoredData = _storage.LoadData<TestData>(DataUri, null);

            Assert.AreEqual(1, _storage.GetAllKeys().Count());
            Assert.AreEqual(sourceData.Ident, restoredData.Ident);
            Assert.AreEqual(sourceData.GetType(), restoredData.GetType());
        }

        [Test]
        public void Clear_Storage()
        {
            _storage.Clear();
            _storage.SaveData("Test1", Guid.NewGuid());
            _storage.SaveData("Test2", Guid.NewGuid());
            _storage.SaveData("Test3", Guid.NewGuid());

            Assert.AreEqual(3, _storage.GetAllKeys().Count());
            _storage.Clear();
            Assert.AreEqual(0, _storage.GetAllKeys().Count());
        }
    }
}