using System;

namespace Depra.Data.Persistent.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string Format = "Invalid pah: {0}";

        public InvalidPathException(string path) : base(string.Format(Format, path))
        {
        }

        public InvalidPathException(string path, Exception innerException) : base(string.Format(Format, path),
            innerException)
        {
        }
    }
}