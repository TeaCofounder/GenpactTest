using FileGuard.Helpers;

namespace FileGuard
{
    partial class Form1
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
            FileOperations.Instance.Dispose();
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txLog = new System.Windows.Forms.RichTextBox();
            this.txTarget = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSource = new System.Windows.Forms.Button();
            this.btnTarget = new System.Windows.Forms.Button();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.fileBrowser = new System.Windows.Forms.OpenFileDialog();
            this.watcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(639, 636);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(112, 35);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(639, 563);
            this.btnStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(112, 35);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Pause";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txSource
            // 
            this.txSource.Location = new System.Drawing.Point(18, 38);
            this.txSource.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txSource.Name = "txSource";
            this.txSource.ReadOnly = true;
            this.txSource.Size = new System.Drawing.Size(694, 26);
            this.txSource.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Source Directory";
            // 
            // txLog
            // 
            this.txLog.Location = new System.Drawing.Point(18, 170);
            this.txLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txLog.Name = "txLog";
            this.txLog.ReadOnly = true;
            this.txLog.Size = new System.Drawing.Size(583, 501);
            this.txLog.TabIndex = 6;
            this.txLog.Text = "";
            // 
            // txTarget
            // 
            this.txTarget.Location = new System.Drawing.Point(18, 116);
            this.txTarget.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txTarget.Name = "txTarget";
            this.txTarget.ReadOnly = true;
            this.txTarget.Size = new System.Drawing.Size(694, 26);
            this.txTarget.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Master Workbook";
            // 
            // btnSource
            // 
            this.btnSource.Location = new System.Drawing.Point(719, 38);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(32, 26);
            this.btnSource.TabIndex = 9;
            this.btnSource.Text = "...";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // btnTarget
            // 
            this.btnTarget.Location = new System.Drawing.Point(719, 116);
            this.btnTarget.Name = "btnTarget";
            this.btnTarget.Size = new System.Drawing.Size(32, 26);
            this.btnTarget.TabIndex = 11;
            this.btnTarget.Text = "...";
            this.btnTarget.UseVisualStyleBackColor = true;
            this.btnTarget.Click += new System.EventHandler(this.btnTarget_Click);
            // 
            // fileBrowser
            // 
            this.fileBrowser.FileName = "master.xlsx";
            this.fileBrowser.Filter = "XLSX files (*.xlsx)|*.xlsx|XLS files (*.xls)|*.xls";
            // 
            // watcher
            // 
            this.watcher.EnableRaisingEvents = true;
            this.watcher.SynchronizingObject = this;
            this.watcher.Created += new System.IO.FileSystemEventHandler(this.watcher_Created);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 692);
            this.Controls.Add(this.btnTarget);
            this.Controls.Add(this.btnSource);
            this.Controls.Add(this.txTarget);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txSource);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox txLog;
        private System.Windows.Forms.TextBox txTarget;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.Button btnTarget;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.OpenFileDialog fileBrowser;
        private System.IO.FileSystemWatcher watcher;
    }
}

