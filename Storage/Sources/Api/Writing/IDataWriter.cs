namespace Depra.Data.Storage.Api.Writing
{
    public interface IDataWriter<in TData>
    {
        void WriteData(string dataName, TData data);
    }
}