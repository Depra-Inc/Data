namespace Depra.Data.Storage.Api.Cleaning
{
    public interface IDataCleaner
    {
        void DeleteData(string name);

        void Clear();
    }
}