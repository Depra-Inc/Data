using System.IO;
using Depra.Data.Persistent.Api;

namespace Depra.Data.Persistent.Readers
{
    public readonly struct FileReader : IDataReader
    {
        private readonly ISerializer _serializer;
        
        public object ReadData(string uri)
        {
            using var stream = File.Open(uri, FileMode.Open);
            var result = _serializer.Deserialize(stream);
            return result;
        }
        
        public FileReader(ISerializer serializer)
        {
            _serializer = serializer;
        }
    }
}