using System.IO;
using Depra.Data.Serialization.Api;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.IO
{
    public class FileWriter<TData> : IDataWriter<TData>
    {
        private readonly ISerializer _serializer;
        private readonly ILocationProvider _location;

        public void WriteData(string dataName, TData data)
        {
            var fullPath = _location.CombineFullFilePath(dataName);
            using (var stream = File.Open(fullPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                _serializer.Serialize(data, stream);
            }
        }

        public FileWriter(ILocationProvider location, ISerializer serializer)
        {
            _location = location;
            _serializer = serializer;
        }
    }
}