namespace Depra.Data.Domain.Writing
{
    public interface IDataWriterByType
    {
        IGenericDataWriter GetValue();

        void SetValue(IGenericDataWriter genericDataWriter);
        
        ITypedDataWriter<TData> GetValue<TData>();

        void SetValue<TData>(ITypedDataWriter<TData> value);
    }
}