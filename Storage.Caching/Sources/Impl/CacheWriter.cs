using Depra.Data.Storage.Caching.Interfaces;
using Depra.Data.Storage.Middleware.Api;

namespace Depra.Data.Storage.Caching.Impl
{
    public readonly struct CacheWriter : IDataWriter
    {
        private readonly ICacheCollection _cacheCollection;

        public void WriteData(string uri, object data) => _cacheCollection.AddOrUpdate(uri, data);

        public void ClearData(string uri) => _cacheCollection.Remove(uri);

        public CacheWriter(ICacheCollection cacheCollection) => _cacheCollection = cacheCollection;
    }
}