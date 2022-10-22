using System;
using System.Collections.Generic;
using Depra.Data.Application.Cache;

namespace Depra.Data.Application.UnitTests.Helpers;

internal static class Helper
{
    public static void WarpUpStorage(CacheDataStorage dataStorage, IEnumerable<string> dataKeys, Func<object> dataFactory)
    {
        foreach (var key in dataKeys)
        {
            dataStorage.SaveData(key, dataFactory());
        }
    }
}