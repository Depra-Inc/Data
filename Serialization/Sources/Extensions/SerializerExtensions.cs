using System;
using System.IO;
using Depra.Data.Serialization.Api;

namespace Depra.Data.Serialization.Extensions
{
    public static class SerializerExtensions
    {
        public static byte[] SerializeToBytes(this ISerializer serializer, object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(obj, memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static T DeserializeBytes<T>(this ISerializer serializer, byte[] serializedObject)
        {
            T deserializedObject;
            using (var memoryStream = new MemoryStream(serializedObject))
            {
                deserializedObject = serializer.Deserialize<T>(memoryStream);
            }

            return deserializedObject;
        }

        public static T Clone<T>(this ISerializer serializer, T source)
        {
            var bytes = serializer.SerializeToBytes(source);
            var deserializedObject = serializer.DeserializeBytes<T>(bytes);
            
            return deserializedObject;
        }

        /// <summary>
        /// Serializes the given object into memory stream.
        /// </summary>
        /// <param name="serializer">Serializer for object.</param>
        /// <param name="obj">The object to be serialized.</param>
        /// <returns>The serialized object as memory stream.</returns>
        public static Stream SerializeToStream(this ISerializer serializer, object obj)
        {
            var stream = new MemoryStream();
            serializer.Serialize(obj, stream);
            return stream;
        }

        /// <summary>
        /// Deserializes as an object.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="stream">The stream to deserialize.</param>
        /// <returns>The deserialized object.</returns>
        public static T DeserializeFromStream<T>(this ISerializer serializer, MemoryStream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var deserializedObject = serializer.Deserialize<T>(stream);
            
            return deserializedObject;
        } 
    }
}