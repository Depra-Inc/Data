using System.IO;
using Depra.Data.Storage.Api;

namespace Depra.Data.Storage.Extensions
{
    public static class SerializerExtensions
    {
        public static void Serialize<TData>(this ISerializer serializer, TData data, Stream stream) =>
            serializer.Serialize(data, stream);

        public static TData Deserialize<TData>(this ISerializer serializer, Stream stream) =>
            (TData)serializer.Deserialize(stream);
    }
}