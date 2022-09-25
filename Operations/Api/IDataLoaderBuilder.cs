namespace Depra.Data.Operations.Api
{
    public interface IDataLoaderBuilder
    {
        IDataLoader Build();

        IDataLoaderBuilder AddReader<TData>(IDataReader<TData> reader);
    }
}