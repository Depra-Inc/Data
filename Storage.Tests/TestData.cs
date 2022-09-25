using System;

namespace Depra.Data.Storage.Tests
{
    [Serializable]
    internal class TestData
    {
        public static TestData Empty { get; } = new() { Ident = 0 };

        public int Ident { get; private set; }

        public TestData()
        {
            Ident = new Random().Next(int.MaxValue);
        }
    }
}