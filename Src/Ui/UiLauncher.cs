using System;
using System.Drawing;
using System.Windows.Forms;
using AutoSqlSync.Core.Facade;
using AutoSqlSync.Core.Schemas;
using log4net;

namespace AutoSqlSync.Ui
{
    public class UiLauncher:IDisposable
    {
        private CoreStarter coreStarter;

        public Form Start(SyncSchema schema, string appName, Color? backColor, UiSettings settings, ILog log)
        {
            MainForm mainForm = null;

            coreStarter = new CoreStarter(schema, appName, () => mainForm.Quit(), settings, log);
            CoreFacade core = null;        
            try
            {
                core = coreStarter.Start();
            }
            catch (Exception x)
            {
                MessageBox.Show(null, x.ToString(), "error");
                return null;
            }
                
            mainForm = new MainForm(core,appName,backColor , settings);

            return mainForm;
            
        }

        public void Dispose()
        {
            if (coreStarter!=null)
            {
                coreStarter.Dispose();
            }
        }
    }
}
