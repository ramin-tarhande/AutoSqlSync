using AutoSqlSync.Core.DaBasics;
using AutoSqlSync.Core.FieldsData;

namespace AutoSqlSync.Core.Write
{
    class DeleteDao
    {
        private readonly RobustBasicOperations basics;
        public DeleteDao(RobustBasicOperations basics)
        {
            this.basics = basics;
        }

        /*
            DELETE FROM Employee WHERE ID1 = @ID1 AND ID2 = @ID2
         */

        internal bool Delete(string tableName, FieldDataSet srcPkFieldsData)
        {
            var sqlText = string.Format("DELETE FROM {0} {1}",
                tableName,
                srcPkFieldsData.CreateWhere());

            var parameters = srcPkFieldsData.CreateParams();

            var r = basics.Execute(new Sql(sqlText, parameters), "delete");

            return r;
        }
    }
}
