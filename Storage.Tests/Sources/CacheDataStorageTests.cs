using Depra.Data.Storage.Api;
using Depra.Data.Storage.Caching.Impl;
using Depra.Data.Storage.Impl;

namespace Depra.Data.Storage.Tests
{
    internal class CacheDataStorageTests : DataStorageTestRunner
    {
        protected override string[] FreeDataNames { get; } = { "Cache_1", "Cache_2", "Cache_3" };

        protected override IDataStorage BuildDataStorage()
        {
            var cache = new ThreadSafeCacheDictionary();
            var storage = new StandardDataStorageBuilder()
                .SetLocation(new CacheLocation(cache))
                .SetSaver(saver => saver.AddWriter(new CacheWriter<TestData>(cache)))
                .SetLoader(loader => loader.AddReader(new CacheReader<TestData>(cache)))
                .Build();

            return storage;
        }

        protected override void CreateResourcesForTest() => WarmUpData(ExistedDataNames);
    }
}