using Depra.Data.Storage.Api.Saving;
using Depra.Data.Storage.Api.Writing;
using Depra.Data.Storage.Internal.Exceptions;

namespace Depra.Data.Storage.Impl.Saving
{
    public class DataSaver : IDataSaver
    {
        private readonly DataWriterByType _dataWriters;

        public void AddWriter<TData>(IDataWriter<TData> writer)
        {
            if (TryResolveDataWriter<TData>(out var alreadyRegisteredWriter))
            {
                throw new AlreadyRegisteredException(alreadyRegisteredWriter.GetType());
            }
            
            _dataWriters.SetValue(writer);
        }

        public void SaveData<TData>(string name, TData data)
        {
            if (TryResolveDataWriter<TData>(out var writer) == false)
            {
                throw new NotSupportedTypeException(typeof(TData));
            }

            writer.WriteData(name, data);
        }

        public DataSaver(DataWriterByType writers)
        {
            _dataWriters = writers;
        }

        private bool TryResolveDataWriter<TData>(out IDataWriter<TData> writer)
        {
            writer = _dataWriters.GetValue<TData>();
            return writer != null;
        }
    }
}