using System.Linq;
using Depra.Data.Storage.Api.Reading;
using Depra.Data.Storage.Caching.Interfaces;

namespace Depra.Data.Storage.Caching.Impl
{
    public readonly struct CacheReader<TData> : IDataReader<TData>
    {
        private readonly ICacheCollection _cacheCollection;

        public bool CanRead(string dataName) => _cacheCollection.GetAllKeys().Contains(dataName);

        public TData ReadData(string dataName) => (TData)_cacheCollection.GetOrCreate(dataName, () => null);
        
        public CacheReader(ICacheCollection cacheCollection) => _cacheCollection = cacheCollection;
    }
}