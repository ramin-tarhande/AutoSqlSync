namespace AutoSqlSync.Core
{
    public interface SyncProgress
    {
        int Reads { get; }
        int Writes { get; }
        int Failures { get; }
        int Invalids { get; }
        int BufferSize { get; }

        ReadState ReadState { get; }
        WriteState WriteState { get; }
    }
}