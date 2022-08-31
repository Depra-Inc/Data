using FluentAssertions;

namespace Depra.Data.Storage.Tests
{
    internal static class AssertionExtensions
    {
        public static FileAssertion Should(this FileToTest file) => new() { FileDataObject = file };

        public class FileAssertion
        {
            public FileToTest FileDataObject { get; init; }

            public void NotExist(string because = "", params object[] reasonArgs)
                => System.IO.File.Exists(FileDataObject.Path).Should().BeFalse(because, reasonArgs);

            public void Exist(string because = "", params object[] reasonArgs)
                => System.IO.File.Exists(FileDataObject.Path).Should().BeTrue(because, reasonArgs);
        }

        public static FileToTest File(string fName) => new() { Path = fName };

        public class FileToTest
        {
            public string Path;
        }
    }
}