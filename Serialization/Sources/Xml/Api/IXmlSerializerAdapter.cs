using System;
using System.IO;

namespace Depra.Data.Serialization.Xml.Api
{
    public interface IXmlSerializerAdapter
    {
        void Serialize(Stream stream, object obj, Type objectType);

        object Deserialize(Stream stream, Type objectType);
    }
}