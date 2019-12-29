using System;
using System.Collections.Generic;
using System.Text;
using AutoSqlSync.Core.FieldsData;
using Dapper;

namespace AutoSqlSync.Core.Write
{
    static class FieldDataHelpers
    {
        internal static string CreateWhere(this FieldDataSet fieldDataSet)
        {
            var where = new StringBuilder();
            where.Append("WHERE ");

            var whereEquals = fieldDataSet.CreateSqlText((fn, pv) => string.Format("{0}={1}", fn, pv), " and ");

            where.Append(whereEquals);

            return where.ToString();
        }
        
        internal static string CreateSqlText(this FieldDataSet fieldDataSet,
            Func<string, string, string> map, string separator)
        {
            var equalsText = new List<string>();

            foreach (var fieldData in fieldDataSet)
            {
                var paramVariable = fieldData.ParameterVariable();

                var itemText = map(fieldData.Name, paramVariable);

                equalsText.Add(itemText);
            }

            var text = string.Join(separator, equalsText);

            return text;
        }

        internal static object CreateParams(this FieldDataSet fieldDataSet)
        {
            var parameters = new DynamicParameters();
            foreach (var fd in fieldDataSet)
            {
                parameters.Add(fd.ParameterVariable(), fd.Value);
            }

            return parameters;
        }

        internal static string ParameterVariable(this FieldData fieldData)
        {
            return string.Format("@{0}", fieldData.Name);
        }
    }
}
