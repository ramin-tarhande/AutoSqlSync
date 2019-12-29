using AutoSqlSync.Core.FieldsData;
using AutoSqlSync.Core.Schemas;

namespace AutoSqlSync.Core.Changes
{
    abstract class Change
    {
        public SourceTableDef SourceDef { get; private set; }
        public SourceFieldDataSet SourceData { get; private set; }
        public Version Version { get; private set; }
        protected Change(SourceTableDef sourceDef, SourceFieldDataSet sourceData, Version version)
        {
            SourceData = sourceData;
            SourceDef = sourceDef;
            //CtPkFieldsData = ctPkFieldsData;
            Version = version;
        }

        public abstract string Text();

        protected string CreateText(string title)
        {
            return string.Format("{0}, {1}: {2}",title, SourceDef, SourceData.Text());
        }

    }

    class Upsert:Change
    {
        public Upsert(SourceTableDef sourceDef, SourceFieldDataSet sourceData,
            Version version)
            : base(sourceDef, sourceData, version)
        {
        }

        public override string Text()
        {
            return CreateText("Upsert");
        }
    }

    class Delete : Change
    {
        public Delete(SourceTableDef sourceDef, SourceFieldDataSet sourceData, Version version)
            : base(sourceDef, sourceData, version)
        {
        }

        public override string Text()
        {
            return CreateText("Delete");
        }
    }

}
