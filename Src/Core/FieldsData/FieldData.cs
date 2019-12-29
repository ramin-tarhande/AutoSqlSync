using System.Collections.Generic;
using System.Linq;

namespace AutoSqlSync.Core.FieldsData
{
    struct FieldData
    {
        public string Name { get; private set; }
        public object Value { get; private set; }
        public FieldData(string name, object value) : this()
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Text();
        }

        public string Text()
        {
            return string.Format("{0}={1}", Name, Value);
        }
    }

    static class FieldDataExtension
    {
        public static SourceFieldDataSet ToSourceFieldDataSet(this IEnumerable<FieldData> fieldsData)
        {
            return new SourceFieldDataSet(
                fieldsData.ToDictionary());
        }

        public static DestiFieldDataSet ToDestiFieldDataSet(this IEnumerable<FieldData> fieldsData)
        {
            return new DestiFieldDataSet(
                fieldsData.ToDictionary());
        }

        public static IDictionary<string, FieldData> ToDictionary(this IEnumerable<FieldData> fieldsData)
        {
            return fieldsData.ToDictionary(x => x.Name, x => x);
        }

    }
}
