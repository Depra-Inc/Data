using System.Collections.Generic;

namespace Depra.Data.Persistent.Api
{
    public interface IPersistentLocalStorage
    {
        IEnumerable<string> AllIdents { get; }

        void SaveData(string name, object data);
        
        object LoadData(string name, object defaultValue);
        
        void Remove(string name);

        void Clear();
    }
}