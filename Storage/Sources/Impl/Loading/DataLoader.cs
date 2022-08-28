using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Reading;
using Depra.Data.Storage.Internal.Exceptions;

namespace Depra.Data.Storage.Impl.Loading
{
    public class DataLoader : IDataLoader
    {
        private readonly DataReaderByType _dataReaders;

        public void AddReader<TData>(IDataReader<TData> reader)
        {
            if (TryResolveDataReader<TData>(out var alreadyRegisteredReader))
            {
                throw new AlreadyRegisteredException(alreadyRegisteredReader.GetType());
            }
            
            _dataReaders.SetValue(reader);
        }

        public TData LoadData<TData>(string name, TData defaultValue)
        {
            if (TryResolveDataReader<TData>(out var dataReader) == false)
            {
                throw new NotSupportedTypeException(typeof(TData));
            }

            return dataReader.CanRead(name)
                ? ReadDataWithReader(dataReader, name, defaultValue)
                : defaultValue;
        }

        public DataLoader(DataReaderByType dataReaders)
        {
            _dataReaders = dataReaders;
        }

        private static TData ReadDataWithReader<TData>(IDataReader<TData> reader, string name, TData defaultValue)
        {
            var readData = reader.ReadData(name);
            if (readData == null)
            {
                readData = defaultValue;
            }

            return readData;
        }

        private bool TryResolveDataReader<TData>(out IDataReader<TData> reader)
        {
            reader = _dataReaders.GetValue<TData>();
            return reader != null;
        }
    }
}