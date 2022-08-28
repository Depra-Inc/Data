using System;
using System.IO;
using Depra.Data.Serialization.Xml.Api;

namespace Depra.Data.Serialization.Xml.Impl
{
    public struct StandardXmlSerializerAdapter : IXmlSerializerAdapter
    {
        public void Serialize(Stream stream, object obj, Type objectType)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(objectType);
            serializer.Serialize(stream, obj);
        }

        public object Deserialize(Stream stream, Type objectType)
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(objectType);
            var deserializedObject = serializer.Deserialize(stream);

            return deserializedObject;
        }

        public override string ToString() => nameof(System.Xml.Serialization.XmlSerializer);
    }
}