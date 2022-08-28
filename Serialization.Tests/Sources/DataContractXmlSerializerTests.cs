using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Xml.Impl;

namespace Depra.Data.Serialization.Tests.Sources
{
    internal class DataContractXmlSerializerTests : SerializerTestsBase
    {
        protected override ISerializer CreateSerializer()
        {
            var adapter = new DataContractSerializerAdapter();
            var serializer = new XmlSerializer(adapter);

            return serializer;
        }
    }
}