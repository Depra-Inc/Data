namespace Depra.Data.Storage.Api.Saving
{
    public interface IDataSaver
    {
        void SaveData<TData>(string name, TData data);

        void RemoveData(string name);
    }
}