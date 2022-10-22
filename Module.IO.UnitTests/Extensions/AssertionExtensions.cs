using FluentAssertions;

namespace Depra.Data.Module.IO.UnitTests.Extensions;

internal static class AssertionExtensions
{
    public static FileAssertion Should(this FileToTest file) => new() {FileDataObject = file};

    public static FileToTest File(this string fileName) => new() {Path = fileName};

    public class FileAssertion
    {
        public FileToTest FileDataObject { get; init; } = null!;

        public void NotExist(string because = "", params object[] reasonArgs)
            => System.IO.File.Exists(FileDataObject.Path).Should().BeFalse(because, reasonArgs);

        public void Exist(string because = "", params object[] reasonArgs)
            => System.IO.File.Exists(FileDataObject.Path).Should().BeTrue(because, reasonArgs);
    }

    public class FileToTest
    {
        public string Path;
    }
}