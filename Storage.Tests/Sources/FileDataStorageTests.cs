using System;
using System.IO;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Impl;
using Depra.Data.Storage.IO;
using Depra.Data.Storage.Serialization;

namespace Depra.Data.Storage.Tests
{
    internal class FileDataStorageTests : DataStorageTestRunner
    {
        private const string FileFormat = ".test";
        private const string DataUri = nameof(FileDataStorageTests);
        private static readonly string Directory = Path.Combine(Environment.CurrentDirectory, "Storage.IO.Tests");

        protected override IDataStorage BuildDataStorage()
        {
            var serializer = new BinarySerializer();
            var location = new LocalFileLocation(Directory, FileFormat, SearchOption.TopDirectoryOnly);
            var fileDataStorage = new StandardDataStorageBuilder()
                .SetLocation(location)
                .SetLoader(loader => loader
                    .AddReader(new FileReader<TestData>(serializer)))
                .SetSaver(saver => saver
                    .AddWriter(new FileWriter<TestData>(serializer)))
                .Build();

            return fileDataStorage;
        }

        protected override DataStorageTester CreateTestClass(IDataStorage dataStorage) => new(dataStorage, DataUri);
    }
}