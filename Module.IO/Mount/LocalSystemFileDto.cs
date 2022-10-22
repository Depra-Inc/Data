namespace Depra.Data.Module.IO.Mount
{
    public class LocalSystemFileDto
    {
        public string Name { get; }

        public string FullPath { get; }
        
        public string Extension { get; }

        public LocalSystemFileDto(string name, string fullPath, string extension)
        {
            Name = name;
            FullPath = fullPath;
            Extension = extension;
        }
    }
}