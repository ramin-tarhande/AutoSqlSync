using System;
using System.Diagnostics;
using AutoSqlSync.Core.Buffers;
using AutoSqlSync.Core.Changes;
using AutoSqlSync.Core.FieldsData;
using AutoSqlSync.Core.Progress;
using AutoSqlSync.Core.Schemas;
using log4net;

namespace AutoSqlSync.Core.Write
{
    class Writer
    {
        private readonly UpsertDao upsertDao;
        private readonly DeleteDao deleteDao;
        private readonly ChangesBuffer buffer;
        private readonly ProgressMeter progress;
        private readonly ILog log;

        public Writer(UpsertDao upsertDao, DeleteDao deleteDao,ChangesBuffer buffer, 
            ProgressMeter progress, ILog log)
        {
            this.progress = progress;
            this.deleteDao = deleteDao;
            this.upsertDao = upsertDao;
            this.buffer = buffer;
            this.log = log;
        }

        public bool Write()
         {
            log.Debug("");
            log.Debug("Write changes");

            progress.WriteState = WriteState.Idle;

            log.Debug(" wait for data");
            var stopState=buffer.WaitForData();
            if (stopState.Stopped)
            {
                return false;
            }

            progress.WriteState = WriteState.Saving;

            log.Debug(" take data");
            var change = buffer.Take();
            Trace.Assert(change != null, "change!=null");

            //
            Apply(change);
            
             return true;
         }

        internal void Apply(Change change)
        {
            log.Debug(change.Text());

            if (change is Upsert)
            {
                ApplyUpsert((Upsert)change);
            }
            else if (change is Delete)
            {
                //throw new Exception("bad conditions");
                ApplyDelete((Delete)change);
            }
            else
            {
                throw new ArgumentException("change");
            }
        }

        void ApplyUpsert(Upsert change)
        {
            var sourceTable = change.SourceDef;
            var sourceData = change.SourceData;
            foreach (var destinationDef in sourceTable.DestinationDefs)
            {
                var def = destinationDef;

                TryApplySingle(() =>
                    {
                        var destiData = def.ComputeAll(sourceData);

                        LogDesti(def, destiData);

                        var destiLookupData = def.ComputeForDestiLookupFields(sourceData);

                        var r = upsertDao.Upsert(def.Name, destiData, destiLookupData);

                        return r;
                    }
                    , change);
            }
        }

        void ApplyDelete(Delete change)
        {
            var sourceDef = change.SourceDef;
            var sourceData = change.SourceData;
            foreach (var destinationDef in sourceDef.DestinationDefs)
            {
                var def = destinationDef;

                TryApplySingle(() =>
                {
                    var destiSrcPkData = def.ComputeForSourcePkBasedFields(sourceData);

                    LogDesti(def, destiSrcPkData);

                    var r = deleteDao.Delete(def.Name, destiSrcPkData);

                    return r;
                }
                , change);
            }
        }

        void TryApplySingle(Func<bool> apply, Change change)
        {
            try
            {
                var r = apply();

                if (r)
                {
                    progress.WriteDone();
                }
            }
            catch (FatalException)
            {
                throw;
            }
            catch (Exception x)
            {
                log.Warn("Applying change failed due to invalid data", x);
                log.WarnFormat(" change: {0}", change.Text());

                progress.InvalidData();
            }
        }

        void LogDesti(DestinationTableDef destinationDef,DestiFieldDataSet destiData)
        {
            log.DebugFormat("        {0}: {1}", destinationDef.Name, destiData.Text());
        }
    }
}
