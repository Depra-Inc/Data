namespace Depra.Data.Operations.Api
{
    public interface IDataSaverBuilder
    {
        IDataSaver Build();
        
        IDataSaverBuilder AddWriter<TData>(IDataWriter<TData> writer);
    }
}