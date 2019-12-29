using System;
using System.Collections.Generic;
using System.Linq;
using AutoSqlSync.Core.Changes;
using AutoSqlSync.Core.FieldsData;
using AutoSqlSync.Core.Schemas;

namespace AutoSqlSync.Core.Read
{
    static class ChangeFactory
    {
        public static Change Create(object pRowData, SourceTableDef table)
        {
            var rowData = (IDictionary<string, object>)pRowData;

            var v = Convert.ToInt64(rowData[SpecialFieldNames.RowCtVersion]);
            var version = new Version(v);

            if (ContainsSourceTableData(rowData,table))
            {
                return new Upsert(table,
                    CreateSourceTableData(rowData, table), version);    
            }
            else
            {
                return new Delete(table,
                    CreateCtPkFieldsData(rowData, table), version);    
            }
        }

        static SourceFieldDataSet CreateSourceTableData(IDictionary<string, object> rowData,SourceTableDef table)
        {
            return table.AllFields
                    .Select(fn => new FieldData(fn, rowData[fn]))
                    .ToSourceFieldDataSet();
        }

        static SourceFieldDataSet CreateCtPkFieldsData(IDictionary<string, object> rowData, SourceTableDef table)
        {
            return table.PkFields
                    .Select(fn => new FieldData(fn, rowData[PkPrefixer.Prefix(fn)]))
                    .ToSourceFieldDataSet();
        }

        static bool ContainsSourceTableData(IDictionary<string, object> rowData, SourceTableDef table)
        {
            var pkValue = GetFieldValue(rowData, table.PkFields.First());
            return pkValue!= null;
        }

        static object GetFieldValue(IDictionary<string, object> rowData,string fieldName)
        {
            return rowData[fieldName];
        }
    }
}
