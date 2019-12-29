using System;

namespace AutoSqlSync.Core.FieldsData
{
    class FieldNotFoundException : Exception
    {
        public string FieldName { get; private set; }
        public FieldNotFoundException(string fieldName, Exception innerException) 
            : base("Field not found",innerException)
        {
            FieldName = fieldName;
        }
    }
}
