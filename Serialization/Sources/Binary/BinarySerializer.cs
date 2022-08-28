using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using Depra.Data.Serialization.Api;

namespace Depra.Data.Serialization.Binary
{
    [Obsolete]
    public class BinarySerializer : ISerializer, IAsyncSerializer
    {
        private readonly BinaryFormatter _binaryFormatter;

        public void Serialize(object obj, Stream stream)
        {
            _binaryFormatter.Serialize(stream, obj);
        }

        public T Deserialize<T>(Stream stream)
        {
            var deserializedObject = _binaryFormatter.Deserialize(stream);
            var deserializedObjectAsT = (T)deserializedObject;
            
            return deserializedObjectAsT;
        }

        public async Task SerializeAsync(object obj, Stream stream, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                _binaryFormatter.Serialize(memoryStream, obj);
            }
        }

        public async Task<object> DeserializeAsync(Stream stream, CancellationToken cancellationToken)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                var deserializedObject = _binaryFormatter.Deserialize(memoryStream);
                return deserializedObject;
            }
        }

        public BinarySerializer()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public override string ToString() => nameof(BinaryFormatter);
    }
}