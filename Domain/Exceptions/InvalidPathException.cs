using System;

namespace Depra.Data.Domain.Exceptions
{
    public class InvalidPathException : Exception
    {
        private const string MESSAGE_FORMAT = "Invalid pah: {0}";

        public InvalidPathException(string path) : base(string.Format(MESSAGE_FORMAT, path)) { }
    }
}