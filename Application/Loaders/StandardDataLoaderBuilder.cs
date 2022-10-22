using Depra.Data.Application.Readers;
using Depra.Data.Domain.Exceptions;
using Depra.Data.Domain.Loading;
using Depra.Data.Domain.Reading;

namespace Depra.Data.Application.Loaders
{
    public class StandardDataLoaderBuilder : IDataLoaderBuilder
    {
        private readonly DataReaderByType _dataReaders;

        public IDataLoader Build() => new DataLoader(_dataReaders);

        public IDataLoaderBuilder With(IGenericDataReader genericDataReader)
        {
            if (_dataReaders.GetValue() != null)
            {
                throw new AlreadyRegisteredException(typeof(IGenericDataReader));
            }
            
            _dataReaders.SetValue(genericDataReader);
            
            return this;
        }

        public IDataLoaderBuilder With<TData>(ITypedDataReader<TData> typedDataReader)
        {
            if (_dataReaders.GetValue<TData>() != null)
            {
                throw new AlreadyRegisteredException(typeof(TData));
            }
            
            _dataReaders.SetValue(typedDataReader);

            return this;
        }

        public StandardDataLoaderBuilder() => _dataReaders = new DataReaderByType();
    }
}