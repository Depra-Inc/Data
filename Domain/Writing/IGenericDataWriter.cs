namespace Depra.Data.Domain.Writing
{
    public interface IGenericDataWriter
    {
        void WriteData<TData>(string dataName, TData data);
    }
}