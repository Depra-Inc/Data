using System.Runtime.CompilerServices;
using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.Impl.Loading
{
    public class DataReaderByType
    {
        private static class Storage<T>
        {
            private static readonly ConditionalWeakTable<DataReaderByType, ITypedDataReader<T>> Table;

            public static ITypedDataReader<T> GetValue(DataReaderByType fieldByType)
            {
                Table.TryGetValue(fieldByType, out var result);
                return result;
            }

            public static void SetValue(DataReaderByType fieldByType, ITypedDataReader<T> value)
            {
                Table.Remove(fieldByType);
                Table.Add(fieldByType, value);
            }

            static Storage()
            {
                Table = new ConditionalWeakTable<DataReaderByType, ITypedDataReader<T>>();
            }
        }
        
        public ITypedDataReader<T> GetValue<T>() => Storage<T>.GetValue(this);

        public void SetValue<T>(ITypedDataReader<T> value) => Storage<T>.SetValue(this, value);
    }
}