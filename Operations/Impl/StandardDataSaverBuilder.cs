using System;
using Depra.Data.Operations.Api;

namespace Depra.Data.Operations.Impl
{
    public class StandardDataSaverBuilder : IDataSaverBuilder
    {
        private IDataSaver _dataSaver;

        public static StandardDataSaverBuilder Configure(Action<IDataSaverBuilder> configureAction)
        {
            var builder = new StandardDataSaverBuilder()
            {
                _dataSaver = new DataSaver()
            };
            configureAction?.Invoke(builder);
            return builder;
        }

        public IDataSaver Build() => _dataSaver;

        public IDataSaverBuilder AddWriter<TData>(IDataWriter<TData> writer) =>
            With(builder => builder.AddWriter(writer));

        private IDataSaverBuilder With(Action<IDataSaver> configure)
        {
            configure?.Invoke(_dataSaver);
            return this;
        }
    }
}