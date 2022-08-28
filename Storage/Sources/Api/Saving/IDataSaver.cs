using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.Api.Saving
{
    public interface IDataSaver
    {
        void AddWriter<TData>(IDataWriter<TData> writer);
        
        void SaveData<TData>(string name, TData data);
    }
}