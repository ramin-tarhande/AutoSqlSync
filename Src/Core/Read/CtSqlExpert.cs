using System.Collections.Generic;
using System.Linq;
using AutoSqlSync.Core.DaBasics;
using AutoSqlSync.Core.Schemas;
using Dapper;

namespace AutoSqlSync.Core.Read
{
    class CtSqlExpert
    {
        public Sql CreatSql(SourceTableDef table, VersionRange versionRange)
        {
            var builder = new SqlBuilder();

            var header = string.Format(
                "SELECT /**select**/ FROM CHANGETABLE(CHANGES {0},@lowerVersion) as c /**leftjoin**/ /**where**/ /**orderby**/"
                , table.Name);

            var t = builder.AddTemplate(header, new { lowerVersion = versionRange.Lower.Value });

            builder.Select(
                FieldsFromChangeTable(table));

            builder.Select(
                FieldsFromSourceTable(table));

            builder.Select(
                SpecialFieldNames.RowCtVersion);
            
            var joinText = string.Format("{0} as s on {1}", table.Name, CreateOnForJoin(table.PkFields));

            builder.LeftJoin(joinText);

            builder.Where("SYS_CHANGE_VERSION<=@upperVersion", new { upperVersion = versionRange.Upper.Value });

            builder.OrderBy(SpecialFieldNames.RowCtVersion);

            return new Sql(t.RawSql, t.Parameters);
        }

        static string FieldsFromChangeTable(SourceTableDef table)
        {
            var fields = table.PkFields.Select(f => string.Format("c.{0} as {1}", f,PkPrefixer.Prefix(f))).ToList();
            return string.Join(",", fields);
        }

        static string FieldsFromSourceTable(SourceTableDef table)
        {
            var all = table.AllFields.Select(f => string.Format("s.{0}", f)).ToList();
            
            return string.Join(",", all);
        }

        static string CreateOnForJoin(IEnumerable<string> pkFields)
        {
            var equals = pkFields.Select(f => string.Format("c.{0}=s.{0}", f)).ToList();

            return string.Join(" and ", equals);
        }
    }
}
