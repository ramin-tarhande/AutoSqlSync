using System.Windows.Forms;

namespace AutoSqlSync.Ui.Tools
{
    public static class MbUtil
    {
        public static void ShowQuitMessage(string text)
        {
            MessageBox.Show(text, "Fatal error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
