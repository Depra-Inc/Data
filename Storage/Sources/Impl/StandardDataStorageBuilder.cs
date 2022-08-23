using System;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Builders;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Saving;
using Depra.Data.Storage.Impl.Loading;
using Depra.Data.Storage.Impl.Saving;

namespace Depra.Data.Storage.Impl
{
    public class StandardDataStorageBuilder : IDataStorageBuilder
    {
        private IDataSaver _saver;
        private IDataLoader _loader;
        private ILocationProvider _location;

        public IDataStorageBuilder SetLocation(ILocationProvider locationProvider)
        {
            _location = locationProvider;
            return this;
        }

        public IDataStorageBuilder SetLoader(Action<IDataLoaderBuilder> configure)
        {
            var builder = new StandardDataLoaderBuilder();
            configure(builder);
            _loader = builder.BuildFor(_location);

            return this;
        }

        public IDataStorageBuilder SetSaver(Action<IDataSaverBuilder> configure)
        {
            var builder = new StandardDataSaverBuilder();
            configure(builder);
            _saver = builder.BuildFor(_location);

            return this;
        }

        public IDataStorage Build()
        {
            Validate();
            var storage = new LocalDataStorage(_location, _saver, _loader);
            
            return storage;
        }

        private void Validate()
        {
            if (_location == null)
            {
                throw new NullReferenceException("Required data location not set!");
            }

            if (_loader == null)
            {
                throw new NullReferenceException("Required data loader not set!");
            }

            if (_saver == null)
            {
                throw new NullReferenceException("Required data saver not set!");
            }
        }
    }
}