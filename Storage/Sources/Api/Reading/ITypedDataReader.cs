namespace Depra.Data.Storage.Api.Reading
{
    public interface ITypedDataReader<out TData>
    {
        TData ReadData(string path);
    }
}