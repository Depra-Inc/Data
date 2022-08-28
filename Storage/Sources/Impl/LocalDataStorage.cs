using System.Collections.Generic;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Cleaning;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Saving;

namespace Depra.Data.Storage.Impl
{
    public class LocalDataStorage : IDataStorage
    {
        private readonly IDataSaver _dataSaver;
        private readonly IDataLoader _dataLoader;
        private readonly IDataCleaner _dataCleaner;
        private readonly ILocationProvider _location;

        public IEnumerable<string> GetAllKeys() => _location.ScanFilenames();

        public void SaveData<TData>(string name, TData data) => _dataSaver.SaveData(name, data);
        
        public TData LoadData<TData>(string name, TData defaultValue) => _dataLoader.LoadData(name, defaultValue);

        public void DeleteData(string name) => _dataCleaner.DeleteData(name);

        public void Clear() => _dataCleaner.Clear();

        public LocalDataStorage(ILocationProvider location, IDataSaver saver, IDataLoader loader)
        {
            _dataSaver = saver;
            _dataLoader = loader;
            _location = location;
            _dataCleaner = new DataCleaner(location);
        }
    }
}