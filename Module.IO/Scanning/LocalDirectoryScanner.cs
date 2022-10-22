using System.Collections.Generic;
using System.IO;
using System.Linq;
using Depra.Data.Domain.Mount;
using Depra.Data.Module.IO.Mount;

namespace Depra.Data.Module.IO.Scanning
{
    public class LocalDirectoryScanner : IDirectoryScanner
    {
        private readonly string _fileFormat;
        private readonly string _fileSearchPattern;
        private readonly LocalDirectory _directory;

        public bool ContainsDataByName(string fileName) =>
            File.Exists(GetFullFilePath(fileName));

        public string GetFullFilePath(string fileName) =>
            Path.Combine(_directory.FullPath, fileName) + _fileFormat;

        public IEnumerable<string> GetAllNames()
        {
            var allFiles = _directory.EnumerateFiles(_fileSearchPattern).ToArray();
            return StripFilenamesExtension(allFiles);
        }

        public LocalDirectoryScanner(LocalDirectory directory, string fileFormat)
        {
            _directory = directory;
            _fileFormat = fileFormat;
            _fileSearchPattern = $"*.{fileFormat}";
        }

        private static IEnumerable<string> StripFilenamesExtension(IReadOnlyList<IFile> files)
        {
            var filenamesWithoutExtension = new string[files.Count];
            for (var i = 0; i < filenamesWithoutExtension.Length; i++)
            {
                filenamesWithoutExtension[i] = Path.GetFileNameWithoutExtension(files[i].Name);
            }

            return filenamesWithoutExtension;
        }
    }
}