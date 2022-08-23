namespace Depra.Data.Storage.Api.Reading
{
    public interface ITypedDataReader<out TData>
    {
        new TData ReadData(string path);
    }
}