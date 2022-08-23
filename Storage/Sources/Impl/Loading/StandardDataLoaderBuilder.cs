using System;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.Impl.Loading
{
    public class StandardDataLoaderBuilder : IDataLoaderBuilder
    {
        private readonly DataReaderByType _dataReaders;

        public IDataLoaderBuilder AddReader<TData>(ITypedDataReader<TData> reader)
        {
            if (_dataReaders.GetValue<TData>() != null)
            {
                throw new ArgumentException();
            }

            _dataReaders.SetValue(reader);
            return this;
        }

        public IDataLoader BuildFor(ILocationProvider location) => new DataLoader(location, _dataReaders);

        public StandardDataLoaderBuilder()
        {
            _dataReaders = new DataReaderByType();
        }
    }
}