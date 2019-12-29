using System;

namespace AutoSqlSync.Core
{
    class FatalException : Exception
    {
        public FatalException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
