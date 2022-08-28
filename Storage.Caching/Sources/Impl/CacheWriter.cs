using Depra.Data.Storage.Api.Writing;
using Depra.Data.Storage.Caching.Interfaces;

namespace Depra.Data.Storage.Caching.Impl
{
    public readonly struct CacheWriter<TData> : IDataWriter<TData>
    {
        private readonly ICacheCollection _cacheCollection;

        public void WriteData(string dataName, TData data) =>  _cacheCollection.AddOrUpdate(dataName, data);
        
        public CacheWriter(ICacheCollection cacheCollection) => _cacheCollection = cacheCollection;
    }
}