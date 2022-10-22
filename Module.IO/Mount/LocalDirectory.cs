using System.Collections.Generic;
using System.IO;
using System.Linq;
using Depra.Data.Domain.Mount;

namespace Depra.Data.Module.IO.Mount
{
    public class LocalDirectory : IDirectory
    {
        private List<IFile> _files;
        private List<LocalDirectory> _directories;
        private readonly SearchOption _searchOption;

        public string Name { get; }
        public string FullPath { get; }

        public bool Exists => Directory.Exists(FullPath);

        public IDirectory Parent { get; }

        public IMountPoint MountPoint { get; }

        public FileSystemInfoKind Kind => FileSystemInfoKind.DIRECTORY;

        public LocalDirectory(string name, string path, SearchOption searchOption, IDirectory parent = null,
            IMountPoint mountPoint = null)
        {
            Name = name;
            FullPath = path;
            Parent = parent;
            MountPoint = mountPoint;
            _searchOption = searchOption;

            CreateIfNotExists();
            FetchFiles("*", SearchOption.TopDirectoryOnly);
            FetchDirectories();
        }

        public IEnumerable<IFile> EnumerateFiles(string pattern)
        {
            FetchFiles(pattern, SearchOption.TopDirectoryOnly);
            return _files;
        }

        public IEnumerable<IDirectory> EnumerateDirectories(string pattern)
        {
            FetchDirectories();
            return _directories;
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(string pattern)
        {
            var result = _directories.Cast<IFileSystemInfo>().ToList();
            result.AddRange(_files);

            return result;
        }

        private void FetchFiles(string pattern, SearchOption searchOption)
        {
            if (_files == null)
            {
                _files = new List<IFile>();
            }

            var filePaths = Directory.GetFiles(FullPath, pattern, searchOption);
            foreach (var filePath in filePaths)
            {
                if (_files.Any(x => x.FullPath == filePath))
                {
                    continue;
                }

                var fileName = Path.GetFileName(filePath);
                var file = new LocalFile(fileName, filePath);
                _files.Add(file);
            }
        }

        private void FetchDirectories()
        {
            if (_directories == null)
            {
                _directories = new List<LocalDirectory>();
            }

            var directories = Directory.GetDirectories(FullPath);
            foreach (var directoryPath in directories)
            {
                if (_directories.Any(x => x.FullPath == directoryPath))
                {
                    continue;
                }

                var directoryName = Path.GetDirectoryName(directoryPath);
                var directory = new LocalDirectory(directoryName, directoryPath, _searchOption);
                _directories.Add(directory);
            }
        }

        private void CreateIfNotExists()
        {
            if (Exists == false)
            {
                Directory.CreateDirectory(FullPath);
            }
        }

        private IDirectory FindParentDirectory()
        {
            var parentInfo = Directory.GetParent(FullPath);
            if (parentInfo == null)
            {
                return null;
            }

            var parentDirectory = new LocalDirectory(parentInfo.Name, parentInfo.FullName, _searchOption);
            return parentDirectory;
        }
    }
}