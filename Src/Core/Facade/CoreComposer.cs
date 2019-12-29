using System;
using AutoSqlSync.Core.Buffers;
using AutoSqlSync.Core.DaBasics;
using AutoSqlSync.Core.Progress;
using AutoSqlSync.Core.Read;
using AutoSqlSync.Core.Retrying;
using AutoSqlSync.Core.Schemas;
using AutoSqlSync.Core.Tools;
using AutoSqlSync.Core.Write;
using log4net;

namespace AutoSqlSync.Core.Facade
{
    class CoreComposer
    {
        private Sleeper sleeper;
        private ProgressMeter progress;
        private ChangesBuffer buffer;

        private readonly SyncSchema schema;
        private readonly Action quitApp;
        private readonly CoreSettings settings;
        private readonly ILog log;
        public CoreComposer(SyncSchema schema, Action quitApp, CoreSettings settings, ILog log)
        {
            this.quitApp = quitApp;
            this.schema = schema;
            this.settings = settings;
            this.log = log;
        }

        public CoreFacade Compose()
        {
            CreateCommonObjects();
            
            var readDriver = CreateReadPart();

            var writeDriver = CreateWritePart();

            return new CoreFacade(readDriver,writeDriver,buffer,progress,log);
        }

        void CreateCommonObjects()
        {
            buffer = new ChangesBuffer(settings,log);
            progress=new ProgressMeter(buffer,settings);
            sleeper = new Sleeper(log);
            //retryingExecuter = new retryingExecuter(progress,settings, sleeper, log);
            
        }

        ReadDriver CreateReadPart()
        {
            var failureSink=new ReadFailureSink(progress);
            var retryingExecuter = new RetryingExecuter(failureSink, settings, sleeper, log);

            var basics = CreateRobustBasicOperations(settings.SourceConnectionString, retryingExecuter);
            
            var changeTrackingDao = new ChangeTrackingDao(basics);
            var versionRangeExpert = new VersionRangeExpert(changeTrackingDao, settings, log);

            var reader = new Reader(changeTrackingDao, versionRangeExpert, schema, buffer,sleeper,
                progress,settings, log);
            var readDriver = new ReadDriver(reader,retryingExecuter, quitApp, log);

            return readDriver;
        }

        WriteDriver CreateWritePart()
        {
            var failureSink = new WriteFailureSink(new DataProblemExpert(),  progress);

            var retryingExecuter = new RetryingExecuter(failureSink, settings, sleeper, log);

            var basics = CreateRobustBasicOperations(settings.DestinationConnectionString, retryingExecuter);

            var upsertDao = new UpsertDao(basics);
            var deleteDao = new DeleteDao(basics);

            var writer=new Writer(upsertDao,deleteDao,buffer,progress,log);
            var writeDriver = new WriteDriver(writer,retryingExecuter, quitApp, log);

            return writeDriver;
        }

        RobustBasicOperations CreateRobustBasicOperations(string connectionString, RetryingExecuter retryingExecuter)
        {
            var connectionFactory = new DbConnectionFactory(connectionString, log);

            var basicOperations = new BasicOperations(connectionFactory, settings.DbCommandTimeout_s, log);

            //var retryingExecuter = new retryingExecuter(progress, settings, sleeper, log);

            return new RobustBasicOperations(basicOperations, retryingExecuter);
        }
    }
}
