using System;
using Depra.Data.Operations.Api;

namespace Depra.Data.Storage.Api
{
    public interface IDataStorageBuilder
    {
        IDataStorage Build();
        
        IDataStorageBuilder AddLoader(Action<IDataLoaderBuilder> configureLoader);

        IDataStorageBuilder AddSaver(Action<IDataSaverBuilder> configureSaver);
    }
}