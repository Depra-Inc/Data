using Depra.Data.Storage.Api.Writing;
using Depra.Data.Storage.Caching.Interfaces;

namespace Depra.Data.Storage.Caching.Impl
{
    public readonly struct CacheWriter<TData> : ITypedDataWriter<TData>
    {
        private readonly ICacheCollection _cacheCollection;

        public void WriteData(string path, TData data) =>  _cacheCollection.AddOrUpdate(path, data);
        
        public CacheWriter(ICacheCollection cacheCollection) => _cacheCollection = cacheCollection;
    }
}