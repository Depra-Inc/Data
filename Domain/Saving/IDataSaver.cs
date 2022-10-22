namespace Depra.Data.Domain.Saving
{
    public interface IDataSaver
    {
        void SaveData<TData>(string name, TData data);
    }
}