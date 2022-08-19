namespace Depra.Data.Persistent.Api
{
    public interface ILocationProvider
    {
        bool ContainsFile(string fileName);

        string CombineFullFilePath(string fileName);
    }
}