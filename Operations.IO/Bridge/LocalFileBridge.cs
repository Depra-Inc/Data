using System.Collections.Generic;
using System.IO;
using Depra.Data.Operations.Api;
using Depra.Serialization.Application.Serializers;

namespace Depra.Data.Operations.IO.Bridge
{
    public class LocalFileBridge : IDataDirectory
    {
        private readonly string _format;
        private readonly string _directoryPath;
        private readonly ISerializer _serializer;
        private readonly LocalFileSearcher _fileSearcher;

        public TData GetData<TData>(string dataName)
        {
            using (var stream = File.Open(dataName, FileMode.Open, FileAccess.Read))
            {
                var deserializedData = _serializer.Deserialize(stream, typeof(TData));
                return (TData)deserializedData;
            }
        }

        public void SetData<TData>(string dataName, TData data)
        {
            using (var stream = File.Open(dataName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                _serializer.Serialize(data, stream, typeof(TData));
            }
        }

        public void RemoveData(string fileName) => File.Delete(CombineFullFilePath(fileName));

        public IEnumerable<string> GetAllNames()
        {
            var filenames = _fileSearcher.GetAllFiles(_directoryPath);
            return StripFilenamesExtension(filenames);
        }

        public bool ContainsDataByName(string fileName) =>
            File.Exists(CombineFullFilePath(fileName));

        public string CombineFullFilePath(string fileName) =>
            Path.Combine(_directoryPath, fileName) + _format;

        public LocalFileBridge(string directoryPath, string format, ISerializer serializer, SearchOption searchOption)
        {
            _format = format;
            _directoryPath = directoryPath;

            _serializer = serializer;
            
            var searchPattern = "*" + format;
            _fileSearcher = new LocalFileSearcher(searchPattern, searchOption);

            TryCreateDirectory();
        }

        private void TryCreateDirectory()
        {
            if (Directory.Exists(_directoryPath) == false)
            {
                Directory.CreateDirectory(_directoryPath);
            }
        }

        private static IEnumerable<string> StripFilenamesExtension(IReadOnlyList<string> filenames)
        {
            var filenamesWithoutExtension = new string[filenames.Count];
            for (var i = 0; i < filenamesWithoutExtension.Length; i++)
            {
                filenamesWithoutExtension[i] = Path.GetFileNameWithoutExtension(filenames[i]);
            }

            return filenamesWithoutExtension;
        }
    }
}