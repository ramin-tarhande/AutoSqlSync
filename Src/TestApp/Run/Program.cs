using System;
using System.Windows.Forms;

namespace TestApp.Run
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Intro();

            new AppStarter().Start();
        }

        private static void Intro()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException); //required here!
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}
