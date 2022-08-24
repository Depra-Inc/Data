using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Depra.Data.Storage.Api;

namespace Depra.Data.Storage.Serialization
{
    /// <summary>
    /// Binary Serializer for saving/restoring data.
    /// </summary>
    public class BinarySerializer : ISerializer
    {
        private readonly BinaryFormatter _binaryFormatter;
        
        public void Serialize(object obj, Stream stream)
        {
            _binaryFormatter.Serialize(stream, obj);
        }
        
        public object Deserialize(Stream stream)
        {
            var deserializedObject = _binaryFormatter.Deserialize(stream);
            return deserializedObject;
        }

        public BinarySerializer()
        {
            _binaryFormatter = new BinaryFormatter();
        }
    }
}