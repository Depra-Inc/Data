using Depra.Data.Application.Writers;
using Depra.Data.Domain.Exceptions;
using Depra.Data.Domain.Saving;
using Depra.Data.Domain.Writing;

namespace Depra.Data.Application.Savers
{
    public class StandardDataSaverBuilder : IDataSaverBuilder
    {
        private readonly IDataWriterByType _dataWriters;
        
        public IDataSaver Build() => new DataSaver(_dataWriters);

        public IDataSaverBuilder With(IGenericDataWriter genericDataReader)
        {
            if (_dataWriters.GetValue() != null)
            {
                throw new AlreadyRegisteredException(typeof(IGenericDataWriter));
            }
            
            _dataWriters.SetValue(genericDataReader);
            
            return this;
        }
        
        public IDataSaverBuilder With<TData>(ITypedDataWriter<TData> typedDataWriter)
        {
            if (_dataWriters.GetValue<TData>() != null)
            {
                throw new AlreadyRegisteredException(typeof(TData));
            }
            
            _dataWriters.SetValue(typedDataWriter);

            return this;
        }

        public StandardDataSaverBuilder() => _dataWriters = new DataWriterByType();
    }
}