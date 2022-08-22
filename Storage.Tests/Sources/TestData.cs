using System;

namespace Depra.Data.Storage.Tests
{
    [Serializable]
    internal class TestData
    {
        public int Ident { get; }

        public TestData()
        {
            Ident = new Random().Next(int.MaxValue);
        }
    }
}