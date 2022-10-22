using Depra.Data.Domain.Mount;
using Depra.Data.Domain.Reading;

namespace Depra.Data.Application.Readers
{
    public class DataReader<TData> : ITypedDataReader<TData>
    {
        private readonly IDirectoryScanner _directoryScanner;
        private readonly IDataReadingStrategy _readingStrategy;

        public bool CanRead(string dataName) => _directoryScanner.ContainsDataByName(dataName);

        public TData ReadData(string dataName)
        {
            var fullPath = _directoryScanner.GetFullFilePath(dataName);
            var data = _readingStrategy.ReadData<TData>(fullPath);

            return data;
        }

        public DataReader(IDirectoryScanner directoryScanner, IDataReadingStrategy readingStrategy)
        {
            _readingStrategy = readingStrategy;
            _directoryScanner = directoryScanner;
        }
    }
}