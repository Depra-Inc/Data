using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Impl;

namespace Depra.Data.Serialization.Tests.Sources
{
    internal class XmlSerializerTests : SerializerTestsBase
    {
        protected override ISerializer CreateSerializer() => new XmlSerializer();
    }
}