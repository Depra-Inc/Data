using System.Collections.Generic;
using Depra.Data.Domain.Cleaning;
using Depra.Data.Domain.Loading;
using Depra.Data.Domain.Mount;
using Depra.Data.Domain.Saving;
using Depra.Data.Domain.Storage;

namespace Depra.Data.Application.Storage
{
    public class DataStorage : IDataStorage
    {
        private readonly IDataSaver _dataSaver;
        private readonly IDataLoader _dataLoader;
        private readonly IDataCleaner _dataCleaner;
        private readonly IDirectoryScanner _directoryScanner;

        public IEnumerable<string> GetAllKeys() => _directoryScanner.GetAllNames();

        public void SaveData<TData>(string dataKey, TData data) => _dataSaver.SaveData(dataKey, data);

        public TData LoadData<TData>(string dataKey, TData defaultValue) => _dataLoader.LoadData(dataKey, defaultValue);

        public void ClearData(string dataKey) => _dataCleaner.ClearData(dataKey);

        public void ClearData() => _dataCleaner.ClearData();

        public DataStorage(IDirectoryScanner directoryScanner, IDataSaver saver, IDataLoader loader,
            IDataCleaner dataCleaner)
        {
            _dataSaver = saver;
            _dataLoader = loader;
            _directoryScanner = directoryScanner;
            _dataCleaner = dataCleaner;
        }
    }
}