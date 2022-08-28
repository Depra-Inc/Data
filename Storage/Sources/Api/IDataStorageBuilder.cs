using System;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Saving;

namespace Depra.Data.Storage.Api
{
    public interface IDataStorageBuilder
    {
        IDataStorage Build();
        
        IDataStorageBuilder AddLoader(Action<IDataLoaderBuilder> configureLoader);

        IDataStorageBuilder AddSaver(Action<IDataSaverBuilder> configureSaver);
    }
}