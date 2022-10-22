using System;
using System.Linq;
using Depra.Data.Domain.Cleaning;
using Depra.Data.Domain.Exceptions;

namespace Depra.Data.Application.Cache
{
    public sealed class CacheCleaner : IDataCleaner
    {
        private readonly IDataCleaningStrategy _cleaningStrategy;

        public void ClearData() => _cleaningStrategy.ClearData();

        public void ClearData(string name) => _cleaningStrategy.ClearData(name);

        public CacheCleaner(CacheDictionary cache, bool safeMode)
        {
            if (safeMode)
            {
                _cleaningStrategy = new SafeStrategy(cache);
            }
            else
            {
                _cleaningStrategy = new UnsafeStrategy(cache);
            }
        }

        private class SafeStrategy : IDataCleaningStrategy
        {
            private readonly CacheDictionary _cache;

            public void ClearData() => _cache.Clear();

            public void ClearData(string name) => _cache.RemoveData(name);

            public SafeStrategy(CacheDictionary cache) => _cache = cache;
        }

        private class UnsafeStrategy : IDataCleaningStrategy
        {
            private readonly CacheDictionary _cache;

            public void ClearData()
            {
                if (_cache.Any() == false)
                {
                    throw new InvalidOperationException("Cache is empty!");
                }

                _cache.Clear();
            }

            public void ClearData(string name)
            {
                if (_cache.ContainsKey(name) == false)
                {
                    throw new InvalidPathException(name);
                }

                _cache.RemoveData(name);
            }

            public UnsafeStrategy(CacheDictionary cache) => _cache = cache;
        }
    }
}