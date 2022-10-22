using System.Collections.Generic;

namespace Depra.Data.Domain.Mount
{
    public interface IDirectoryScanner
    {
        bool ContainsDataByName(string dataName);
        
        string GetFullFilePath(string fileName);

        IEnumerable<string> GetAllNames();
    }
}