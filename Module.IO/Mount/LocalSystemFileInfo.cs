using System.IO;
using Depra.Data.Domain.Mount;

namespace Depra.Data.Module.IO.Mount
{
    public class LocalSystemFileInfo : LocalSystemFileDto, IFileSystemInfo
    {
        public bool Exists => File.Exists(FullPath);

        public IDirectory Parent { get; }

        public IMountPoint MountPoint { get; }

        public FileSystemInfoKind Kind => FileSystemInfoKind.FILE;

        public LocalSystemFileInfo(string name, string path, IDirectory parent = null, IMountPoint mountPoint = null) : base(name, path, Path.GetFileNameWithoutExtension(path))
        {
            Parent = parent;
            MountPoint = mountPoint;
        }
    }
}