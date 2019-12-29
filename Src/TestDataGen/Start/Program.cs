using System;
using System.Windows.Forms;

namespace TestDataGen.Start
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form f;
            try
            {
                f = TdgLauncher.Start();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "starting TestDataGen failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            Application.Run(f);
        }
    }
}
