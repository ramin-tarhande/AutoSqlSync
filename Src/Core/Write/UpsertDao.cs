using System.Text;
using AutoSqlSync.Core.DaBasics;
using AutoSqlSync.Core.FieldsData;

namespace AutoSqlSync.Core.Write
{
    class UpsertDao
    {
        private readonly RobustBasicOperations basics;
        public UpsertDao(RobustBasicOperations basics)
        {
            this.basics = basics;
        }

        /*
        IF EXISTS (SELECT * FROM Employee WHERE ID1 = @ID1 AND ID2 = @ID2)
            UPDATE Employee
            SET Col1 = Val1, Col2 = Val2, ...., ColN = ValN
            WHERE ID1 = @ID1 AND ID2 = @ID2
        ELSE
            INSERT INTO dbo.Employee(Col1, ..., ColN)
            VALUES(Val1, .., ValN)
        */

        internal bool Upsert(string tableName, FieldDataSet allFieldsData, FieldDataSet destiLookupData) //FieldDataSet srcPkFieldsData, 
        {
            var sqlText = CreatSqlText(tableName, allFieldsData, destiLookupData);

            var parameters = allFieldsData.CreateParams();

            var r = basics.Execute(new Sql(sqlText, parameters), "upsert");

            return r;
        }

        static string CreatSqlText(string tableName, FieldDataSet allFieldsData, FieldDataSet destiLookupData)
        {
            var r = new StringBuilder();

            var ifUpdate = CreateIfUpdate(tableName, allFieldsData, destiLookupData);

            r.Append(ifUpdate);
            /*
            var srcPkIfUpdate=CreateUpdateIfUpdate(tableName, allFieldsData, srcPkFieldsData);

            r.Append(srcPkIfUpdate);

            if (destiPkData != null)
            {
                r.Append("\nELSE ");

                var destiPkIfUpdate = CreateUpdateIfUpdate(tableName, allFieldsData, destiPkData);

                r.Append(extraIfUpdate);
            }
            */
            r.Append("\nELSE\n");

            r.Append(
                CreateInsert(tableName, allFieldsData));


            return r.ToString();
        }

        static string CreateIfUpdate(string tableName, FieldDataSet allFieldsData,FieldDataSet whereFieldsData)
        {
            var r = new StringBuilder();

            var srcPkWhere = whereFieldsData.CreateWhere();
            
            r.AppendFormat("IF EXISTS (SELECT * FROM {0} {1})\n", tableName, srcPkWhere);

            r.Append(
                CreateUpdate(tableName, allFieldsData, srcPkWhere));

            return r.ToString();
        }


        static string CreateUpdate(string tableName, FieldDataSet allFieldsData, string where)
        {
            var equalsText = allFieldsData.CreateSqlText((fn, pv) => string.Format("{0}={1}", fn, pv), ", ");

            return string.Format("UPDATE {0}\nSET {1}\n{2}", tableName, equalsText, where);
        }


        static string CreateInsert(string tableName, FieldDataSet allFieldsData)
        {
            var fieldNames = allFieldsData.CreateSqlText((fn, pv) => fn, ", ");

            var fieldValues = allFieldsData.CreateSqlText((fn, pv) => pv, ", ");

            return string.Format("INSERT INTO {0}({1})\nVALUES({2})",
                tableName, fieldNames, fieldValues);
        }
    }
}
