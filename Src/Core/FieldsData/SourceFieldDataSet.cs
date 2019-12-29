using System;
using System.Collections.Generic;

namespace AutoSqlSync.Core.FieldsData
{
    class SourceFieldDataSet : FieldDataSet,SourceFieldValues
    {
        public SourceFieldDataSet(IDictionary<string, FieldData> dic)
            : base(dic)
        {
        }

        public object this[string fieldName]
        {
            get
            {
                try
                {
                    var dic = GetDictionary();
                    return dic[fieldName].Value;
                }
                catch (Exception x)
                {
                    throw new FieldNotFoundException(fieldName, x);
                }
            }
        }
    }
}
