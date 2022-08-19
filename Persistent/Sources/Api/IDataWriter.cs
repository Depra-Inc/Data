namespace Depra.Data.Persistent.Api
{
    public interface IDataWriter
    {
        void WriteData(string uri, object data);

        void ClearData(string uri);
    }
}