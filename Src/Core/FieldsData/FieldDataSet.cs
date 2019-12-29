using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoSqlSync.Core.FieldsData
{
    abstract class FieldDataSet : IEnumerable<FieldData>
    {
        private readonly IDictionary<string, FieldData> dic;

        protected FieldDataSet(IDictionary<string, FieldData> dic)
        {
            this.dic = dic;
        }

        protected IDictionary<string, FieldData> GetDictionary()
        {
            return dic;
        }

        public string Text()
        {
            return string.Join(", ",
                dic.Values.Select(fd => fd.Text()));
        }

        public IEnumerable<string> FieldNames()
        {
            return dic.Keys;
        }

        public IEnumerator<FieldData> GetEnumerator()
        {
            return dic.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Contains(string fieldName)
        {
            return dic.ContainsKey(fieldName);
        }

    }
}
