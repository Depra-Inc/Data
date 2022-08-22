using System;
using System.IO;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.IO;

namespace Depra.Data.Storage.Tests
{
    internal class FileDataStorageTests : DataStorageTestRunner
    {
        private const string FileFormat = ".test";
        private const string DataUri = nameof(FileDataStorageTests);
        private static readonly string Directory = Path.Combine(Environment.CurrentDirectory, "Storage.IO.Tests");

        protected override IDataStorage BuildDataStorage()
        {
            var location = new LocalFileLocation(Directory, FileFormat, SearchOption.TopDirectoryOnly);
            var fileDataStorage = new FileDataStorageBuilder().SetLocation(location).Build();

            return fileDataStorage;
        }

        protected override DataStorageTester CreateTestClass() => new DataStorageTester(DataUri);
    }
}