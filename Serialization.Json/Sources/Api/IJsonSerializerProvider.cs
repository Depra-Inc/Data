using System.IO;

namespace Depra.Data.Serialization.Json.Api
{
    public interface IJsonSerializerProvider
    {
        void Serialize(Stream stream, object obj);

        T Deserialize<T>(Stream stream);
    }
}