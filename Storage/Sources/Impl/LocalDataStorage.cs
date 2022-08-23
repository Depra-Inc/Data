using System.Collections.Generic;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Saving;

namespace Depra.Data.Storage.Impl
{
    public class LocalDataStorage : IDataStorage
    {
        private readonly IDataSaver _dataSaver;
        private readonly IDataLoader _dataLoader;
        private readonly ILocationProvider _location;

        public IEnumerable<string> GetAllKeys() => _location.ScanFilenames();

        public void SaveData<TData>(string name, TData data) => _dataSaver.SaveData(name, data);
        
        public TData LoadData<TData>(string name, TData defaultValue) => _dataLoader.LoadData(name, defaultValue);

        public void RemoveData(string name) => _dataSaver.RemoveData(name);

        public void Clear()
        {
            var allKeys = _location.ScanFilenames();
            foreach (var key in allKeys)
            {
                RemoveData(key);
            }
        }

        public LocalDataStorage(ILocationProvider location, IDataSaver saver, IDataLoader loader)
        {
            _dataSaver = saver;
            _dataLoader = loader;
            _location = location;
        }
    }
}