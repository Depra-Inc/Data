using Depra.Data.Operations.Api;

namespace Depra.Data.Operations.Impl
{
    public class DataReader<TData> : IDataReader<TData>
    {
        private readonly IDataDirectory _location;
        private readonly IReadingRule<TData> _rule;

        public bool CanRead(string dataName) => _location.ContainsDataByName(dataName);

        public TData ReadData(string dataName)
        {
            var fullPath = _location.CombineFullFilePath(dataName);
            var data = _rule.ReadData(fullPath);
            
            return data;
        }

        public DataReader(IDataDirectory location, IReadingRule<TData> rule)
        {
            _rule = rule;
            _location = location;
        }
    }
}