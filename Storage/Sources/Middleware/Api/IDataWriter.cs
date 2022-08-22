namespace Depra.Data.Storage.Middleware.Api
{
    public interface IDataWriter
    {
        void WriteData(string uri, object data);

        void ClearData(string uri);
    }
}