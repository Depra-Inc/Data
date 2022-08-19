using System;
using System.Linq;
using Depra.Data.Persistent.Api;
using Depra.Data.Persistent.Extensions;
using Depra.Data.Persistent.Locations;
using Depra.Data.Persistent.Readers;
using Depra.Data.Persistent.Serializers;
using Depra.Data.Persistent.Storages;
using Depra.Data.Persistent.Writers;
using NUnit.Framework;

namespace Depra.Data.Persistent.Tests
{
    internal class PersistentDataStorageTests
    {
        private const string FileName = nameof(PersistentLocalStorage);
        private const string FileFormat = ".test";
        private const string Directory = "Persistent.Tests";

        private ILocationProvider _location;
        private IPersistentLocalStorage _storage;

        [SetUp]
        public void Setup()
        {
            var serializer = new BinarySerializer();
            var dataReader = new FileReader(serializer);
            var dataWriter = new FileWriter(serializer);
            _location = new LocalFileLocation(Directory, FileFormat);
            _storage = new PersistentLocalStorage(_location, dataReader, dataWriter);
        }

        [TearDown]
        public void TearDown()
        {
            _storage.Remove(FileName);
        }

        [Test]
        public void Save_Data()
        {
            var sourceData = new TestData();
            _storage.SaveData(FileName, sourceData);
            var savedData = _storage.AllIdents.Single(path => path == FileName);
            
            Assert.IsTrue(_storage.AllIdents.Contains(FileName));
            Assert.AreEqual(FileName, savedData);
        }

        [Test]
        public void Save_And_Load_Data()
        {
            var sourceData = new TestData();
            _storage.SaveData(FileName, sourceData);
            var restoredData = _storage.LoadData<TestData>(FileName, null);

            Assert.IsNotNull(restoredData);
            Assert.AreEqual(sourceData.Ident, restoredData.Ident);
        }

        [Test]
        public void Clear_Storage()
        {
            _storage.SaveData("Test1", Guid.NewGuid());
            _storage.SaveData("Test2", Guid.NewGuid());
            _storage.SaveData("Test3", Guid.NewGuid());
            
            Assert.AreEqual(3, _storage.AllIdents.Count());
            
            _storage.Clear();
            
            Assert.AreEqual(0, _storage.AllIdents.Count());
        }
    }
}