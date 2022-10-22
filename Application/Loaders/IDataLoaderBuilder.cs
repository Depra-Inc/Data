using Depra.Data.Domain.Loading;
using Depra.Data.Domain.Reading;

namespace Depra.Data.Application.Loaders
{
    public interface IDataLoaderBuilder
    {
        IDataLoader Build();

        IDataLoaderBuilder With(IGenericDataReader genericDataReader);
        
        IDataLoaderBuilder With<TData>(ITypedDataReader<TData> typedDataReader);
    }
}