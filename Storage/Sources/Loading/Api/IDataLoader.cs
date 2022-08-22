namespace Depra.Data.Storage.Loading.Api
{
    public interface IDataLoader
    {
        object LoadData(string name, object defaultValue);
    }
}