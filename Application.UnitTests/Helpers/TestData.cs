using System;

namespace Depra.Data.Application.UnitTests.Helpers;

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