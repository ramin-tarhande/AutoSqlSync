using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace TestDataGen.Data
{
    class MyConnectionFactory
    {
        private readonly string connectionString;
        public MyConnectionFactory(string connectionString)
        {
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
                Trace.Write("failed to open the db connection");
                throw new Exception("failed to open the db connection");
            }
        }
    }
}
