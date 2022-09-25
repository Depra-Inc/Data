using System.Runtime.CompilerServices;
using Depra.Data.Operations.Api;

namespace Depra.Data.Operations.Impl
{
    internal class DataReaderByType
    {
        private static class Storage<T>
        {
            private static readonly ConditionalWeakTable<DataReaderByType, IDataReader<T>> Table;

            public static IDataReader<T> GetValue(DataReaderByType fieldByType)
            {
                Table.TryGetValue(fieldByType, out var result);
                return result;
            }

            public static void SetValue(DataReaderByType fieldByType, IDataReader<T> value)
            {
                Table.Remove(fieldByType);
                Table.Add(fieldByType, value);
            }

            static Storage()
            {
                Table = new ConditionalWeakTable<DataReaderByType, IDataReader<T>>();
            }
        }

        public IDataReader<T> GetValue<T>()
        {
            return Storage<T>.GetValue(this);
        }

        public void SetValue<T>(IDataReader<T> value)
        {
            Storage<T>.SetValue(this, value);
        }
    }
}