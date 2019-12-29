using AutoSqlSync.Core.Buffers;
using AutoSqlSync.Core.Read;
using AutoSqlSync.Core.Write;
using log4net;

namespace AutoSqlSync.Core.Facade
{
    public class CoreFacade
    {
        public SyncProgress Progress { get; private set; }

        
        private readonly ReadDriver readDriver;
        private readonly WriteDriver writeDriver;
        private readonly ChangesBuffer buffer;
        private readonly ILog log;
        internal CoreFacade(ReadDriver readDriver, WriteDriver writeDriver, ChangesBuffer buffer,
            SyncProgress progress, ILog log)
        {
            this.buffer = buffer;
            this.Progress = progress;
            //this.retryingExecuter = retryingExecuter;
            this.writeDriver = writeDriver;
            this.readDriver = readDriver;
            this.log = log;
        }

        public void Start()
        {
            readDriver.Start();
            writeDriver.Start();
        }

        public void Stop()
        {
            log.Debug("");
            log.Debug("Stopping Sync");

            //retryingExecuter.Stop();
            buffer.Stop();

            readDriver.Stop();
            writeDriver.Stop();
        }
    }
}
