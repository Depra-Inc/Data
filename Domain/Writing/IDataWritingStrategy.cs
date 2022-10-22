namespace Depra.Data.Domain.Writing
{
    public interface IDataWritingStrategy
    {
        void WriteData<TData>(string dataName, TData data);
    }
}