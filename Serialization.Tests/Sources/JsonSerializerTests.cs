using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Json.Impl;

namespace Depra.Data.Serialization.Tests.Sources
{
    internal class JsonSerializerTests : SerializerTestsBase
    {
        protected override ISerializer CreateSerializer() =>
            new JsonSerializer(new DataContractJsonSerializerAdapter());
    }
}