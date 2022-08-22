using System.IO;
using Depra.Data.Storage.Api;

namespace Depra.Data.Storage.IO
{
    public readonly struct FileReader : IDataReader
    {
        private readonly ISerializer _serializer;
        
        public object ReadData(string path)
        {
            using (var stream = File.Open(path, FileMode.Open))
            {
                var result = _serializer.Deserialize(stream);
                return result;
            }
        }
        
        public FileReader(ISerializer serializer) => _serializer = serializer;
    }
}