using System;

namespace Depra.Data.Domain.Exceptions
{
    public class AlreadyRegisteredException : Exception
    {
        private const string MESSAGE_FORMAT = "Type {0} is already registered!";

        public AlreadyRegisteredException(Type type) : base(string.Format(MESSAGE_FORMAT, type)) { }
    }
}