using System;
using System.Collections.Generic;

namespace Depra.Data.Storage.Caching.Interfaces
{
    public interface ICacheCollection
    {
        object Get(string uri);
        
        object GetOrCreate(string uri, Func<object> createFunc);
        
        void AddOrUpdate(string uri, object data);

        void Remove(string uri);

        IEnumerable<string> GetAllKeys();
    }
}