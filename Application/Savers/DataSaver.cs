using Depra.Data.Domain.Exceptions;
using Depra.Data.Domain.Saving;
using Depra.Data.Domain.Writing;

namespace Depra.Data.Application.Savers
{
    public class DataSaver : IDataSaver
    {
        private readonly IDataWriterByType _dataWriters;

        public void SaveData<TData>(string name, TData data)
        {
            if (TryResolveTypedDataWriter<TData>(out var typedDataReader))
            {
                typedDataReader.WriteData(name, data);
                return;
            }

            if (TryResolveGenericDataWriter(out var genericDataWriter))
            {
                genericDataWriter.WriteData(name, data);
                return;
            }

            throw new NotSupportedTypeException(typeof(TData));
        }

        public DataSaver(IDataWriterByType dataWriters) =>
            _dataWriters = dataWriters ?? throw new DataSaverException("Data writers is empty!");

        private bool TryResolveTypedDataWriter<TData>(out ITypedDataWriter<TData> writer)
        {
            writer = _dataWriters.GetValue<TData>();
            return writer != null;
        }

        private bool TryResolveGenericDataWriter(out IGenericDataWriter reader)
        {
            reader = _dataWriters.GetValue();
            return reader != null;
        }
    }
}