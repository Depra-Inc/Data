using Depra.Data.Operations.Api;
using Depra.Data.Storage.Internal.Exceptions;

namespace Depra.Data.Operations.Impl
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

        public DataSaver()
        {
            _dataWriters = new DataWriterByType();
        }

        private bool TryResolveDataWriter<TData>(out IDataWriter<TData> writer)
        {
            writer = _dataWriters.GetValue<TData>();
            return writer != null;
        }
    }
}