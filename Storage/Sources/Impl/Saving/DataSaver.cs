using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Saving;
using Depra.Data.Storage.Api.Writing;
using Depra.Data.Storage.Internal.Exceptions;

namespace Depra.Data.Storage.Impl.Saving
{
    public readonly struct DataSaver : IDataSaver
    {
        private readonly ILocationProvider _location;
        private readonly DataWriterByType _dataWriters;

        public void SaveData<TData>(string name, TData data)
        {
            if (TryResolveDataWriter<TData>(out var writer) == false)
            {
                throw new NotSupportedTypeException(typeof(TData));
            }

            var fullPath = _location.CombineFullFilePath(name);
            writer.WriteData(fullPath, data);
        }

        public void RemoveData(string name)
        {
            if (_location.ContainsDataByName(name) == false)
            {
                throw new InvalidPathException(_location.CombineFullFilePath(name));
            }

            _location.Remove(name);
        }

        public DataSaver(ILocationProvider location, DataWriterByType writers)
        {
            _location = location;
            _dataWriters = writers;
        }

        private bool TryResolveDataWriter<TData>(out ITypedDataWriter<TData> writer)
        {
            writer = _dataWriters.GetValue<TData>();
            return writer != null;
        }
    }
}