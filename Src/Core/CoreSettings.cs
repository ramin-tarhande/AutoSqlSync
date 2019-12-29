using System;

namespace AutoSqlSync.Core
{
    public interface CoreSettings
    {
        string SourceConnectionString { get; set; }
        string DestinationConnectionString { get; set; }

        int? DbCommandTimeout_s { get; set; }

        //TimeSpan SourcePollingInterval { get; set; }

        int BufferSizeThreshold { get; set; }

        int LoadMaxVersionDiff { get; set; }

        TimeSpan ReadIdleSleepTime { get; set; }
        //TimeSpan WriteIdleSleepTime { get; set; }

        TimeSpan FailureSleepTime { get; set; }

        int CountWrapping { get; set; }
    }
}