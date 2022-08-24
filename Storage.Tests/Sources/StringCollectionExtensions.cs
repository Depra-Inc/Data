using System;
using System.Collections.Generic;

namespace Depra.Data.Storage.Tests
{
    internal static class StringCollectionExtensions
    {
        private static readonly Random Rnd;
        
        public static string Random(this IReadOnlyList<string> keys) => keys[Rnd.Next(0, keys.Count)];

        static StringCollectionExtensions()
        {
            Rnd = new Random();
        }
    }
}