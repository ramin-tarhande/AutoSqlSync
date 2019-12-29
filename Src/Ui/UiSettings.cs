using AutoSqlSync.Core;

namespace AutoSqlSync.Ui
{
    public interface UiSettings : CoreSettings
    {
        bool ConfirmExit { get; set; }   
    }
}