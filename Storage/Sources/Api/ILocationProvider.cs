using System.Collections.Generic;

namespace Depra.Data.Storage.Api
{
    public interface ILocationProvider
    {
        void Remove(string fileName);
        
        IEnumerable<string> ScanFilenames();
        
        bool ContainsDataByName(string fileName);

        string CombineFullFilePath(string fileName);
    }
}