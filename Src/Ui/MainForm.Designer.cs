namespace AutoSqlSync.Ui
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.readFd = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bufferFd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.writesFd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.failuresFd = new System.Windows.Forms.TextBox();
            this.readStateFd = new System.Windows.Forms.Label();
            this.writeStateFd = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.invalidsFd = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // readFd
            // 
            this.readFd.Location = new System.Drawing.Point(58, 21);
            this.readFd.Name = "readFd";
            this.readFd.ReadOnly = true;
            this.readFd.Size = new System.Drawing.Size(55, 20);
            this.readFd.TabIndex = 3;
            this.readFd.TabStop = false;
            this.readFd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "Read:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "Buffer:";
            // 
            // bufferFd
            // 
            this.bufferFd.Location = new System.Drawing.Point(58, 55);
            this.bufferFd.Name = "bufferFd";
            this.bufferFd.ReadOnly = true;
            this.bufferFd.Size = new System.Drawing.Size(55, 20);
            this.bufferFd.TabIndex = 5;
            this.bufferFd.TabStop = false;
            this.bufferFd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 8;
            this.label3.Text = "Write:";
            // 
            // writesFd
            // 
            this.writesFd.Location = new System.Drawing.Point(58, 89);
            this.writesFd.Name = "writesFd";
            this.writesFd.ReadOnly = true;
            this.writesFd.Size = new System.Drawing.Size(55, 20);
            this.writesFd.TabIndex = 7;
            this.writesFd.TabStop = false;
            this.writesFd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(184, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Failures:";
            // 
            // failuresFd
            // 
            this.failuresFd.Location = new System.Drawing.Point(235, 21);
            this.failuresFd.Name = "failuresFd";
            this.failuresFd.ReadOnly = true;
            this.failuresFd.Size = new System.Drawing.Size(35, 20);
            this.failuresFd.TabIndex = 9;
            this.failuresFd.TabStop = false;
            this.failuresFd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // readStateFd
            // 
            this.readStateFd.AutoSize = true;
            this.readStateFd.ForeColor = System.Drawing.Color.White;
            this.readStateFd.Location = new System.Drawing.Point(118, 24);
            this.readStateFd.Name = "readStateFd";
            this.readStateFd.Size = new System.Drawing.Size(56, 14);
            this.readStateFd.TabIndex = 11;
            this.readStateFd.Text = "read state";
            // 
            // writeStateFd
            // 
            this.writeStateFd.AutoSize = true;
            this.writeStateFd.ForeColor = System.Drawing.Color.White;
            this.writeStateFd.Location = new System.Drawing.Point(118, 92);
            this.writeStateFd.Name = "writeStateFd";
            this.writeStateFd.Size = new System.Drawing.Size(59, 14);
            this.writeStateFd.TabIndex = 12;
            this.writeStateFd.Text = "write state";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(184, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "Invalids:";
            // 
            // invalidsFd
            // 
            this.invalidsFd.Location = new System.Drawing.Point(236, 89);
            this.invalidsFd.Name = "invalidsFd";
            this.invalidsFd.ReadOnly = true;
            this.invalidsFd.Size = new System.Drawing.Size(35, 20);
            this.invalidsFd.TabIndex = 13;
            this.invalidsFd.TabStop = false;
            this.invalidsFd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.ClientSize = new System.Drawing.Size(291, 134);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.invalidsFd);
            this.Controls.Add(this.writeStateFd);
            this.Controls.Add(this.readStateFd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.failuresFd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.writesFd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bufferFd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readFd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CtSync Ui";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox readFd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bufferFd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox writesFd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox failuresFd;
        private System.Windows.Forms.Label readStateFd;
        private System.Windows.Forms.Label writeStateFd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox invalidsFd;
    }
}

