using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.Api.Loading
{
    public interface IDataLoaderBuilder
    {
        IDataLoader Build();

        IDataLoaderBuilder AddReader<TData>(IDataReader<TData> reader);
    }
}