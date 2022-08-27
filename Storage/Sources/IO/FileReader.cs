using System.IO;
using Depra.Data.Serialization.Api;
using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.IO
{
    public readonly struct FileReader<TData> : ITypedDataReader<TData>
    {
        private readonly ISerializer _serializer;

        public TData ReadData(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var deserializedData = _serializer.Deserialize<TData>(stream);
                return (TData)deserializedData;
            }
        }

        public FileReader(ISerializer serializer) => _serializer = serializer;
    }
}