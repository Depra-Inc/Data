using System;

namespace Depra.Data.Domain.Mount
{
    /// <summary>
    /// Represents abstract directory.
    /// This could be regular folder on file system, zip file, HTTP server or anything else that can present files in file system hierarchy.
    /// </summary>
    public interface IMountPoint : IDisposable
    {
        /// <summary>
        /// Returns mount point URI.
        /// </summary>
        string MountPointUri { get; }
        
        /// <summary>
        /// Returns root directory of the mount point.
        /// </summary>
        IDirectory Root { get; }

        /// <summary>
        /// Gets the file info for the file in the mount point.
        /// </summary>
        /// <param name="path">The path to the file relative to mount point root.</param>
        /// <returns><see cref="IFile"/></returns>
        IFile FileInfo(string path);
        
        /// <summary>
        /// Gets the directory info for the directory in the mount point.
        /// </summary>
        /// <param name="path">The path to the directory relative to mount point root.</param>
        /// <returns><see cref="IDirectory"/></returns>
        IDirectory DirectoryInfo(string path);

        /// <summary>
        /// Gets the file system entry (file or directory) info for the entry in the mount point.
        /// </summary>
        /// <param name="path">The path to the file system entry relative to mount point root.</param>
        /// <returns><see cref="IFileSystemInfo"/></returns>
        IFileSystemInfo FileSystemInfo(string path);
    }
}