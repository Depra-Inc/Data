namespace Depra.Data.Storage.Api.Loading
{
    public interface IDataLoader
    {
        TData LoadData<TData>(string name, TData defaultValue);
    }
}