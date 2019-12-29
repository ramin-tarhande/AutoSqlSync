using System;
using System.Collections.Generic;
using System.Linq;
using AutoSqlSync.Core.FieldsData;

namespace AutoSqlSync.Core.Schemas
{
    static class DestiFieldDefExtensions
    {
        public static IEnumerable<DestiFieldDef> FindByName(this IEnumerable<DestiFieldDef> destiFields, 
            IEnumerable<string> fieldNames)
        {
            if (fieldNames == null)
            {
                return null;
            }
            else
            {
                return destiFields.Where(df => fieldNames.Contains(df.DestinationFieldName))
                    .ToList();
            }
        }
        
        public static IEnumerable<DestiFieldDef> FindBasedOn(this IEnumerable<DestiFieldDef> destiFields,
            IEnumerable<string> sourceFieldNames)
        {
            return sourceFieldNames.Select(sfn => destiFields.FindBasedOn(sfn)).ToList();
        }

        public static DestiFieldDef FindBasedOn(this IEnumerable<DestiFieldDef> destiFields,
            string sourceFieldName)
        {
            try
            {
                return destiFields.First(df => df.IsBasedOn(sourceFieldName));
            }
            catch
            {
                throw new Exception(
                    string.Format("Destination field based on source field: {0} not defined", sourceFieldName));
            }
        }

        public static DestiFieldDataSet Compute(this IEnumerable<DestiFieldDef> destiFields, SourceFieldDataSet sourceData)
        {
            return destiFields.Select(df => df.Compute(sourceData)).ToDestiFieldDataSet();
        }
    }
}
