namespace Depra.Data.Operations.Api
{
    public interface IDataReader<out TData>
    {
        bool CanRead(string dataName);

        TData ReadData(string dataName);
    }

    public interface IDirectoryRule
    {
    }

    public readonly struct ReadingRule : IDirectoryRule
    {
    }

    public readonly struct WritingRule : IDirectoryRule
    {
        
    }

    public interface IReadingRule<out TData>
    {
        bool CanRead(string dataName);

        TData ReadData(string dataName);
    }

    public interface IWritingRule<in TData>
    {
        bool CanWrite(string dataName);

        void WriteData(string dataName, TData data);
    }
}