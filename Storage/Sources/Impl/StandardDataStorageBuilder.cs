﻿using System;
using Depra.Data.Storage.Api;
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
        private  IDataStorage _dataStorage;

        public static IDataStorageBuilder Configure(ILocationProvider location, Action<IDataStorageBuilder> configure)
        {
            var builder = new StandardDataStorageBuilder();
            configure?.Invoke(builder);
            builder._dataStorage = new LocalDataStorage(location, builder._saver, builder._loader);
            
            return builder;
        }

        public IDataStorage Build()
        {
            Validate();
            return _dataStorage;
        }

        public IDataStorageBuilder AddLoader(Action<IDataLoaderBuilder> configureLoader)
        {
            _loader = StandardDataLoaderBuilder.Configure(configureLoader).Build();
            return this;
        }

        public IDataStorageBuilder AddSaver(Action<IDataSaverBuilder> configureSaver)
        {
            _saver = StandardDataSaverBuilder.Configure(configureSaver).Build();
            return this;
        }

        private StandardDataStorageBuilder()
        {
        }

        private void Validate()
        {
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