using System.Collections.Generic;

namespace AutoSqlSync.Core.Schemas
{
    public class SyncSchema
    {
        public IEnumerable<SourceTableDef> SourceTables { get; private set; }
        public SyncSchema(IEnumerable<SourceTableDef> sourceTables)
        {
            SourceTables = sourceTables;
        }
    }
}
