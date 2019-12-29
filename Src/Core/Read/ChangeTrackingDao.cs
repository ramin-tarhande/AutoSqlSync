using System.Collections.Generic;
using System.Linq;
using AutoSqlSync.Core.Changes;
using AutoSqlSync.Core.DaBasics;
using AutoSqlSync.Core.Schemas;

namespace AutoSqlSync.Core.Read
{
    class ChangeTrackingDao
    {
        private readonly RobustBasicOperations basics;
        private readonly CtSqlExpert ctSqlExpert;
        public ChangeTrackingDao(RobustBasicOperations basics)
        {
            this.basics=basics;
            ctSqlExpert=new CtSqlExpert();
        }

        internal Version GetLastExistingVersion()
        {
            var v = basics.Query<long>(
                new Sql("select CHANGE_TRACKING_CURRENT_VERSION()"),"GetLastExistingVersion")
                          .FirstOrDefault();

            return new Version(v);
        }

        internal IEnumerable<Change> Load(SourceTableDef table,VersionRange versionRange)//, Version changesVersion)
        {
            var rows = LoadCore(table, versionRange);

            var changes = Convert(rows, table);
            
            return changes;
        }

        IEnumerable<object> LoadCore(SourceTableDef table, VersionRange versionRange)
        {
            var sql = ctSqlExpert.CreatSql(table, versionRange);

            return basics.Query<object>(sql,"LoadTrackingData");

        }

        static IEnumerable<Change> Convert(IEnumerable<object> rows, SourceTableDef table)
        {
            return rows.Select(rowData =>
                ChangeFactory.Create(rowData, table))
                .ToList();
        }
    }
}
