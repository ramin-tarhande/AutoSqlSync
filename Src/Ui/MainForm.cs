using System;
using System.Drawing;
using System.Windows.Forms;
using AutoSqlSync.Core;
using AutoSqlSync.Core.Facade;

namespace AutoSqlSync.Ui
{
    partial class MainForm : Form
    {
        private readonly SyncProgress progress;
        private readonly CoreFacade core;
        private readonly UiSettings settings;
        private readonly Color? backColor;
        private readonly string appName;
        internal MainForm(CoreFacade core, string appName,Color? backColor , UiSettings settings)
        {
            this.backColor = backColor;
            this.appName = appName;
            this.settings = settings;
            this.progress = core.Progress;
            this.core = core;
            InitializeComponent();

            
            this.Load += MainForm_Load;
            this.Closed += MainForm_Closed;
            this.Closing += MainForm_Closing;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = appName + " [AutoSqlSync]";
            if (backColor.HasValue)
            {
                this.BackColor = backColor.Value;    
            }
            
            core.Start();
        }


        void MainForm_Closed(object sender, EventArgs e)
        {
            core.Stop();
        }

        void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (quitting || !settings.ConfirmExit)
            {
                e.Cancel = false;
                return;
            }

            var r = MessageBox.Show(this, string.Format("Stop {0}?",appName), appName, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r!= DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            readFd.Text = progress.Reads.ToString();
            writesFd.Text = progress.Writes.ToString();
            failuresFd.Text = progress.Failures.ToString();
            bufferFd.Text = progress.BufferSize.ToString();
            readStateFd.Text=progress.ReadState.ToString();
            writeStateFd.Text = progress.WriteState.ToString();
            invalidsFd.Text = progress.Invalids.ToString();
        }

        private bool quitting;
        public void Quit()
        {
            this.Invoke((MethodInvoker) delegate
            {
                quitting = true;
                MessageBox.Show(this, string.Format("Error in {0}; check log file",appName) , appName, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
                Close();    
            });            
        }
    }
}
