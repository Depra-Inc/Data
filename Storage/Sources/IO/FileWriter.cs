using System.IO;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.IO
{
    public readonly struct FileWriter<TData> : ITypedDataWriter<TData>
    {
        private readonly ISerializer _serializer;

        public void WriteData(string path, TData data)
        {
            using (var stream = File.Open(path, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(data, stream);
            }
        }

        public FileWriter(ISerializer serializer)
        {
            _serializer = serializer;
        }
    }
}