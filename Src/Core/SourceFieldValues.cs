namespace AutoSqlSync.Core
{
    public interface SourceFieldValues
    {
        object this[string fieldName] { get; }
    }
}