﻿using System;
using Depra.Data.Storage.Api.Loading;
using Depra.Data.Storage.Api.Reading;

namespace Depra.Data.Storage.Impl.Loading
{
    public class StandardDataLoaderBuilder : IDataLoaderBuilder
    {
        private IDataLoader _dataLoader;

        public static IDataLoaderBuilder Configure(Action<IDataLoaderBuilder> configureAction)
        {
            var builder = new StandardDataLoaderBuilder
            {
                _dataLoader = new DataLoader(new DataReaderByType())
            };
            configureAction?.Invoke(builder);
            return builder;
        }

        public IDataLoader Build() => _dataLoader;

        public IDataLoaderBuilder AddReader<TData>(IDataReader<TData> reader) =>
            With(builder => builder.AddReader(reader));

        private IDataLoaderBuilder With(Action<IDataLoader> configure)
        {
            configure?.Invoke(_dataLoader);
            return this;
        }
    }
}