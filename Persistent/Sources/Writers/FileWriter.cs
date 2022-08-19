using System.IO;
using Depra.Data.Persistent.Api;

namespace Depra.Data.Persistent.Writers
{
    public readonly struct FileWriter : IDataWriter
    {
        private readonly ISerializer _serializer;
        
        public void WriteData(string path, object data)
        {
            using var stream = File.Open(path, FileMode.Create);
            _serializer.Serialize(data, stream);
        }

        public void ClearData(string path) => File.Delete(path);

        public FileWriter(ISerializer serializer)
        {
            _serializer = serializer;
        }
    }
}