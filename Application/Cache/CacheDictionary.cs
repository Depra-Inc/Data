using System;
using System.Collections.Concurrent;

namespace Depra.Data.Application.Cache
{
    public sealed class CacheDictionary : ConcurrentDictionary<string, object>
    {
        public TData GetData<TData>(string dataName)
        {
            object Factory() => null;
            var data = (TData) GetOrAdd(dataName, (Func<object>) Factory);

            return data;
        }

        public void SetData<TData>(string dataName, TData data) =>
            AddOrUpdate(dataName, data, (dataKey, oldData) => data);

        public void RemoveData(string fileName) => TryRemove(fileName, out _);
    }
}