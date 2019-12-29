using System;
using CtSync;
using log4net;

namespace SohCollector.RunUi
{
    class WfAppQuitter : AppQuitter
    {
        private readonly Action quitCallback;
        private readonly ILog log;
        public WfAppQuitter(ILog log, Action quitCallback)
        {
            this.log = log;
            this.quitCallback = quitCallback;
        }

        public void Quit()
        {
            log.Debug("Quitting");
            quitCallback();
            //Process.GetCurrentProcess().Kill();
            //Application.Exit();
        }
    }
}
