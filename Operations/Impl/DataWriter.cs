using Depra.Data.Operations.Api;

namespace Depra.Data.Operations.Impl
{
    public class DataWriter<TData> : IDataWriter<TData>
    {
        private readonly IDataDirectory _location;
        private readonly IWritingRule<TData> _rule;

        public void WriteData(string dataName, TData data)
        {
            var fullPath = _location.CombineFullFilePath(dataName);
            _rule.WriteData(fullPath, data);
        }

        public DataWriter(IDataDirectory location, IWritingRule<TData> rule)
        {
            _rule = rule;
            _location = location;
        }
    }
}