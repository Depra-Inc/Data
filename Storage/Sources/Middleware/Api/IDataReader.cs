namespace Depra.Data.Storage.Middleware.Api
{
    public interface IDataReader
    {
        object ReadData(string path);
    }
}