using Depra.Data.Persistent.Api;

namespace Depra.Data.Persistent.Extensions
{
    public static class PersistentLocalStorageExtensions
    {
        public static void SaveData<T>(this IPersistentLocalStorage storage, string name, T data) =>
            storage.SaveData(name, data);

        public static T LoadData<T>(this IPersistentLocalStorage storage, string name, T defaultValue) =>
            (T)storage.LoadData(name, defaultValue);
    }
}