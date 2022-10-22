namespace Depra.Data.Domain.Reading
{
    public interface IDataReadingStrategy
    {
        TData ReadData<TData>(string dataPath);
    }
}