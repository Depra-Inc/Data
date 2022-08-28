using System.IO;
using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Xml.Api;

namespace Depra.Data.Serialization.Xml.Impl
{
    /// <summary>
    /// Objects passed to this serializer must contain a default constructor.
    /// Doesn't serialize properties.
    /// </summary>
    public class XmlSerializer : ISerializer
    {
        private readonly IXmlSerializerAdapter _serializerAdapter;
        
        public void Serialize(object obj, Stream stream)
        {
            _serializerAdapter.Serialize(stream, obj, obj.GetType());
        }

        public T Deserialize<T>(Stream stream)
        {
            var deserializedObject = _serializerAdapter.Deserialize(stream, typeof(T));
            var deserializedObjectAsT = (T)deserializedObject;

            return deserializedObjectAsT;
        }

        public XmlSerializer(IXmlSerializerAdapter xmlSerializerAdapter)
        {
            _serializerAdapter = xmlSerializerAdapter;
        }

        public override string ToString() => _serializerAdapter.ToString();
    }
}