using System.IO;
using Depra.Data.Domain.Mount;

namespace Depra.Data.Module.IO.Mount
{
    public class LocalFile : LocalSystemFileInfo, IFile
    {
        public Stream OpenRead() => File.OpenRead(FullPath);

        public LocalFile(string name, string path, IDirectory parent = null, IMountPoint mountPoint = null) : base(name,
            path, parent, mountPoint) { }
    }
}