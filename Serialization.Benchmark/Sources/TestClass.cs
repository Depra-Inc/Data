using System;

namespace Depra.Data.Serialization.Benchmark
{
    [Serializable]
    public class TestClass
    {
        public override string ToString() => nameof(TestClass);
    }
}