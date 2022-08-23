using System.Collections.Generic;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Caching.Interfaces;

namespace Depra.Data.Storage.Caching.Impl
{
    public class CacheLocation : ILocationProvider
    {
        private readonly ICacheCollection _cache;

        public void Remove(string fileName) => _cache.Remove(fileName);

        public IEnumerable<string> ScanFilenames() => _cache.GetAllKeys();

        public bool ContainsDataByName(string fileName) => _cache.Get(CombineFullFilePath(fileName)) != null;

        public string CombineFullFilePath(string fileName) => fileName;

        public CacheLocation(ICacheCollection cache)
        {
            _cache = cache;
        }
    }
}