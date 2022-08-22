using Depra.Data.Storage.Loading.Api;
using Depra.Data.Storage.Middleware.Api;

namespace Depra.Data.Storage.Loading.Impl
{
    public readonly struct DataLoader : IDataLoader
    {
        private readonly IDataReader _reader;
        private readonly ILocationProvider _location;

        public object LoadData(string name, object defaultValue)
        {
            object data;
            if (_location.ContainsDataByName(name))
            {
                var uri = _location.CombineFullFilePath(name);
                data = _reader.ReadData(uri) ?? defaultValue;
            }
            else
            {
                data = defaultValue;
            }

            return data;
        }

        public DataLoader(IDataReader reader, ILocationProvider location)
        {
            _reader = reader;
            _location = location;
        }
    }
}