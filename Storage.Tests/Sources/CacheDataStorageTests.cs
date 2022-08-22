using Depra.Data.Storage.Api;
using Depra.Data.Storage.Caching.Impl;

namespace Depra.Data.Storage.Tests
{
    internal class CacheDataStorageTests : DataStorageTestRunner
    {
        private const string DataUri = "Cache";

        protected override IDataStorage BuildDataStorage()
        {
            var cache = new ThreadSafeCacheDictionary();
            var storage = new CacheDataStorageBuilder()
                .SetLocation(new CacheLocation(cache))
                .SetCache(cache)
                .Build();

            return storage;
        }

        protected override DataStorageTester CreateTestClass() => new DataStorageTester(DataUri);
    }
}