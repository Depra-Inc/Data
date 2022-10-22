using System.IO;
using Depra.Data.Domain.Reading;
using Depra.Data.Module.IO.Scanning;
using Depra.Serialization.Application.Serializers;

namespace Depra.Data.Module.IO.Reading
{
    public class LocalFileReader : IGenericDataReader
    {
        private readonly ISerializer _serializer;
        private readonly LocalDirectoryScanner _directoryScanner;

        public bool CanRead<TData>(string dataName) => File.Exists(_directoryScanner.GetFullFilePath(dataName));

        public TData ReadData<TData>(string dataName)
        {
            var fullFilePath = _directoryScanner.GetFullFilePath(dataName);
            using (var stream = File.Open(fullFilePath, FileMode.Open, FileAccess.Read))
            {
                var deserializedData = _serializer.Deserialize(stream, typeof(TData));
                return (TData) deserializedData;
            }
        }

        public LocalFileReader(ISerializer serializer, LocalDirectoryScanner directoryScanner)
        {
            _serializer = serializer;
            _directoryScanner = directoryScanner;
        }
    }
}