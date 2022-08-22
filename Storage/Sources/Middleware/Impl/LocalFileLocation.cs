using System.Collections.Generic;
using System.IO;
using Depra.Data.Storage.Middleware.Api;

namespace Depra.Data.Storage.Middleware.Impl
{
    public readonly struct LocalFileLocation : ILocationProvider
    {
        private readonly string _format;
        private readonly string _directoryPath;
        private readonly LocalFileSearcher _fileSearcher;

        public IEnumerable<string> ScanFilenames()
        {
            var filenames = _fileSearcher.GetAllFiles(_directoryPath);
            return StripFilenamesExtension(filenames);
        }

        public bool ContainsDataByName(string key) =>
            File.Exists(CombineFullFilePath(key));

        public string CombineFullFilePath(string key) =>
            Path.Combine(_directoryPath, key) + _format;

        public LocalFileLocation(string directoryPath, string format, SearchOption searchOption) : this()
        {
            _format = format;
            _directoryPath = directoryPath;

            var searchPattern = "*" + format;
            _fileSearcher = new LocalFileSearcher(searchPattern, searchOption);

            TryCreateDirectory();
        }

        private void TryCreateDirectory()
        {
            if (Directory.Exists(_directoryPath) == false)
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }

        private static IEnumerable<string> StripFilenamesExtension(IReadOnlyList<string> filenames)
        {
            var filenamesWithoutExtension = new string[filenames.Count];
            for (var i = 0; i < filenamesWithoutExtension.Length; i++)
            {
                filenamesWithoutExtension[i] = Path.GetFileNameWithoutExtension(filenames[i]);
            }

            return filenamesWithoutExtension;
        }
    }
}