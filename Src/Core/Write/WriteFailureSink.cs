using System;
using AutoSqlSync.Core.DaBasics;
using AutoSqlSync.Core.Progress;
using AutoSqlSync.Core.Retrying;

namespace AutoSqlSync.Core.Write
{
    class WriteFailureSink : FailureSink
    {
        private readonly DataProblemExpert dataProblemExpert;
        private readonly ProgressMeter progress;
        public WriteFailureSink(DataProblemExpert dataProblemExpert,ProgressMeter progress)
        {
            this.dataProblemExpert = dataProblemExpert;
            this.progress = progress;
        }

        public Retry Failed(Exception x)
        {
            var dataProblem=dataProblemExpert.IsDataProblem(x);

            if (dataProblem)
            {
                progress.InvalidData();
                return Retry.No;
            }
            else
            {
                progress.WriteState = WriteState.Failed;
                progress.Failed();
                return Retry.Yes;
            }
        }
    }
}
