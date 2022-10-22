using Depra.Data.Domain.Writing;

namespace Depra.Data.Application.Cache
{
    public sealed class CacheWriter : IGenericDataWriter
    {
        private readonly CacheDictionary _cache;

        public void WriteData<TData>(string dataName, TData data) => _cache.SetData(dataName, data);

        public CacheWriter(CacheDictionary cache) => _cache = cache;
    }
}