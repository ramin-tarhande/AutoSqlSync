using System;
using AutoSqlSync.Core.Retrying;
using AutoSqlSync.Core.Tools;
using log4net;

namespace AutoSqlSync.Core.Write
{
    class WriteDriver
    {
        private readonly ThreadRunner threadRunner;
        private readonly RetryingExecuter retryingExecuter;
        public WriteDriver(Writer writer, RetryingExecuter retryingExecuter, Action quitApp, ILog log)
        {
            this.retryingExecuter = retryingExecuter;

            threadRunner = new ThreadRunner(
                writer.Write, "write", quitApp, log);
        }

        public void Start()
        {
            threadRunner.Start();
        }

        public void Stop()
        {
            retryingExecuter.Stop();
            threadRunner.Stop();
        }
    }
}
