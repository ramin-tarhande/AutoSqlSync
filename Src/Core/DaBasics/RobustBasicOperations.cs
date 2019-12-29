using System.Collections.Generic;
using System.Diagnostics;
using AutoSqlSync.Core.Retrying;

namespace AutoSqlSync.Core.DaBasics
{
    class RobustBasicOperations
    {
        private readonly BasicOperations inner;
        private readonly RetryingExecuter retryingExecuter;
        public RobustBasicOperations(BasicOperations inner, RetryingExecuter retryingExecuter)
        {
            this.inner = inner;
            this.retryingExecuter = retryingExecuter;
        }

        internal bool Execute(Sql sql,string operationName)
        {
            /*retryingExecuter.Execute(
                () => {
                        if (operationName == "delete") throw new Exception("bad delete");
                        inner.Execute(sqlText, parameters);
                       },operationName);
            return;*/

            var r=retryingExecuter.Execute(
                () => inner.Execute(sql), operationName);

            return r;
        }

        internal IEnumerable<T> Query<T>(Sql sql,string operationName)
        {
            IEnumerable<T> data=null;

            var r=retryingExecuter.Execute(
                () => data = inner.Query<T>(sql), operationName);

            Trace.Assert(r,"Queries are supposed to succeed (maybe after some retries)");

            return data;
        }
    }
}
