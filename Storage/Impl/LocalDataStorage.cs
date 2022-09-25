using System.Collections.Generic;
using Depra.Data.Operations.Api;
using Depra.Data.Operations.Impl;
using Depra.Data.Storage.Api;

namespace Depra.Data.Storage.Impl
{
    public class LocalDataStorage : IDataStorage
    {
        private readonly IDataSaver _dataSaver;
        private readonly IDataLoader _dataLoader;
        private readonly ICleaningRule _cleaningRule;
        private readonly IDataDirectory _location;

        public IEnumerable<string> GetAllKeys() => _location.GetAllNames();

        public void SaveData<TData>(string name, TData data) => _dataSaver.SaveData(name, data);
        
        public TData LoadData<TData>(string name, TData defaultValue) => _dataLoader.LoadData(name, defaultValue);

        public void DeleteData(string name) => _cleaningRule.DeleteData(name);

        public void Clear() => _cleaningRule.Clear();

        public LocalDataStorage(IDataDirectory location, IDataSaver saver, IDataLoader loader)
        {
            _dataSaver = saver;
            _dataLoader = loader;
            _location = location;
            _cleaningRule = new LocalCleaningRule(location);
        }
    }
}