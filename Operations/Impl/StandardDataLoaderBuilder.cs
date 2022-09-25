using System;
using Depra.Data.Operations.Api;

namespace Depra.Data.Operations.Impl
{
    public class StandardDataLoaderBuilder : IDataLoaderBuilder
    {
        private IDataLoader _dataLoader;

        public static IDataLoaderBuilder Configure(Action<IDataLoaderBuilder> configureAction)
        {
            var builder = new StandardDataLoaderBuilder
            {
                _dataLoader = new DataLoader()
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