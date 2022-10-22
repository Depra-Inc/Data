namespace Depra.Data.Module.IO.UnitTests.Extensions;

internal static class StringCollectionExtensions
{
    private static readonly Random RND;
        
    public static string Random(this IReadOnlyList<string> keys) => keys[RND.Next(0, keys.Count)];

    static StringCollectionExtensions()
    {
        RND = new Random();
    }
}