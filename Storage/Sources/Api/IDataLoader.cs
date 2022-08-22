namespace Depra.Data.Storage.Api
{
    public interface IDataLoader
    {
        object LoadData(string name, object defaultValue);
    }
}