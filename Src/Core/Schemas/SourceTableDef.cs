using System.Collections.Generic;
using System.Linq;

namespace AutoSqlSync.Core.Schemas
{
    public class SourceTableDef
    {
        public string Name { get; private set; }
        public IEnumerable<string> PkFields { get; private set; }
        public IEnumerable<string> OrdinaryFields { get; private set; }
        public IEnumerable<string> AllFields { get; private set; }
        private readonly List<DestinationTableDef> destinationDefs;

        public SourceTableDef(string name, IEnumerable<string> pkFields,
            IEnumerable<string> ordinaryFields)
        {
            Name = name;
            PkFields = pkFields;
            OrdinaryFields = ordinaryFields;
            AllFields = pkFields.Union(OrdinaryFields).ToList();
            destinationDefs = new List<DestinationTableDef>();
        }

        public IEnumerable<DestinationTableDef> DestinationDefs
        {
            get { return destinationDefs; }
        }

        public void AddDestination(string destiTableName, IEnumerable<DestiFieldDef> destiFields,
            IEnumerable<string> destiLookupFieldNames=null)
        {
            var sourcePkBasedFields = destiFields.FindBasedOn(PkFields);

            IEnumerable<DestiFieldDef> destiLookupFields;
            if (destiLookupFieldNames == null)
            {
                destiLookupFields = sourcePkBasedFields;
            }
            else
            {
                destiLookupFields = destiFields.FindByName(destiLookupFieldNames);    
            }
            
            destinationDefs.Add(
                new DestinationTableDef(destiTableName, destiFields, sourcePkBasedFields,destiLookupFields));
        }

        
        public override string ToString()
        {
            return Name;
        }
    }
}
