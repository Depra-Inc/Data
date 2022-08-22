using System;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Caching.Interfaces;
using Depra.Data.Storage.Impl;

namespace Depra.Data.Storage.Caching.Impl
{
    public class CacheDataStorageBuilder : IDataStorageBuilder
    {
        private ICacheCollection _cache;
        private ILocationProvider _location;
        
        public CacheDataStorageBuilder SetLocation(CacheLocation location)
        {
            _location = location;
            return this;
        }

        public IDataStorageBuilder SetCache(ICacheCollection cache)
        {
            _cache = cache;
            return this;
        }
        
        public IDataStorage Build()
        {
            if (_location == null)
            {
                throw new NullReferenceException("Data location is null!");
            }
            
            var reader = new CacheReader(_cache);
            var writer = new CacheWriter(_cache);
            var saver = new DataSaver(writer, _location);
            var loader = new DataLoader(reader, _location);
            var storage = new LocalDataStorage(_location, saver, loader);

            return storage;
        }
    }
}