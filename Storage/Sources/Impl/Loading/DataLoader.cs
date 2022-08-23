using System;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.Impl.Loading
{
    public readonly struct DataLoader : IDataLoader
    {
        private readonly ILocationProvider _location;
        private readonly DataReaderByType _dataReaders;

        public TData LoadData<TData>(string name, TData defaultValue)
        {
            if (_location.ContainsDataByName(name) == false)
            {
                return defaultValue;
            }

            if (TryResolveDataReader<TData>(out var dataReader) == false)
            {
                throw new TypeAccessException();
            }

            var data = ReadDataWithReader(dataReader, name, defaultValue);
            return data;
        }

        public DataLoader(ILocationProvider location, DataReaderByType dataReaders)
        {
            _location = location;
            _dataReaders = dataReaders;
        }

        private TData ReadDataWithReader<TData>(ITypedDataReader<TData> reader, string name, TData defaultValue)
        {
            var uri = _location.CombineFullFilePath(name);
            var readData = reader.ReadData(uri);
            if (readData == null)
            {
                readData = defaultValue;
            }
            
            return readData;
        }

        private bool TryResolveDataReader<TData>(out ITypedDataReader<TData> reader)
        {
             reader = _dataReaders.GetValue<TData>();
             return reader != null;
        }
    }
}