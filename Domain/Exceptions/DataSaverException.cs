using System;

namespace Depra.Data.Domain.Exceptions
{
    public class DataSaverException : Exception
    {
        public DataSaverException(string message) : base(message) { }
    }
}