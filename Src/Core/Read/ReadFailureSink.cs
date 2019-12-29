using System;
using AutoSqlSync.Core.Progress;
using AutoSqlSync.Core.Retrying;

namespace AutoSqlSync.Core.Read
{
    class ReadFailureSink : FailureSink
    {
        private readonly ProgressMeter progress;
        public ReadFailureSink(ProgressMeter progress)
        {
            this.progress = progress;
        }

        public Retry Failed(Exception x)
        {
            progress.ReadState = ReadState.Failed;

            progress.Failed();

            return Retry.Yes;
        }
    }
}
