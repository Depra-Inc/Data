using System.Collections.Generic;
using Depra.Data.Storage.Caching.Interfaces;
using Depra.Data.Storage.Middleware.Api;

namespace Depra.Data.Storage.Caching.Impl
{
    public class CacheLocation : ILocationProvider
    {
        private readonly ICacheCollection _cache;

        public IEnumerable<string> ScanFilenames() => _cache.GetAllKeys();

        public bool ContainsDataByName(string key) => _cache.Get(CombineFullFilePath(key)) != null;

        public string CombineFullFilePath(string key) => key;

        public CacheLocation(ICacheCollection cache)
        {
            _cache = cache;
        }
    }
}