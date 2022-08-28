using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Binary;

namespace Depra.Data.Serialization.Tests.Sources
{
    internal class BinarySerializerTests : SerializerTestsBase
    {
        protected override ISerializer CreateSerializer() => new BinarySerializer();
    }
}