namespace Depra.Data.Domain.Mount
{
    /// <summary>
    /// Defines a file system entry.
    /// </summary>
    public interface IFileSystemInfo
    {
        /// <summary>
        /// Returns true if file system entry exists, false otherwise.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets file system entry name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the path to file system entry inside mount point <see cref="IMountPoint"/>
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Gets parent directory of file system entry.
        /// </summary>
        IDirectory Parent { get; }

        /// <summary>
        /// Gets mount point for file system entry.
        /// </summary>
        IMountPoint MountPoint { get; }

        /// <summary>
        /// Gets file system entry kind.
        /// </summary>
        FileSystemInfoKind Kind { get; }
    }
}