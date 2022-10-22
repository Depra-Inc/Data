namespace Depra.Data.Domain.Reading
{
    public interface IGenericDataReader
    {
        bool CanRead<TData>(string dataName);

        TData ReadData<TData>(string dataName);
    }
}