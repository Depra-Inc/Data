using System.IO;
using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Json.Api;

namespace Depra.Data.Serialization.Json.Impl
{
    public class JsonSerializer : ISerializer
    {
        private readonly IJsonSerializerAdapter _serializerAdapter;

        public void Serialize(object obj, Stream stream)
        {
            _serializerAdapter.Serialize(stream, obj);
        }

        public T Deserialize<T>(Stream stream)
        {
            var deserializedObject = _serializerAdapter.Deserialize<T>(stream);
            return deserializedObject;
        }

        public JsonSerializer(IJsonSerializerAdapter serializerAdapter)
        {
            _serializerAdapter = serializerAdapter;
        }

        public override string ToString() => _serializerAdapter.ToString();
    }
}