using System;

namespace Depra.Data.Serialization.Tests.Sources
{
    [Serializable]
    public struct TestSerializableStruct
    {
        public string Id;

        public TestSerializableStruct(string id)
        {
            Id = id;
        }
    }
}