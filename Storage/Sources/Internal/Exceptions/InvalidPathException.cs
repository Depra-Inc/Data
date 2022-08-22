using System;

namespace Depra.Data.Storage.Internal.Exceptions
{
    internal class InvalidPathException : Exception
    {
        private const string Format = "Invalid pah: {0}";

        public InvalidPathException(string path) : base(string.Format(Format, path))
        {
        }
    }
}