using Depra.Data.Storage.Api.Reading;
using Depra.Data.Storage.Caching.Interfaces;

namespace Depra.Data.Storage.Caching.Impl
{
    public readonly struct CacheReader<TData> : ITypedDataReader<TData>
    {
        private readonly ICacheCollection _cacheCollection;

        public TData ReadData(string path) => (TData)_cacheCollection.GetOrCreate(path, () => null);
        
        public CacheReader(ICacheCollection cacheCollection) => _cacheCollection = cacheCollection;
    }
}