using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Depra.Data.Operations.Api;

namespace Depra.Data.Operations.Cache.Bridge
{
    public class CacheLocation : IDataDirectory
    {
        private readonly ConcurrentDictionary<string, object> _cache;
        
        public TData GetData<TData>(string dataName)
        {
            object Factory() => null;
            var data = (TData)_cache.GetOrAdd(dataName, (Func<object>)Factory);

            return data;
        }

        public void SetData<TData>(string dataName, TData data)
        {
            _cache.AddOrUpdate(dataName, data, (dataKey, oldData) => data);
        }

        public void RemoveData(string fileName) => _cache.TryRemove(fileName, out _);
        
        public bool ContainsDataByName(string fileName) => _cache.TryGetValue(fileName, out _);

        public string CombineFullFilePath(string fileName) => fileName;

        public IEnumerable<string> GetAllNames() => _cache.Select(pair => pair.Key);
        
        public CacheLocation()
        {
            _cache = new ConcurrentDictionary<string, object>();
        }
    }
}