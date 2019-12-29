using System;
using System.Windows.Forms;
using AutoSqlSync.Ui;
using AutoSqlSync.Ui.Tools;
using log4net;
using TestApp.TestSchema;
using Utilities;

namespace TestApp.Run
{
    class AppStarter
    {
        private ILog log;
        public void Start()
        {
            try
            {
                StartCore();
            }
            catch (Exception x)
            {
                if (log != null)
                {
                    log.Fatal(null, x);
                }

                MbUtil.ShowQuitMessage(x.Message);
            }
        }

        private void StartCore()
        {
            var schema = TestSchemaFactory.Create();

            var settings = SettingsLoader<UiSettings>.Load(ConfigFinder.MainConfig());

            log = LogStarter.Start(ConfigFinder.LogConfig(), "Main");

            ErrorHandler.Start(log);

            using (var uiLauncher = new UiLauncher())
            {
                var mainForm = uiLauncher.Start(schema, "TestApp", null, settings, log);
                if (mainForm != null)
                {
                    Application.Run(mainForm);
                }
            }
        }
    }
}
