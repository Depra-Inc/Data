using System.IO;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.IO
{
    public readonly struct FileReader<TData> : ITypedDataReader<TData>
    {
        private readonly ISerializer _serializer;

        public TData ReadData(string path)
        {
            using (var stream = File.Open(path, FileMode.Open))
            {
                var result = _serializer.Deserialize(stream);
                return (TData)result;
            }
        }

        public FileReader(ISerializer serializer) => _serializer = serializer;
    }
}