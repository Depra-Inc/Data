using System.IO;
using Depra.Data.Storage.Middleware.Api;

namespace Depra.Data.Storage.Middleware.Impl
{
    public readonly struct FileWriter : IDataWriter
    {
        private readonly ISerializer _serializer;

        public void WriteData(string path, object data)
        {
            using (var stream = File.Open(path, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(data, stream);
            }
        }

        public void ClearData(string path) => File.Delete(path);

        public FileWriter(ISerializer serializer)
        {
            _serializer = serializer;
        }
    }
}