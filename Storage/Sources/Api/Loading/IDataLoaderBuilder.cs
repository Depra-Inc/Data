using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.Api.Loading
{
    public interface IDataLoaderBuilder
    {
        IDataLoaderBuilder AddReader<TData>(ITypedDataReader<TData> reader);

        IDataLoader BuildFor(ILocationProvider location);
    }
}