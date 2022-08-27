using System;
using System.IO;
using Depra.Data.Serialization.Api;

namespace Depra.Data.Serialization.Impl
{
    /// <summary>
    /// Objects passed to this serializer must contain a default constructor.
    /// Doesn't serialize properties.
    /// </summary>
    public class XmlSerializer : ISerializer
    {
        public void Serialize(object obj, Stream stream)
        {
            var serializer = CreateAdapter(obj.GetType());
            serializer.Serialize(stream, obj);
        }

        public T Deserialize<T>(Stream stream)
        {
            var serializer = CreateAdapter(typeof(T));
            var deserializedObject = serializer.Deserialize(stream);
            var deserializedObjectAsT = (T)deserializedObject;

            return deserializedObjectAsT;
        }

        private static System.Xml.Serialization.XmlSerializer CreateAdapter(Type objectType) =>
            new System.Xml.Serialization.XmlSerializer(objectType);
    }
}