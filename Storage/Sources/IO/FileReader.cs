using System.IO;
using Depra.Data.Serialization.Api;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.IO
{
    public class FileReader<TData> : IDataReader<TData>
    {
        private readonly ISerializer _serializer;
        private readonly ILocationProvider _location;

        public bool CanRead(string dataName) => _location.ContainsDataByName(dataName);

        public TData ReadData(string dataName)
        {
            var fullPath = _location.CombineFullFilePath(dataName);
            using (var stream = File.Open(fullPath, FileMode.Open, FileAccess.Read))
            {
                var deserializedData = _serializer.Deserialize<TData>(stream);
                return deserializedData;
            }
        }

        public FileReader(ILocationProvider location, ISerializer serializer)
        {
            _location = location;
            _serializer = serializer;
        }
    }
}