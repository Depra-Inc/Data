using Depra.Data.Storage.Api;

namespace Depra.Data.Storage.Extensions
{
    public static class DataStorageExtensions
    {
        public static void SaveData<T>(this IDataStorage storage, string name, T data) =>
            storage.SaveData(name, data);

        public static T LoadData<T>(this IDataStorage storage, string name, T defaultValue) =>
            (T)storage.LoadData(name, defaultValue);
    }
}