using System;
using System.IO;

namespace Depra.Data.Serialization.Api
{
    /// <summary>
    /// Interface for all serializers for saving/restoring data.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize the specified object to stream with encoding.
        /// </summary>
        void Serialize(object obj, Stream stream);

        /// <summary>
        /// Deserialize the specified object from stream using the encoding.
        /// </summary>
        T Deserialize<T>(Stream stream);
    }
}