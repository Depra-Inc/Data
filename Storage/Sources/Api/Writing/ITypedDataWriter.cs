namespace Depra.Data.Storage.Api.Writing
{
    public interface ITypedDataWriter<in TData>
    {
        void WriteData(string path, TData data);
    }
}