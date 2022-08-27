using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Json.Impl;
using Serialization.Json.Newtonsoft.Sources;

namespace Depra.Data.Serialization.Tests.Sources
{
    internal class NewtonsoftSerializerTest : SerializerTestsBase
    {
        protected override ISerializer CreateSerializer()
        {
            var newtonsoftSerializer = new Newtonsoft.Json.JsonSerializer();
            var serializerProvider = new NewtonsoftJsonSerializerProvider(newtonsoftSerializer);
            return new JsonSerializer(serializerProvider);
        }
    }
}