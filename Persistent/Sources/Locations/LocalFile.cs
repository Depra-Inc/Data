using Depra.Data.Persistent.Api;

namespace Depra.Data.Persistent.Locations
{
    public readonly struct LocalFile : IDataProvider
    {
        public object Data { get; }
        
        public ILocationProvider Location { get; }

        public LocalFile(object data, ILocationProvider location)
        {
            Data = data;
            Location = location;
        }
    }
}