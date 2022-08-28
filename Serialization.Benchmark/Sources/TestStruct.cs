using System;

namespace Depra.Data.Serialization.Benchmark
{
    [Serializable]
    public class TestStruct
    {
        public override string ToString() => nameof(TestStruct);
    }
}