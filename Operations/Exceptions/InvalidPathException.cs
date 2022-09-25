using System;

namespace Depra.Data.Storage.Internal.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string MessageFormat = "Invalid pah: {0}";

        public InvalidPathException(string path) : base(string.Format(MessageFormat, path))
        {
        }
    }
}