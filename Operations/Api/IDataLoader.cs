namespace Depra.Data.Operations.Api
{
    public interface IDataLoader
    {
        void AddReader<TData>(IDataReader<TData> reader);
        
        TData LoadData<TData>(string name, TData defaultValue);
    }
}