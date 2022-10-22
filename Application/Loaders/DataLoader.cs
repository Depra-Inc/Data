using System;
using System.Collections.Generic;
using Depra.Data.Domain.Exceptions;
using Depra.Data.Domain.Loading;
using Depra.Data.Domain.Reading;

namespace Depra.Data.Application.Loaders
{
    public class DataLoader : IDataLoader
    {
        private readonly IDataReaderByType _dataReaders;

        public TData LoadData<TData>(string name, TData defaultValue)
        {
            if (TryResolveTypedDataReader<TData>(out var typedDataReader))
            {
                return typedDataReader.CanRead(name)
                    ? ReadDataWithTypedReader(typedDataReader, name, defaultValue)
                    : defaultValue != null ? defaultValue : throw new KeyNotFoundException(name);
            }

            if (TryResolveGenericDataReader(out var genericDataReader))
            {
                return genericDataReader.CanRead<TData>(name)
                    ? ReadDataWithGenericReader(genericDataReader, name, defaultValue)
                    : defaultValue != null ? defaultValue : throw new KeyNotFoundException(name);
            }

            throw new NotSupportedTypeException(typeof(TData));
        }

        public DataLoader(IDataReaderByType dataReaders) =>
            _dataReaders = dataReaders ?? throw new DataLoaderException("Data readers is empty!");

        private bool TryResolveTypedDataReader<TData>(out ITypedDataReader<TData> reader)
        {
            reader = _dataReaders.GetValue<TData>();
            return reader != null;
        }

        private bool TryResolveGenericDataReader(out IGenericDataReader reader)
        {
            reader = _dataReaders.GetValue();
            return reader != null;
        }

        private static TData ReadDataWithTypedReader<TData>(ITypedDataReader<TData> reader, string name,
            TData defaultValue)
        {
            var readData = reader.ReadData(name);
            if (readData == null)
            {
                readData = defaultValue;
            }

            return readData;
        }

        private static TData ReadDataWithGenericReader<TData>(IGenericDataReader reader, string name,
            TData defaultValue)
        {
            var readData = reader.ReadData<TData>(name);
            if (readData == null)
            {
                readData = defaultValue;
            }

            return readData;
        }
    }
}