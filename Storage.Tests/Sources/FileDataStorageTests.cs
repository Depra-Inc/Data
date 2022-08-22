using System;
using System.IO;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Loading.Impl;
using Depra.Data.Storage.Local;
using Depra.Data.Storage.Middleware.Api;
using Depra.Data.Storage.Middleware.Impl;
using Depra.Data.Storage.Saving.Impl;

namespace Depra.Data.Storage.Tests
{
    internal class FileDataStorageTests : LocaleStorageTests
    {
        private const string FileFormat = ".test";
        private static readonly string Directory = Path.Combine(Environment.CurrentDirectory, "Storage.Tests");

        protected override string DataUri => nameof(FileDataStorageTests);

        protected override ILocationProvider CreateLocation() =>
            new LocalFileLocation(Directory, FileFormat, SearchOption.TopDirectoryOnly);

        protected override IDataStorage CreateStorage(ILocationProvider location)
        {
            var serializer = new BinarySerializer();
            var dataReader = new FileReader(serializer);
            var dataWriter = new FileWriter(serializer);
            var dataSaver = new DataSaver(dataWriter, location);
            var dataLoader = new DataLoader(dataReader, location);
            var storage = new LocalDataStorage(location, dataSaver, dataLoader);

            return storage;
        }
    }
}