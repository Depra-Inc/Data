using System;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Saving;

namespace Depra.Data.Storage.Api.Builders
{
    public interface IDataStorageBuilder
    {
        IDataStorageBuilder SetLocation(ILocationProvider locationProvider);

        IDataStorageBuilder SetLoader(Action<IDataLoaderBuilder> configure);

        IDataStorageBuilder SetSaver(Action<IDataSaverBuilder> configure);

        IDataStorage Build();
    }
}