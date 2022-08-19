using System.IO;
using Depra.Data.Persistent.Api;

namespace Depra.Data.Persistent.Locations
{
    public readonly struct LocalFileLocation : ILocationProvider
    {
        private readonly string _format;
        private readonly string _directoryPath;

        public bool ContainsFile(string fileName) => File.Exists(CombineFullFilePathWithoutFormat(fileName));

        public string CombineFullFilePath(string fileName) => Path.Combine(_directoryPath, fileName) + _format;

        public LocalFileLocation(string directoryPath, string format) : this()
        {
            _format = format;
            _directoryPath = directoryPath;

            TryCreateDirectory();
        }

        private string CombineFullFilePathWithoutFormat(string fileName) => Path.Combine(_directoryPath, fileName);

        private void TryCreateDirectory()
        {
            if (Directory.Exists(_directoryPath) == false)
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }
    }
}