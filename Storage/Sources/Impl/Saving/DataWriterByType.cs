using System.Runtime.CompilerServices;
using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.Impl.Saving
{
    public class DataWriterByType
    {
        private static class Storage<T>
        {
            private static readonly ConditionalWeakTable<DataWriterByType, ITypedDataWriter<T>> Table;

            public static ITypedDataWriter<T> GetValue(DataWriterByType fieldByType)
            {
                Table.TryGetValue(fieldByType, out var result);
                return result;
            }

            public static void SetValue(DataWriterByType fieldByType, ITypedDataWriter<T> value)
            {
                Table.Remove(fieldByType);
                Table.Add(fieldByType, value);
            }

            static Storage()
            {
                Table = new ConditionalWeakTable<DataWriterByType, ITypedDataWriter<T>>();
            }
        }

        public ITypedDataWriter<T> GetValue<T>() => Storage<T>.GetValue(this);

        public void SetValue<T>(ITypedDataWriter<T> value) => Storage<T>.SetValue(this, value);
    }
}