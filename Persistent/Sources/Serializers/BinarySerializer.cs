using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Depra.Data.Persistent.Api;

namespace Depra.Data.Persistent.Serializers
{
    /// <summary>
    /// Binary Serializer for saving/restoring data.
    /// </summary>
    [Serializable]
    public struct BinarySerializer : ISerializer
    {
        public void Serialize(object obj, Stream stream)
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
        }
        
        public object Deserialize(Stream stream)
        {
            var formatter = new BinaryFormatter();
            var deserializedObject = formatter.Deserialize(stream);

            return deserializedObject;
        }
    }
}