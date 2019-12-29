using System.Windows.Forms;
using TestDataGen.Data;
using TestDataGen.Ui;
using Utilities;

namespace TestDataGen.Start
{
    class TdgLauncher
    {
        public static Form Start()
        {
            var settings = SettingsLoader<TdgSettings>.Load("TestDataGen.config");

            var progress = new MyProgress();

            var cf=new MyConnectionFactory(settings.ConnectionString);

            var da=new GenDa(cf);
            
            var maxId = da.GetMaxId();

            var g=new Generator(da,maxId,progress);

            return new Form1(g, progress);
        }
    }
}
