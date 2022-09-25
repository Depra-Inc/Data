namespace Depra.Data.Operations.Api
{
    public interface ICleaningRule
    {
        void Clear();
        
        void DeleteData(string name);
    }
}