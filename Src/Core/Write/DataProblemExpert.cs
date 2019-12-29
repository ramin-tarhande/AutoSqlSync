using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace AutoSqlSync.Core.Write
{
    class DataProblemExpert
    {
        private readonly HashSet<int> dataProblems;
        public DataProblemExpert()
        {
            dataProblems = new HashSet<int>()
            {
                2627, //pk
                2601, //unique index
                245, //conversion
                547, //constraint/fk
                515, //not nullable
                8152  //data would be truncated
            };
        }

        public bool IsDataProblem(Exception x)
        {
            var sx = x as SqlException;
            if (sx!=null)
            {
                return IsSqlExceptionDataProblem(sx);
            }

            if (x is SqlTypeException)
            {
                return true;
            }
            
            return false;
        }

        bool IsSqlExceptionDataProblem(SqlException sx)
        {
            return dataProblems.Contains(sx.Number);
            //return sx.Class == 14 || sx.Class == 16;
        }
    }
}
