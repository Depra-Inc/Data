using System.Collections.Generic;
using Depra.Data.Persistent.Api;
using Depra.Data.Persistent.Exceptions;
using Depra.Data.Persistent.Locations;

namespace Depra.Data.Persistent.Storages
{
    public class PersistentLocalStorage : IPersistentLocalStorage
    {
        private readonly IDataReader _dataReader;
        private readonly IDataWriter _dataWriter;
        private readonly ILocationProvider _location;
        private readonly IDictionary<string, IDataProvider> _values;

        public IEnumerable<string> AllIdents => _values.Keys;

        public void SaveData(string name, object data)
        {
            if (_values.TryGetValue(name, out var provider) && provider.Data != data)
            {
                _values[name] = new LocalFile(data, _location);
                return;
            }

            AddDataEntry(name, data);
        }

        private void AddDataEntry(string name, object data)
        {
            var path = _location.CombineFullFilePath(name);
            var dataEntry = new LocalFile(data, _location);
            _dataWriter.WriteData(path, data);
            _values.Add(name, dataEntry);
        }

        public object LoadData(string name, object defaultValue)
        {
            if (_values.TryGetValue(name, out var provider))
            {
                return provider.Data;
            }

            object data;
            if (_location.ContainsFile(name))
            {
                data = _dataReader.ReadData(name) ?? defaultValue;
            }
            else
            {
                data = defaultValue;
            }

            _values.Add(name, new LocalFile(data, _location));

            return data;
        }

        public void Remove(string name)
        {
            if (_location.ContainsFile(name))
            {
                throw new InvalidPathException(name);
            }

            _dataWriter.ClearData(name);
        }

        public void Clear()
        {
            foreach (var path in _values.Keys)
            {
                Remove(path);
            }
            
            _values.Clear();
        }

        public PersistentLocalStorage(ILocationProvider location, IDataReader reader, IDataWriter writer)
        {
            _location = location;
            _dataReader = reader;
            _dataWriter = writer;

            _values = new Dictionary<string, IDataProvider>();
        }
    }
}