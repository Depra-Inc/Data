using System.Runtime.CompilerServices;
using Depra.Data.Domain.Writing;

namespace Depra.Data.Application.Writers
{
    internal class DataWriterByType : IDataWriterByType
    {
        private static class Storage<TWriter> where TWriter : class
        {
            private static readonly ConditionalWeakTable<DataWriterByType, TWriter> TABLE;

            public static TWriter GetValue(DataWriterByType fieldByType)
            {
                TABLE.TryGetValue(fieldByType, out var result);
                return result;
            }

            public static void SetValue(DataWriterByType fieldByType, TWriter value)
            {
                TABLE.Remove(fieldByType);
                TABLE.Add(fieldByType, value);
            }

            static Storage()
            {
                TABLE = new ConditionalWeakTable<DataWriterByType, TWriter>();
            }
        }

        public IGenericDataWriter GetValue() =>
            Storage<IGenericDataWriter>.GetValue(this);

        public ITypedDataWriter<TData> GetValue<TData>() =>
            Storage<ITypedDataWriter<TData>>.GetValue(this);

        public void SetValue(IGenericDataWriter genericDataWriter) =>
            Storage<IGenericDataWriter>.SetValue(this, genericDataWriter);
        
        public void SetValue<TData>(ITypedDataWriter<TData> value) =>
            Storage<ITypedDataWriter<TData>>.SetValue(this, value);
    }
}