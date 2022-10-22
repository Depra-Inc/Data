using System.Runtime.CompilerServices;
using Depra.Data.Domain.Reading;

namespace Depra.Data.Application.Readers
{
    internal class DataReaderByType : IDataReaderByType
    {
        private static class Storage<TDataReader> where TDataReader : class
        {
            private static readonly ConditionalWeakTable<DataReaderByType, TDataReader> TABLE;

            public static TDataReader GetValue(DataReaderByType fieldByType)
            {
                TABLE.TryGetValue(fieldByType, out var result);
                return result;
            }

            public static void SetValue(DataReaderByType fieldByType, TDataReader value)
            {
                TABLE.Remove(fieldByType);
                TABLE.Add(fieldByType, value);
            }

            static Storage()
            {
                TABLE = new ConditionalWeakTable<DataReaderByType, TDataReader>();
            }
        }

        public ITypedDataReader<TData> GetValue<TData>() =>
            Storage<ITypedDataReader<TData>>.GetValue(this);

        public IGenericDataReader GetValue() => 
            Storage<IGenericDataReader>.GetValue(this);

        public void SetValue<TData>(ITypedDataReader<TData> typedDataReader) =>
            Storage<ITypedDataReader<TData>>.SetValue(this, typedDataReader);

        public void SetValue(IGenericDataReader genericDataReader) =>
            Storage<IGenericDataReader>.SetValue(this, genericDataReader);
    }
}