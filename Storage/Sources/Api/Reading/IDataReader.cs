namespace Depra.Data.Storage.Api.Reading
{
    public interface IDataReader<out TData>
    {
        bool CanRead(string dataName);
        
        TData ReadData(string dataName);
    }
}