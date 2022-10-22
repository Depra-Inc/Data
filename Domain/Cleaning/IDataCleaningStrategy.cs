namespace Depra.Data.Domain.Cleaning
{
    public interface IDataCleaningStrategy
    {
        void ClearData();

        void ClearData(string name);
    }
}