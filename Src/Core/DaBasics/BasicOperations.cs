using System;
using System.Collections.Generic;
using Dapper;
using log4net;

namespace AutoSqlSync.Core.DaBasics
{
    class BasicOperations
    {
        private readonly DbConnectionFactory connectionFactory;
        private readonly ILog log;
        private readonly int? commandTimeout_s;
        public BasicOperations(DbConnectionFactory connectionFactory, int? commandTimeout_s, ILog log)
        {
            this.commandTimeout_s = commandTimeout_s;
            this.connectionFactory = connectionFactory;
            this.log = log;
        }

        internal void Execute(Sql sql)
        {
            //log.DebugFormat(sqlText);

            int r;

            try
            {
                using (var connection = connectionFactory.Create())
                {
                    r = connection.Execute(sql.Text, sql.Parameters,null,commandTimeout_s);
                }
            }
            catch
            {
                //throw new Exception(string.Format("Failed to execute sql command:\n {0}\n", sqlText), x);
                log.DebugFormat("\nFailed to execute sql command:\n {0}", sql.Text);
                throw;
            }

            log.DebugFormat("affectedRows={0}", r);
        }


        internal IEnumerable<T> Query<T>(Sql sql)
        {
            //log.DebugFormat(sqlText);

            try
            {
                using (var connection = connectionFactory.Create())
                {
                    return connection.Query<T>(sql.Text, sql.Parameters, null, true, commandTimeout_s);
                }
            }
            catch (Exception x)
            {
                //throw new Exception(string.Format("Failed to run sql query:\n {0}\n", sqlText), x);
                log.DebugFormat("\nFailed to run sql query:\n {0}", sql.Text);
                throw;
            }
            
        }
    }
}
