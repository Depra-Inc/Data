namespace Depra.Data.Domain.Mount
{
    /// <summary>
    /// Defines the kind of a <see cref="IFileSystemInfo"/> entry.
    /// </summary>
    public enum FileSystemInfoKind
    {
        /// <summary>
        /// Entry is a file.
        /// </summary>
        FILE,

        /// <summary>
        /// Entry is a dictionary.
        /// </summary>
        DIRECTORY
    }
}