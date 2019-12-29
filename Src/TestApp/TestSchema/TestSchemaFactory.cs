using AutoSqlSync.Core.Conversion;
using AutoSqlSync.Core.Schemas;

namespace TestApp.TestSchema
{
    public static class TestSchemaFactory
    {
        public static SyncSchema Create()
        {
            return new SyncSchema(new[]
            {
                CreateForASource(),
            });
        }

        static SourceTableDef CreateForASource()
        {
            var sourceTable = new SourceTableDef(
                name:"ASource", 
                pkFields:new[] { "Id" },
                ordinaryFields: new[] { "Title", "Description"});

            sourceTable.AddDestination(
                "ADestination",
                new DestiFieldDef[]
                {
                    new Copy("Id", "Id"),
                    new Copy("Title", "Title"),
                    new Copy("Descrip", "Description"),
                    
                    new Custom("Together", f =>
                    {
                        return f["Title"].AsString() + " -- " + f["Description"].AsString();
                    }),
                    
                    new Constant("Extra", ":-)"), 
                });

            return sourceTable;
        }
    }
}
