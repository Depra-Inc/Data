namespace Depra.Data.Domain.Reading
{
    public interface IDataReaderByType
    {
        IGenericDataReader GetValue();

        void SetValue(IGenericDataReader genericDataReader);
        
        ITypedDataReader<TData> GetValue<TData>();

        void SetValue<TData>(ITypedDataReader<TData> typedDataReader);
    }
}