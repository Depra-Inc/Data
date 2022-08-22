using System.Collections.Generic;

namespace Depra.Data.Storage.Middleware.Api
{
    public interface ILocationProvider
    {
        IEnumerable<string> ScanFilenames();

        bool ContainsDataByName(string key);

        string CombineFullFilePath(string key);
    }
}