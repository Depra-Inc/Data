namespace Depra.Data.Operations.Api
{
    public interface IDataSaver
    {
        void AddWriter<TData>(IDataWriter<TData> writer);
        
        void SaveData<TData>(string name, TData data);
    }
}