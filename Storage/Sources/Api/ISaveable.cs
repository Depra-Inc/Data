namespace Depra.Data.Storage.Api
{
    public interface ISaveable
    {
        object CaptureState();

        void RestoreState(object state);
    }
}