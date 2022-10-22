using System.IO;
using Depra.Data.Domain.Cleaning;
using Depra.Data.Domain.Exceptions;
using Depra.Data.Domain.Mount;

namespace Depra.Data.Module.IO.Cleaning
{
    public class LocalFileCleaner : IDataCleaner
    {
        private readonly IDirectoryScanner _directoryScanner;

        public void ClearData()
        {
            var allKeys = _directoryScanner.GetAllNames();
            foreach (var key in allKeys)
            {
                ClearData(key);
            }
        }

        public void ClearData(string name)
        {
            if (_directoryScanner.ContainsDataByName(name) == false)
            {
                throw new InvalidPathException(_directoryScanner.GetFullFilePath(name));
            }

            var fullPath = _directoryScanner.GetFullFilePath(name);
            File.Delete(fullPath);
        }

        public LocalFileCleaner(IDirectoryScanner directoryScanner) => _directoryScanner = directoryScanner;
    }
}