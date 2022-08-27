using System;

namespace Depra.Data.Serialization.Tests.Sources
{
    [Serializable]
    public class TestSerializableClass
    {
        public string Id;

        public TestSerializableClass()
        {
        }

        public TestSerializableClass(string id)
        {
            Id = id;
        }
    }
}