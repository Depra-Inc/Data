using System;
using System.Collections.Generic;
using System.Linq;
using Depra.Data.Serialization.Binary;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Impl;
using Depra.Data.Storage.Internal.Exceptions;
using Depra.Data.Storage.IO;
using FluentAssertions;
using NUnit.Framework;
using static Depra.Data.Storage.Tests.AssertionExtensions;

namespace Depra.Data.Storage.Tests
{
    [TestFixture]
    internal class FileDataStorageTests
    {
        private const string FileFormat = ".test";
        private const string FolderName = "Storage.IO.Tests";

        private static readonly string[] FreeDataNames = { "FileData_1", "FileData_2", "FileData_3" };
        private static readonly string[] ExistedDataNames = { "ExistedData_1", "ExistedData_2", "ExistedData_3" };
        private static readonly string DirectoryPath = System.IO.Path.Combine(Environment.CurrentDirectory, FolderName);

        private static readonly ILocationProvider Location =
            new LocalFileLocation(DirectoryPath, FileFormat, System.IO.SearchOption.TopDirectoryOnly);

        private IDataStorage _fileDataStorage;

        [SetUp]
        public void SetUp()
        {
            _fileDataStorage = BuildDataStorage();

            FreeResources();
            CreateResourcesForTest();
        }

        [TearDown]
        public void TearDown()
        {
            FreeResources();
        }

        [Test]
        public void WhenSavingData_AndStorageContainKey_ThenSuccess()
        {
            // Arrange.
            var data = new TestData();
            var randomExistedDataName = ExistedDataNames.Random();
            var fullFilePath = Location.CombineFullFilePath(randomExistedDataName);

            // Act.
            _fileDataStorage.SaveData(randomExistedDataName, data);

            // Assert.
            _fileDataStorage.GetAllKeys().Should().Contain(randomExistedDataName);
            File(fullFilePath).Should().Exist();
        }

        [Test]
        public void WhenSavingData_AndStorageDoesntContainKey_ThenThrowError()
        {
            // Arrange.
            var data = new TestData();
            var randomNonExistedDataName = FreeDataNames.Random();
            var fullFilePath = Location.CombineFullFilePath(randomNonExistedDataName);

            // Act.
            _fileDataStorage.SaveData(randomNonExistedDataName, data);

            // Assert.
            _fileDataStorage.GetAllKeys().Should().Contain(randomNonExistedDataName);
            File(fullFilePath).Should().Exist();
        }

        [Test]
        public void WhenLoadingData_AndStorageContainKey_ThenSuccess()
        {
            // Arrange.
            var data = TestData.Empty;
            var randomExistingDataName = ExistedDataNames.Random();

            // Act.
            var restoredData = _fileDataStorage.LoadData(randomExistingDataName, data);

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
            var restoredData = _fileDataStorage.LoadData(randomNonExistedDataName, data);

            // Assert.
            restoredData.Should().Be(data);
        }

        [Test]
        public void WhenDeletingData_AndStorageContainKey_ThenStorageDoesntContainKey()
        {
            // Arrange.
            var randomExistedDataName = ExistedDataNames.Random();

            // Act.
            _fileDataStorage.DeleteData(randomExistedDataName);

            // Assert.
            _fileDataStorage.GetAllKeys().Should().NotContain(randomExistedDataName);
        }

        [Test]
        public void WhenClearStorage_AndStorageNonEmpty_ThenStorageEmpty()
        {
            // Act.
            WarmUpData(FreeDataNames);

            // Assert.
            _fileDataStorage.Clear();
            _fileDataStorage.GetAllKeys().Should().BeEmpty();
        }

        [Test]
        public void WhenSavingType_AndTypeNonSupportedByStorage_ThenThrowError()
        {
            // Arrange.
            const string data = "stringData";

            // Act.
            void SavingHandler() => _fileDataStorage.SaveData(FreeDataNames.Random(), data);

            // Assert.
            Assert.Throws<NotSupportedTypeException>(SavingHandler);
        }

        [Test]
        public void WhenLoadingType_AndTypeNonSupportedByStorage_ThenThrowError()
        {
            // Arrange.
            const string data = "stringData";

            // Act.
            void LoadingHandler() => _fileDataStorage.LoadData(FreeDataNames.Random(), data);

            // Assert.
            Assert.Throws<NotSupportedTypeException>(LoadingHandler);
        }

        [Test]
        public void WhenDeletingData_AndStorageDoesntContainData_ThenThrowError()
        {
            // Arrange.
            const string dataKey = "Non-existent data";

            // Act.
            void RemovingHandler() => _fileDataStorage.DeleteData(dataKey);

            // Assert.
            Assert.Throws<InvalidPathException>(RemovingHandler);
        }

        private static IDataStorage BuildDataStorage()
        {
            var serializer = new BinarySerializer();
            var fileDataStorage = StandardDataStorageBuilder
                .Configure(Location, builder => builder
                    .AddLoader(loader => loader.AddReader(new FileReader<TestData>(Location, serializer)))
                    .AddSaver(saver => saver.AddWriter(new FileWriter<TestData>(Location, serializer))))
                .Build();

            return fileDataStorage;
        }

        private void WarmUpData(IEnumerable<string> dataNames)
        {
            foreach (var dataName in dataNames)
            {
                var data = new TestData();
                _fileDataStorage.SaveData(dataName, data);
            }
        }

        private void CreateResourcesForTest() => WarmUpData(ExistedDataNames);

        private static void FreeResources()
        {
            var filesForDelete = new List<string>();
            filesForDelete.AddRange(FreeDataNames);
            filesForDelete.AddRange(ExistedDataNames);

            foreach (var fullFilePath in filesForDelete.Select(fileName => Location.CombineFullFilePath(fileName)))
            {
                System.IO.File.Delete(fullFilePath);
            }
        }
    }
}