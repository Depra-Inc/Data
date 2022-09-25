using System.Collections.Generic;

namespace Depra.Data.Operations.Api
{
    public interface IDataDirectory
    {
        bool ContainsDataByName(string fileName);

        string CombineFullFilePath(string fileName);

        IEnumerable<string> GetAllNames();
    }
}