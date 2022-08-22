using Depra.Data.Storage.Internal.Exceptions;
using Depra.Data.Storage.Middleware.Api;
using Depra.Data.Storage.Saving.Api;

namespace Depra.Data.Storage.Saving.Impl
{
    public readonly struct DataSaver : IDataSaver
    {
        private readonly IDataWriter _writer;
        private readonly ILocationProvider _location;
        
        public void SaveData(string name, object data)
        {
            var fullPath = _location.CombineFullFilePath(name);
            _writer.WriteData(fullPath, data);
        }

        public void RemoveData(string name)
        {
            if (_location.ContainsDataByName(name) == false)
            {
                throw new InvalidPathException(_location.CombineFullFilePath(name));
            }

            var fullPath = _location.CombineFullFilePath(name);
            _writer.ClearData(fullPath);
        }

        public DataSaver(IDataWriter writer, ILocationProvider location)
        {
            _writer = writer;
            _location = location;
        }
    }
}