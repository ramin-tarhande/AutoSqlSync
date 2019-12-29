using System;

namespace AutoSqlSync.Core.Retrying
{
    public interface FailureSink
    {
        Retry Failed(Exception x);
    }
}