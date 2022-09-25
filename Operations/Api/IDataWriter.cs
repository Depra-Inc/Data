namespace Depra.Data.Operations.Api
{
    public interface IDataWriter<in TData>
    {
        void WriteData(string dataName, TData data);
    }
}