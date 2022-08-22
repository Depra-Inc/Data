using System;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Impl;
using Depra.Data.Storage.Serialization;

namespace Depra.Data.Storage.IO
{
    public class FileDataStorageBuilder : IDataStorageBuilder
    {
        private ILocationProvider _location;
        
        public IDataStorageBuilder SetLocation(LocalFileLocation locationProvider)
        {
            _location = locationProvider;
            return this;
        }
        
        public IDataStorage Build()
        {
            if (_location == null)
            {
                throw new NullReferenceException("Data location is null!");
            }
            
            var serializer = new BinarySerializer();
            var dataReader = new FileReader(serializer);
            var dataWriter = new FileWriter(serializer);
            var dataSaver = new DataSaver(dataWriter, _location);
            var dataLoader = new DataLoader(dataReader, _location);
            var storage = new LocalDataStorage(_location, dataSaver, dataLoader);

            return storage;
        }
    }
}