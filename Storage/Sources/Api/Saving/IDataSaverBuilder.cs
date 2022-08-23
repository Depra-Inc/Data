using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.Api.Saving
{
    public interface IDataSaverBuilder
    {
        IDataSaverBuilder AddWriter<TData>(ITypedDataWriter<TData> writer);

        IDataSaver BuildFor(ILocationProvider location);
    }
}