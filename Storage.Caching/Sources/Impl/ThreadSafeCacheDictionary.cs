using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Depra.Data.Storage.Caching.Interfaces;

namespace Depra.Data.Storage.Caching.Impl
{
    public class ThreadSafeCacheDictionary : ICacheCollection
    {
        private readonly ConcurrentDictionary<string, object> _cacheMap;

        public int Count => _cacheMap.Count;
        
        public bool Contains(string fileName) => _cacheMap.ContainsKey(fileName);
        
        public object Get(string uri)
        {
            _cacheMap.TryGetValue(uri, out var result);
            return result;
        }

        public object GetOrCreate(string uri, Func<object> createFunc) => _cacheMap.GetOrAdd(uri, createFunc);
        
        public void AddOrUpdate(string uri, object data) =>
            _cacheMap.AddOrUpdate(uri, data, (dataKey, oldData) => data);

        public void Remove(string uri) => _cacheMap.TryRemove(uri, out _);

        public IEnumerable<string> GetAllKeys() => _cacheMap.Select(pair => pair.Key);

        public ThreadSafeCacheDictionary() => _cacheMap = new ConcurrentDictionary<string, object>();
    }
}