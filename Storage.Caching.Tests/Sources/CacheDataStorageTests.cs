using Depra.Data.Storage.Api;
using Depra.Data.Storage.Caching.Impl;
using Depra.Data.Storage.Caching.Interfaces;
using Depra.Data.Storage.Loading.Impl;
using Depra.Data.Storage.Local;
using Depra.Data.Storage.Middleware.Api;
using Depra.Data.Storage.Saving.Impl;
using Depra.Data.Storage.Tests;

namespace Depra.Data.Storage.Caching.Tests
{
    internal class CacheDataStorageTests : LocaleStorageTests
    {
        protected override string DataUri => "Cache";

        private ICacheCollection _cacheCollection;

        protected override ILocationProvider CreateLocation()
        {
            _cacheCollection = new ThreadSafeCacheDictionary();
            var cacheLocation = new CacheLocation(_cacheCollection);

            return cacheLocation;
        }

        protected override IDataStorage CreateStorage(ILocationProvider location)
        {
            var reader = new CacheReader(_cacheCollection);
            var writer = new CacheWriter(_cacheCollection);
            var saver = new DataSaver(writer, location);
            var loader = new DataLoader(reader, location);
            var storage = new LocalDataStorage(location, saver, loader);

            return storage;
        }
    }
}