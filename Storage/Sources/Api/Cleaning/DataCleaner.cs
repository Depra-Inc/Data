using Depra.Data.Storage.Internal.Exceptions;

namespace Depra.Data.Storage.Api.Cleaning
{
    public class DataCleaner : IDataCleaner
    {
        private readonly ILocationProvider _location;

        public void DeleteData(string name)
        {
            if (_location.ContainsDataByName(name) == false)
            {
                throw new InvalidPathException(_location.CombineFullFilePath(name));
            }
            
            _location.Remove(name);
        }

        public void Clear()
        {
            var allKeys = _location.ScanFilenames();
            foreach (var key in allKeys)
            {
                DeleteData(key);
            }
        }

        public DataCleaner(ILocationProvider location)
        {
            _location = location;
        }
    }
}