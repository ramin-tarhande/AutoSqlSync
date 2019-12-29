using System;
using System.Data.SqlClient;
using log4net;

namespace AutoSqlSync.Core.DaBasics
{
    class DbConnectionFactory
    {
        private readonly ILog log;
        private readonly string connectionString;
        
        public DbConnectionFactory(string connectionString, ILog log)
        {
            
            this.log = log;
            this.connectionString = connectionString;
        }

        public SqlConnection Create()
        {
            try
            {
                var c = new SqlConnection(connectionString);
                c.Open();
                return c;
            }
            catch (Exception)
            {
                log.Debug("failed to open the db connection");
                return null;
            }
        }
    }
}
