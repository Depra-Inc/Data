using System.Collections.Generic;

namespace Depra.Data.Domain.Storage
{
    public interface IDataStorage
    {
        void SaveData<TData>(string dataKey, TData data);
        
        TData LoadData<TData>(string dataKey, TData defaultValue);
        
        void ClearData(string dataKey);

        void ClearData();
        
        IEnumerable<string> GetAllKeys();
    }
}