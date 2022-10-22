using Depra.Data.Application.Loaders;
using Depra.Data.Application.Savers;
using Depra.Data.Application.Storage;
using Depra.Data.Module.IO.Cleaning;
using Depra.Data.Module.IO.Mount;
using Depra.Data.Module.IO.Reading;
using Depra.Data.Module.IO.Scanning;
using Depra.Data.Module.IO.Writing;
using Depra.Serialization.Application.Serializers;

namespace Depra.Data.Module.IO.Storage
{
    public class LocalFIleDataStorage : DataStorage
    {
        public LocalFIleDataStorage(ISerializer serializer, LocalDirectory directory, string format) : this(serializer,
            new LocalDirectoryScanner(directory, format)) { }

        public LocalFIleDataStorage(ISerializer serializer, LocalDirectoryScanner directoryScanner) : base(
            directoryScanner, 
            new StandardDataSaverBuilder().With(new LocalFileWriter(serializer, directoryScanner)).Build(), 
            new StandardDataLoaderBuilder().With(new LocalFileReader(serializer, directoryScanner)).Build(), 
            new LocalFileCleaner(directoryScanner)) { }
    }
}