using System.Collections.Generic;
using System.Linq;
using Depra.Data.Domain.Mount;

namespace Depra.Data.Application.Cache
{
    public class CacheScanner : IDirectoryScanner
    {
        private readonly CacheDictionary _cacheDictionary;

        public bool ContainsDataByName(string fileName) => _cacheDictionary.TryGetValue(fileName, out _);

        public string GetFullFilePath(string fileName) => fileName;

        public IEnumerable<string> GetAllNames() => _cacheDictionary.Select(pair => pair.Key);

        public CacheScanner(CacheDictionary cacheDictionary) => _cacheDictionary = cacheDictionary;
    }
}