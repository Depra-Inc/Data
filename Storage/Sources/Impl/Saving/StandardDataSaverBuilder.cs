using System;
using Depra.Data.Storage.Api;
using Depra.Data.Storage.Api.Saving;
using Depra.Data.Storage.Api.Writing;

namespace Depra.Data.Storage.Impl.Saving
{
    public class StandardDataSaverBuilder : IDataSaverBuilder
    {
        private readonly DataWriterByType _dataWriters;

        public IDataSaverBuilder AddWriter<TData>(ITypedDataWriter<TData> writer)
        {
            if (_dataWriters.GetValue<TData>() != null)
            {
                throw new ArgumentException();
            }

            _dataWriters.SetValue(writer);
            return this;
        }

        public IDataSaver BuildFor(ILocationProvider location) => new DataSaver(location, _dataWriters);

        public StandardDataSaverBuilder()
        {
            _dataWriters = new DataWriterByType();
        }
    }
}