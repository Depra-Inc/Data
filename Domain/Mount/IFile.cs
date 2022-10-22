using System.IO;

namespace Depra.Data.Domain.Mount
{
    /// <summary>
    /// Defines a file <see cref="IFileSystemInfo"/> entry.
    /// </summary>
    public interface IFile : IFileSystemInfo
    {
        /// <summary>
        /// Opens the file stream for reading.
        /// </summary>
        /// <returns><see cref="Stream"/> that can be used for reading the file.</returns>
        Stream OpenRead();
    }
}