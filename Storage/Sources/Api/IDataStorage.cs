using System.Collections.Generic;

namespace Depra.Data.Storage.Api
{
    public interface IDataStorage
    {
        void SaveData<TData>(string name, TData data);
        
        TData LoadData<TData>(string name, TData defaultValue);
        
        void RemoveData(string name);

        void Clear();
        
        IEnumerable<string> GetAllKeys();
    }
}