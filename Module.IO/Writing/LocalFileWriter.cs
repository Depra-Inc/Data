using System.IO;
using Depra.Data.Domain.Writing;
using Depra.Data.Module.IO.Scanning;
using Depra.Serialization.Application.Serializers;

namespace Depra.Data.Module.IO.Writing
{
    public class LocalFileWriter : IGenericDataWriter
    {
        private readonly ISerializer _serializer;
        private readonly LocalDirectoryScanner _directoryScanner;
        
        public void WriteData<TData>(string dataName, TData data)
        {
            var fullFilePath = _directoryScanner.GetFullFilePath(dataName);
            using (var stream = File.Open(fullFilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                _serializer.Serialize(data, stream, typeof(TData));
            }
        }
        
        public LocalFileWriter(ISerializer serializer, LocalDirectoryScanner directoryScanner)
        {
            _serializer = serializer;
            _directoryScanner = directoryScanner;
        }
    }
}