using Depra.Data.Domain.Saving;
using Depra.Data.Domain.Writing;

namespace Depra.Data.Application.Savers
{
    public interface IDataSaverBuilder
    {
        IDataSaver Build();

        IDataSaverBuilder With(IGenericDataWriter genericDataReader);
        
        IDataSaverBuilder With<TData>(ITypedDataWriter<TData> writer);
    }
}