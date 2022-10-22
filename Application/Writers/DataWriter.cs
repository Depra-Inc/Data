using Depra.Data.Domain.Mount;
using Depra.Data.Domain.Writing;

namespace Depra.Data.Application.Writers
{
    public class DataWriter<TData> : ITypedDataWriter<TData>
    {
        private readonly IDirectoryScanner _directoryScanner;
        private readonly IDataWritingStrategy _writingStrategy;

        public void WriteData(string dataName, TData data)
        {
            var fullPath = _directoryScanner.GetFullFilePath(dataName);
            _writingStrategy.WriteData(fullPath, data);
        }

        public DataWriter(IDirectoryScanner directoryScanner, IDataWritingStrategy writingStrategy)
        {
            _writingStrategy = writingStrategy;
            _directoryScanner = directoryScanner;
        }
    }
}