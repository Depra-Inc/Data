namespace Depra.Data.Storage.Saving.Api
{
    public interface IDataSaver
    {
        void SaveData(string name, object data);

        void RemoveData(string name);
    }
}