using System.ComponentModel;
using Depra.Data.Serialization.Api;
using Depra.Data.Serialization.Extensions;
using NUnit.Framework;

namespace Depra.Data.Serialization.Tests.Sources
{
    [TestFixture]
    internal abstract class SerializerTestsBase
    {
        private ISerializer Serializer { get; set; }

        [SetUp]
        public void SetUp()
        {
            Serializer = CreateSerializer();
        }

        [Test]
        public void Clone_Class()
        {
            var sourceObject = new TestSerializableClass(CreateIdentifier());
            var cloneObject = Serializer.Clone(sourceObject);

            Assert.AreEqual(sourceObject.Id, cloneObject.Id);
        }

        [Test]
        public void Close_Struct()
        {
            var sourceObject = new TestSerializableStruct(CreateIdentifier());
            var cloneObject = Serializer.Clone(sourceObject);

            Assert.AreEqual(sourceObject.Id, cloneObject.Id);
        }

        [Test]
        public void Clone_Primitive()
        {
            var sourceObject = CreateIdentifier();
            var cloneObject = Serializer.Clone(sourceObject);

            Assert.AreEqual(sourceObject, cloneObject);
        }

        //[Test]
        public void Deserialize_Class()
        {
            
        }

        protected abstract ISerializer CreateSerializer();

        private string CreateIdentifier() => Serializer.GetType().Name;
    }
}