using System;

namespace Depra.Data.Domain.Exceptions
{
    public class DataLoaderException : Exception
    {
        public DataLoaderException(string message) : base(message) { }
    }
}