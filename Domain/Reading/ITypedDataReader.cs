namespace Depra.Data.Domain.Reading
{
    public interface ITypedDataReader<out TData>
    {
        bool CanRead(string dataName);

        TData ReadData(string dataName);
    }
}