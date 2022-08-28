using System;
using System.IO;
using System.Runtime.Serialization;
using Depra.Data.Serialization.Xml.Api;

namespace Depra.Data.Serialization.Xml.Impl
{
    public struct DataContractSerializerAdapter : IXmlSerializerAdapter
    {
        public void Serialize(Stream stream, object obj, Type objectType)
        {
            var serializer = new DataContractSerializer(objectType);
            serializer.WriteObject(stream, obj);
        }

        public object Deserialize(Stream stream, Type objectType)
        {
            var serializer = new DataContractSerializer(objectType);
            var deserializedObject = serializer.ReadObject(stream);

            return deserializedObject;
        }

        public override string ToString() => "DcXmlSerializer";
    }
}