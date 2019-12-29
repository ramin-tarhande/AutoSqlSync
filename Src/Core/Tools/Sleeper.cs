using System;
using System.Threading;
using log4net;

namespace AutoSqlSync.Core.Tools
{
    class Sleeper
    {
        private readonly ILog log;
        public Sleeper(ILog log)
        {
            this.log = log;
        }

        public void Sleep(TimeSpan sleepTime,string context)
        {
            //log.DebugFormat("Sleep for {0} ({1})", sleepTime,context);

            Thread.Sleep(sleepTime);

            //log.DebugFormat("Sleep done ({0})", context);
        }

    }
}
