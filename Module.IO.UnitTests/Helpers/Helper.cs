using Depra.Data.Module.IO.Scanning;
using Depra.Data.Module.IO.Storage;

namespace Depra.Data.Module.IO.UnitTests.Helpers;

internal static class Helper
{
    public static void WarpUpStorage(LocalFIleDataStorage dataStorage, IEnumerable<string> dataKeys,
        Func<object> dataFactory)
    {
        foreach (var key in dataKeys)
        {
            dataStorage.SaveData(key, dataFactory());
        }
    }

    public static void FreeResources(IEnumerable<string> filesForDelete, LocalDirectoryScanner directoryScanner)
    {
        foreach (var fullFilePath in filesForDelete.Select(directoryScanner.GetFullFilePath))
        {
            File.Delete(fullFilePath);
        }
    }
}