using System;
using AutoSqlSync.Core.Schemas;
using log4net;

namespace AutoSqlSync.Core.Facade
{
    public class CoreStarter : IDisposable
    {
        private readonly ILog log;
        private readonly SyncSchema schema;
        private readonly Action quitApp;
        private readonly CoreSettings settings;
        private readonly string appName;
        public CoreStarter(SyncSchema schema, string appName,Action quitApp, CoreSettings settings, ILog log)
        {
            //this.appName = appName;
            this.appName = appName;
            this.schema = schema;
            this.quitApp = quitApp;
            this.log = log;
            this.settings = settings;
        }

        public CoreFacade Start()
        {
            try
            {
                return StartCore();
            }
            catch (Exception x)
            {
                if (log!=null)
                {
                    log.Info(string.Format("Starting {0} failed",appName),x);
                }
                throw;
            }
        }

        CoreFacade StartCore()
        {
            log.InfoFormat("Starting {0}",appName);
            
            var cts = CreateCts();

            log.InfoFormat("{0} started",appName);
            log.Info("");

            return cts;
        }

        CoreFacade CreateCts()
        {
            var ctSyncComposer = new CoreComposer(schema, quitApp, settings, log);

            var ctsFacade = ctSyncComposer.Compose();

            return ctsFacade;
        }

        
        public void Dispose()
        {
            log.InfoFormat("{0} stopped",appName);
            log.Info("");
        }
    }
}
