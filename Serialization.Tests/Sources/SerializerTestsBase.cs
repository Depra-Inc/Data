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
        public void Class_Serialization()
        {
            var sourceObject = new TestSerializableClass(CreateIdentifier());
            var cloneObject = Serializer.Clone(sourceObject);

            Assert.AreEqual(sourceObject.Id, cloneObject.Id);
        }

        [Test]
        public void Struct_Serialization()
        {
            var sourceObject = new TestSerializableStruct(CreateIdentifier());
            var cloneObject = Serializer.Clone(sourceObject);

            Assert.AreEqual(sourceObject.Id, cloneObject.Id);
        }

        [Test]
        public void Primitive_Serialization()
        {
            var sourceObject = CreateIdentifier();
            var cloneObject = Serializer.Clone(sourceObject);

            Assert.AreEqual(sourceObject, cloneObject);
        }

        protected abstract ISerializer CreateSerializer();

        private string CreateIdentifier() => Serializer.GetType().Name;
    }
}