using System;
using System.IO;
using Depra.Data.Serialization.Binary;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Impl;
using Depra.Data.Storage.IO;
using NUnit.Framework;

namespace Depra.Data.Storage.Tests
{
    internal class FileDataStorageTests : DataStorageTestsBase
    {
        private const string FileFormat = ".test";
        private const string FolderName = "Storage.IO.Tests";

        private static readonly string DirectoryPath = Path.Combine(Environment.CurrentDirectory, FolderName);

        private static readonly ILocationProvider Location =
            new LocalFileLocation(DirectoryPath, FileFormat, SearchOption.TopDirectoryOnly);

        protected override string[] FreeDataNames { get; } = { "FileData_1", "FileData_2", "FileData_3" };

        protected override IDataStorage BuildDataStorage()
        {
            var serializer = new BinarySerializer();
            var fileDataStorage = StandardDataStorageBuilder
                .Configure(Location, builder => builder
                    .AddLoader(loader => loader.AddReader(new FileReader<TestData>(Location, serializer)))
                    .AddSaver(saver => saver.AddWriter(new FileWriter<TestData>(Location, serializer))))
                .Build();

            return fileDataStorage;
        }

        protected override void SpecificDataExistenceCheck(string dataName)
        {
            var fullDataPath = CombineFullPath(dataName);
            var isFileExisting = File.Exists(fullDataPath);

            Assert.IsTrue(isFileExisting);
        }

        protected override void CreateResourcesForTest()
        {
            foreach (var existedFileName in Directory.GetFiles(DirectoryPath))
            {
                File.Delete(existedFileName);
            }
        }

        protected override void FreeResources()
        {
            foreach (var existedDataName in ExistedDataNames)
            {
                File.Delete(existedDataName);
            }
        }

        private static string CombineFullPath(string dataName) => Path.Combine(DirectoryPath, dataName) + FileFormat;
    }
}