using System.IO;

namespace Depra.Data.Operations.IO.Bridge
{
    internal readonly struct LocalFileSearcher
    {
        private readonly string _searchPattern;
        private readonly SearchOption _searchOption;

        public string[] GetAllFiles(string directoryPath)
        {
            var result = Directory.GetFiles(directoryPath, _searchPattern, _searchOption);
            return result;
        }
        
        public LocalFileSearcher(string pattern, SearchOption option)
        {
            _searchOption = option;
            _searchPattern = pattern;
        }
    }
}