using System.Collections.Generic;

namespace Depra.Data.Storage.Api
{
    public interface IDataStorage
    {
        void SaveData(string name, object data);
        
        object LoadData(string name, object defaultValue);
        
        void RemoveData(string name);

        void Clear();
        
        IEnumerable<string> GetAllKeys();
    }
}