using System;

namespace Depra.Data.Storage.Internal.Exceptions
{
    internal class AlreadyRegisteredException : Exception
    {
        private const string MessageFormat = "Type {0} is already registered!";
        
        public AlreadyRegisteredException(Type type) : base(string.Format(MessageFormat, type))
        {
        }
    }
}