using Depra.Data.Storage.Api;
using Depra.Data.Storage.Caching.Interfaces;

namespace Depra.Data.Storage.Caching.Impl
{
    public readonly struct CacheReader : IDataReader
    {
        private readonly ICacheCollection _cacheCollection;

        public object ReadData(string path) => _cacheCollection.GetOrCreate(path, () => null);

        public CacheReader(ICacheCollection cacheCollection) => _cacheCollection = cacheCollection;
    }
}