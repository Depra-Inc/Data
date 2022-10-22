namespace Depra.Data.Domain.Writing
{
    public interface ITypedDataWriter<in TData>
    {
        void WriteData(string dataName, TData data);
    }
}