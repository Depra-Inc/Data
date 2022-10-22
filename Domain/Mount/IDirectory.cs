using System.Collections.Generic;

namespace Depra.Data.Domain.Mount
{
    /// <summary>
    /// Defines directory <see cref="IFileSystemInfo"/> entry.
    /// </summary>
    public interface IDirectory : IFileSystemInfo
    {
        /// <summary>
        /// Enumerates the <see cref="IFile"/> entries in the directory.
        /// </summary>
        /// <param name="pattern">Matching pattern for the entries.</param>
        /// <returns>The enumerator to <see cref="IFile"/> entries in the directory.</returns>
        IEnumerable<IFile> EnumerateFiles(string pattern);

        /// <summary>
        /// Enumerates the <see cref="IDirectory"/> entries in the directory.
        /// </summary>
        /// <param name="pattern">Matching pattern for the entries.</param>
        /// <returns>The enumerator to <see cref="IDirectory"/> entries in the directory.</returns>
        IEnumerable<IDirectory> EnumerateDirectories(string pattern);

        /// <summary>
        /// Enumerates the <see cref="IFileSystemInfo"/> entries in the directory.
        /// </summary>
        /// <param name="pattern">Matching pattern for the entries.</param>
        /// <returns>The enumerator to <see cref="IFileSystemInfo"/> entries in the directory.</returns>
        IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(string pattern);
    }
}