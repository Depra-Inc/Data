using System;
using System.IO;
using System.Runtime.Serialization.Json;
using Depra.Data.Serialization.Json.Api;

namespace Depra.Data.Serialization.Json.Impl
{
    public class DataContractJsonSerializerAdapter : IJsonSerializerAdapter
    {
        public void Serialize(Stream stream, object obj)
        {
            var serializer = CreateSerializer(obj.GetType());
            serializer.WriteObject(stream, obj);
        }

        public T Deserialize<T>(Stream stream)
        {
            var serializer = CreateSerializer(typeof(T));
            var deserializedObject = serializer.ReadObject(stream);
            var deserializedObjectAsT = (T)deserializedObject;
            
            return deserializedObjectAsT;
        }

        private static DataContractJsonSerializer CreateSerializer(Type objectType) =>
            new DataContractJsonSerializer(objectType);

        public override string ToString() => "DcJsonSerializer";
    }
}