using System;
using AutoSqlSync.Core.Tools;
using log4net;

namespace AutoSqlSync.Core.Retrying
{
    class RetryingExecuter
    {
        private bool stop;

        private readonly FailureSink failureSink;
        private readonly CoreSettings settings;
        private readonly Sleeper sleeper;
        private readonly ILog log;
        public RetryingExecuter(FailureSink failureSink, CoreSettings settings, Sleeper sleeper, ILog log)
        {
            this.failureSink = failureSink;
            this.settings = settings;
            this.sleeper = sleeper;
            this.log = log;
        }

        public void Stop()
        {
            stop = true;
        }

        public bool Execute(Action operation, string operationName)
        {
            var logfailure = operationName + " failed";

            TryResult tr;
            do
            {
                tr = TryToDo(operation, logfailure);

                if (tr.ShouldRetry())
                {
                    this.sleeper.Sleep(this.settings.FailureSleepTime,"failure");
                }

            } while (tr.ShouldRetry() && !stop);

            return tr.Success;
        }

        TryResult TryToDo(Action operation, string logfailure)
        {
            try
            {
                operation();

                return new TryResult(true, Retry.No); 
            }
            catch (Exception x)
            {
                log.Info(logfailure, x);

                //progress.Failed();

                var retry=failureSink.Failed(x);

                return new TryResult(false, retry); 
            }
        }
    }
}
