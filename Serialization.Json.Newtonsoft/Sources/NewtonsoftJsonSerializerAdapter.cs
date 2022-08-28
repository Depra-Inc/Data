using System.IO;
using Depra.Data.Serialization.Json.Api;
using Newtonsoft.Json;

namespace Serialization.Json.Newtonsoft.Sources
{
    public class NewtonsoftJsonSerializerAdapter : IJsonSerializerAdapter
    {
        private readonly JsonSerializer _serializer;
        
        public void Serialize(Stream stream, object obj)
        {
            using (var streamWriter = new StreamWriter(stream))
            {
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    _serializer.Serialize(jsonWriter, obj);
                }
            }
        }

        public T Deserialize<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var deserializedObject = _serializer.Deserialize<T>(jsonReader);
                    return deserializedObject;
                }
            }
        }

        public NewtonsoftJsonSerializerAdapter(JsonSerializer serializer)
        {
            _serializer = serializer;
        }

        public override string ToString() => "NS" + nameof(JsonSerializer);
    }
}