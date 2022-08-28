using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.Api.Loading
{
    public interface IDataLoader
    {
        void AddReader<TData>(IDataReader<TData> reader);
        
        TData LoadData<TData>(string name, TData defaultValue);
    }
}