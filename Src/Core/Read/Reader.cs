using System.Collections.Generic;
using System.Linq;
using AutoSqlSync.Core.Buffers;
using AutoSqlSync.Core.Changes;
using AutoSqlSync.Core.Progress;
using AutoSqlSync.Core.Schemas;
using AutoSqlSync.Core.Tools;
using log4net;

namespace AutoSqlSync.Core.Read
{
    class Reader
    {
        private readonly ChangeTrackingDao dao;
        private readonly VersionRangeExpert versionRangeExpert;
        private readonly SyncSchema schema;
        private readonly ChangesBuffer buffer;
        private readonly Sleeper sleeper;
        private readonly ProgressMeter progress;
        private readonly CoreSettings settings;
        private readonly ILog log;
        public Reader(ChangeTrackingDao dao, VersionRangeExpert versionRangeExpert,
            SyncSchema schema, ChangesBuffer buffer, Sleeper sleeper, ProgressMeter progress, 
            CoreSettings settings, ILog log)
        {
            this.settings = settings;
            this.sleeper = sleeper;
            this.progress = progress;
            this.versionRangeExpert = versionRangeExpert;
            this.buffer = buffer;
            this.dao = dao;
            this.schema = schema;
            this.log = log;
        }

        public bool ShouldWaitBufferFull()
        {
            return buffer.IsFull();
        }

        public bool Read()
        {
            log.Debug("");
            log.Debug("Read changes");

            log.Debug(" wait for room");
            
            progress.ReadState = ReadState.BufferFull;
            var stopState = buffer.WaitForRoom();
            if (stopState.Stopped)
            {
                return false;
            }

            log.Debug(" room exists");

            progress.ReadState = ReadState.Loading;

            var versionRange = versionRangeExpert.GetVersionRange();

            if (versionRange.IsEmpty())
            {
                progress.ReadState = ReadState.Idle;
                log.Debug("version range is empty");
                log.Debug("");
                sleeper.Sleep(settings.ReadIdleSleepTime,"readIdle");
                return true;
            }

            ReadCore(versionRange);
            
            versionRangeExpert.SyncDoneWith(versionRange);

            return true;
        }

        void ReadCore(VersionRange versionRange)
        {
            var all = LoadAll(versionRange);

            all = Order(all);

            Consume(all);
        }

        IEnumerable<Change> LoadAll(VersionRange versionRange)
        {
            var all = new List<Change>();

            foreach (var sourceTable in schema.SourceTables)
            {
                var tableChanges = dao.Load(sourceTable,versionRange: versionRange);

                //if (tableChanges.OfType<Delete>().Any()) throw new Exception("horrible conditions");

                var count = tableChanges.Count();
                log.DebugFormat("{0} changes: {1}", sourceTable.Name, count);

                all.AddRange(tableChanges);
            }

            return all;
        }

        static IEnumerable<Change> Order(IEnumerable<Change> all)
        {
            return all.OrderBy(c => c.Version.Value).ToList();
        }

        void Consume(IEnumerable<Change> changes)
        {
            var count = changes.Count();

            if (count!=0)
            {
                progress.ReadState = ReadState.Loading;

                progress.ReadDone(count);

                buffer.Put(changes);
            }
        }
    }
}
