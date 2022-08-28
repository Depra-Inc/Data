using System.Runtime.CompilerServices;
using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.Impl.Saving
{
    public class DataWriterByType
    {
        private static class Storage<T>
        {
            private static readonly ConditionalWeakTable<DataWriterByType, IDataWriter<T>> Table;

            public static IDataWriter<T> GetValue(DataWriterByType fieldByType)
            {
                Table.TryGetValue(fieldByType, out var result);
                return result;
            }

            public static void SetValue(DataWriterByType fieldByType, IDataWriter<T> value)
            {
                Table.Remove(fieldByType);
                Table.Add(fieldByType, value);
            }

            static Storage()
            {
                Table = new ConditionalWeakTable<DataWriterByType, IDataWriter<T>>();
            }
        }

        public IDataWriter<T> GetValue<T>() => Storage<T>.GetValue(this);

        public void SetValue<T>(IDataWriter<T> value) => Storage<T>.SetValue(this, value);
    }
}