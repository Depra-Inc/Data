using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.Api.Saving
{
    public interface IDataSaverBuilder
    {
        IDataSaver Build();
        
        IDataSaverBuilder AddWriter<TData>(IDataWriter<TData> writer);
    }
}