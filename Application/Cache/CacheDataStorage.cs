using Depra.Data.Application.Loaders;
using Depra.Data.Application.Savers;
using Depra.Data.Application.Storage;

namespace Depra.Data.Application.Cache
{
    public sealed class CacheDataStorage : DataStorage
    {
        public CacheDataStorage(bool safeMode) : this(new CacheDictionary(), safeMode) { }

        public CacheDataStorage(CacheDictionary cache, bool safeMode) :
            this(cache, new CacheScanner(cache), safeMode) { }

        private CacheDataStorage(CacheDictionary cache, CacheScanner cacheScanner, bool safeMode) : base(cacheScanner,
            new StandardDataSaverBuilder().With(new CacheWriter(cache)).Build(),
            new StandardDataLoaderBuilder().With(new CacheReader(cache, cacheScanner, true)).Build(),
            new CacheCleaner(cache, safeMode)) { }
    }
}