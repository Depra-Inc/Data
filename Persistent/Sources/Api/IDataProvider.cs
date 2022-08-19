namespace Depra.Data.Persistent.Api
{
    public interface IDataProvider
    {
        object Data { get; }
        
        ILocationProvider Location { get; }
    }
}