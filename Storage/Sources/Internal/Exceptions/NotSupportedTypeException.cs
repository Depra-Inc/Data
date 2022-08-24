using System;

namespace Depra.Data.Storage.Internal.Exceptions
{
    internal class NotSupportedTypeException : Exception
    {
        private const string MessageFormat = "Type {0} is not supported!";

        public NotSupportedTypeException(Type type) : base(string.Format(MessageFormat, type))
        {
        }
    }
}