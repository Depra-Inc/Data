using System;

namespace Depra.Data.Domain.Exceptions
{
    public class NotSupportedTypeException : Exception
    {
        private const string MESSAGE_FORMAT = "Type {0} is not supported!";

        public NotSupportedTypeException(Type type) : base(string.Format(MESSAGE_FORMAT, type)) { }
    }
}