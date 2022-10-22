namespace Depra.Data.Domain.Loading
{
    public interface IDataLoader
    {
        TData LoadData<TData>(string name, TData defaultValue);
    }
}