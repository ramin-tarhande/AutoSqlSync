using System.Collections.Generic;
using AutoSqlSync.Core.FieldsData;

namespace AutoSqlSync.Core.Schemas
{
    public class DestinationTableDef
    {
        public string Name { get; private set; }
        public IEnumerable<DestiFieldDef> AllFields { get; private set; }
        internal IEnumerable<DestiFieldDef> SourcePkBasedFields { get; private set; }
        internal IEnumerable<DestiFieldDef> DestiLookupFields { get; private set; }
        public DestinationTableDef(string name, IEnumerable<DestiFieldDef> allFields,
              IEnumerable<DestiFieldDef> sourcePkBasedFields, IEnumerable<DestiFieldDef> destiLookupFields)
        {
            Name = name;
            AllFields = allFields;

            SourcePkBasedFields = sourcePkBasedFields;
            DestiLookupFields = destiLookupFields;
        }

        internal DestiFieldDataSet ComputeAll(SourceFieldDataSet sourceData)
        {
            return AllFields.Compute(sourceData);
        }

        internal DestiFieldDataSet ComputeForSourcePkBasedFields(SourceFieldDataSet sourceData)
        {
            return SourcePkBasedFields.Compute(sourceData);
        }

        internal DestiFieldDataSet ComputeForDestiLookupFields(SourceFieldDataSet sourceData)
        {
            return DestiLookupFields.Compute(sourceData);    
            /*
            if (DestiPkFields==null)
            {
                return null;
            }
            else
            {
                return DestiPkFields.Compute(sourceData);    
            }*/
        }
    }
}
