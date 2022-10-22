using System;
using System.Collections.Generic;

namespace Depra.Data.Application.UnitTests.Extensions;

internal static class StringCollectionExtensions
{
    private static readonly Random RND;
        
    public static string Random(this IReadOnlyList<string> keys) => keys[RND.Next(0, keys.Count)];

    static StringCollectionExtensions()
    {
        RND = new Random();
    }
}