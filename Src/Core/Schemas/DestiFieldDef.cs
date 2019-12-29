using System;
using AutoSqlSync.Core.FieldsData;

namespace AutoSqlSync.Core.Schemas
{
    public abstract class DestiFieldDef
    {
        public string DestinationFieldName { get; private set; }

        protected DestiFieldDef(string destinationFieldName)
        {
            DestinationFieldName = destinationFieldName;
        }

        internal FieldData Compute(SourceFieldDataSet sourceData)
        {
            try
            {
                return ComputeCore(sourceData);
            }
            catch (FieldNotFoundException x)
            {
                throw new FatalException(
                    string.Format("destinatin field {0} depends on {1} and it's not found in source data",
                    DestinationFieldName,x.FieldName),x);
            }
        }

        public abstract string GetSourceFieldName();

        public bool IsBasedOn(string sourceField)
        {
            return GetSourceFieldName() == sourceField;
        }

        internal abstract FieldData ComputeCore(SourceFieldDataSet sourceData);
    }

    public class Copy : DestiFieldDef
    {
        public string SourceFieldName { get; private set; }
        
        public Copy(string destinationFieldName, string sourceFieldName)
            : base(destinationFieldName)
        {
            SourceFieldName = sourceFieldName;
        }


        public override string GetSourceFieldName()
        {
            return SourceFieldName;
        }

        internal override FieldData ComputeCore(SourceFieldDataSet sourceData)
        {
            return new FieldData(DestinationFieldName, sourceData[SourceFieldName]);
            
        }
    }

    public class Custom : DestiFieldDef
    {
        public Func<SourceFieldValues, object> Func { get; private set; }
        public string SourceFieldName { get; private set; }
        public Custom(string destinationFieldName, Func<SourceFieldValues, object> func, string sourceFieldName=null)
            :base(destinationFieldName)
        {
            SourceFieldName = sourceFieldName;
            Func = func;
        }

        internal override FieldData ComputeCore(SourceFieldDataSet sourceData)
        {
            return new FieldData(DestinationFieldName, Func(sourceData));
        }

        public override string GetSourceFieldName()
        {
            return SourceFieldName;
        }
    }

    public class Constant : DestiFieldDef
    {
        public object Value { get; private set; }
        public Constant(string destinationFieldName, object value) 
            :base(destinationFieldName)
        {
            Value = value;
        }

        internal override FieldData ComputeCore(SourceFieldDataSet sourceData)
        {
            return new FieldData(DestinationFieldName, Value);
        }

        public override string GetSourceFieldName()
        {
            return null;
        }
    }
    
}
