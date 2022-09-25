using Depra.Data.Operations.Api;
using Depra.Data.Storage.Internal.Exceptions;

namespace Depra.Data.Operations.Impl
{
    public readonly struct RemoveLocalDataOperation : ICleaningRule
    {
        private readonly string _dataName;
        private readonly IDataDirectory _dataDirectory;
        
        public void Execute()
        {
            _dataDirectory.RemoveData(_dataName);
        }
        
        public RemoveLocalDataOperation(string dataName, IDataDirectory dataDirectory)
        {
            _dataName = dataName;
            _dataDirectory = dataDirectory;
        }
    }
    
    public class LocalCleaningRule : ICleaningRule
    {
        private readonly IDataDirectory _location;

        public void DeleteData(string name)
        {
            if (_location.ContainsDataByName(name) == false)
            {
                throw new InvalidPathException(_location.CombineFullFilePath(name));
            }
            
            _location.RemoveData(name);
        }

        public void Clear()
        {
            var allKeys = _location.GetAllNames();
            foreach (var key in allKeys)
            {
                DeleteData(key);
            }
        }

        public LocalCleaningRule(IDataDirectory location)
        {
            _location = location;
        }
    }
}