using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Depra.Data.Storage.Caching.Memory
{
    public class WaitToFinishMemoryCache<TItem>
    {
        private readonly MemoryCache _cache;
        private readonly ConcurrentDictionary<object, SemaphoreSlim> _locks;

        public async Task<TItem> GetOrCreate(object key, Func<Task<TItem>> createItem)
        {
            if (_cache.TryGetValue(key, out TItem cacheEntry))
            {
                return cacheEntry; // Look for cache key.
            }

            var @lock = _locks.GetOrAdd(key, k => new SemaphoreSlim(1, 1));
            await @lock.WaitAsync();
            try
            {
                if (_cache.TryGetValue(key, out cacheEntry) == false)
                {
                    // Key not in cache, so get data.
                    cacheEntry = await createItem();
                    _cache.Set(key, cacheEntry);
                }
            }
            finally
            {
                @lock.Release();
            }

            return cacheEntry;
        }

        public WaitToFinishMemoryCache()
        {
            _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
            _locks = new ConcurrentDictionary<object, SemaphoreSlim>();
        }
    }
}