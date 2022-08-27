using System;
using System.IO;
using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Json.Api;

namespace Depra.Data.Serialization.Json.Impl
{
    public class JsonSerializer : ISerializer
    {
        private readonly IJsonSerializerProvider _serializerProvider;

        public void Serialize(object obj, Stream stream)
        {
            _serializerProvider.Serialize(stream, obj);
        }

        public T Deserialize<T>(Stream stream)
        {
            var deserializedObject = _serializerProvider.Deserialize<T>(stream);
            return deserializedObject;
        }

        public JsonSerializer(IJsonSerializerProvider serializerProvider)
        {
            _serializerProvider = serializerProvider;
        }
    }
}