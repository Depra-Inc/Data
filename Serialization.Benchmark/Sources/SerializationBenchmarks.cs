using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Binary;
using Depra.Data.Serialization.Extensions;
using Depra.Data.Serialization.Json.Impl;
using Depra.Data.Serialization.Xml.Impl;
using Serialization.Json.Newtonsoft.Sources;

namespace Depra.Data.Serialization.Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class SerializationBenchmarks
    {
        [ParamsSource(nameof(Serializers))] public ISerializer Serializer { get; set; }

        [ParamsSource(nameof(TestObjects))] public object SerializableObject { get; set; }

        [Benchmark]
        public void Serialize() => Serializer.SerializeToStream(SerializableObject);
        
        //[Benchmark]
        //public object Clone() => Serializer.Clone(SerializableObject);

        public static IEnumerable<object> TestObjects()
        {
            yield return "TestString";
            yield return new TestClass();
            yield return new TestStruct();
            // Add more test objects hero if needed.
        }

        public static IEnumerable<ISerializer> Serializers()
        {
            yield return new BinarySerializer();
            yield return new XmlSerializer(new StandardXmlSerializerAdapter());
            yield return new XmlSerializer(new DataContractSerializerAdapter());
            yield return new JsonSerializer(new DataContractJsonSerializerAdapter());
            yield return new JsonSerializer(
                new NewtonsoftJsonSerializerAdapter(Newtonsoft.Json.JsonSerializer.Create()));
            // Add more serializers here if needed.
        }
    }
}