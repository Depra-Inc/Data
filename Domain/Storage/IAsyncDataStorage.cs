namespace Depra.Data.Domain.Storage
{
    public interface IAsyncDataStorage
    {
        void SaveDataAsync(string name, object data);

        object LoadDataAsync(string name, object defaultValue);

        void RemoveAsync(string name);

        void ClearAsync();
    }
}