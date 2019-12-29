using System;
using AutoSqlSync.Core.Retrying;
using AutoSqlSync.Core.Tools;
using log4net;

namespace AutoSqlSync.Core.Read
{
    class ReadDriver
    {
        private readonly ThreadRunner threadRunner;
        private readonly RetryingExecuter retryingExecuter;
        public ReadDriver(Reader reader, RetryingExecuter retryingExecuter, Action quitApp, ILog log)
        {
            this.retryingExecuter = retryingExecuter;
            
            threadRunner = new ThreadRunner(
                reader.Read, "read", quitApp, log);
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
