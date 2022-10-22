using System;
using Depra.Data.Domain.Reading;

namespace Depra.Data.Application.Cache
{
    public sealed class CacheReader : IGenericDataReader
    {
        private readonly bool _safeMode;
        private readonly CacheDictionary _cache;
        private readonly CacheScanner _cacheScanner;

        public bool CanRead<TData>(string dataName) => _cacheScanner.ContainsDataByName(dataName);

        public TData ReadData<TData>(string dataName)
        {
            if (_cacheScanner.ContainsDataByName(dataName) == false && _safeMode == false)
            {
                throw new InvalidOperationException();
            }

            var data = _cache.GetData<TData>(dataName);
            data = data == null && _safeMode ? default : data;

            return data;
        }

        public CacheReader(CacheDictionary cache, CacheScanner cacheScanner, bool safeMode)
        {
            _cache = cache;
            _safeMode = safeMode;
            _cacheScanner = cacheScanner;
        }
    }
}