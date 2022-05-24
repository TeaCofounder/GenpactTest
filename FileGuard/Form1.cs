using FileGuard.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileGuard
{
    public partial class Form1 : Form
    {
        public void WriteTextSafe(string message)
        {
            if(txLog.InvokeRequired)
            {
                Action safeWrite = delegate { WriteTextSafe(message); };
                txLog.Invoke(safeWrite);
            }
            else
            {
                txLog.AppendText(message);
                txLog.ScrollToCaret();
            }
        }
        public Form1()
        {
            InitializeComponent();
            FileOperations.Instance.SetParentForm(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txSource.Text = ConfigurationManager.AppSettings["sourceDirectory"];
            txTarget.Text = ConfigurationManager.AppSettings["targetWorkbook"];
            ButtonsConfig(true);
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            if(folderBrowser.ShowDialog()== DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("sourceDirectory");
                config.AppSettings.Settings.Add("sourceDirectory", folderBrowser.SelectedPath);
                config.Save(ConfigurationSaveMode.Modified);
                txSource.Text = folderBrowser.SelectedPath;
            }
        }

        private void btnTarget_Click(object sender, EventArgs e)
        {
            if(fileBrowser.ShowDialog() == DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("targetWorkbook");
                config.AppSettings.Settings.Add("targetWorkbook", fileBrowser.FileName);
                config.Save(ConfigurationSaveMode.Modified);
                txTarget.Text = fileBrowser.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileOperations.Instance.IsFileLocked(txTarget.Text))
                {
                    txLog.AppendText($"The master Workbook is being used by another process ... Aborting \n");
                    return;
                }
                FileOperations.Instance.EnsureExcel(txTarget.Text);
                watcher.Path = txSource.Text;
                watcher.EnableRaisingEvents = true;
                ButtonsConfig(false);
                txLog.AppendText("the watcher is active\n");
            }
            catch (Exception ex)
            {               
                watcher.EnableRaisingEvents = false;
                ButtonsConfig(true);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                FileOperations.Instance.ReleaseExcel();
                watcher.EnableRaisingEvents = false;
                ButtonsConfig(true);
                txLog.AppendText("the watcher is inactive\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void watcher_Created(object sender, FileSystemEventArgs e)
        {
            txLog.AppendText($"New file detected: {e.Name} ... inspecting\n");
            if (FileOperations.Instance.FilterFiles(e.Name, e.FullPath))
            {
                txLog.AppendText($"{e.Name} it is a special file ... ignoring\n");
                return;
            }

            string extension = Path.GetExtension(e.FullPath);

            if (FileOperations.Instance.IsTargetFile(extension))
            {
                txLog.AppendText($"{e.Name} is a target file ... processing \n");
                FileOperations.Instance.ImportSheets(e);
                txLog.AppendText($"Moving {e.Name} to processed directory \n");
                FileOperations.Instance.MoveFiles(e, $"{txSource.Text}\\processed");
            }
            else
            {
                txLog.AppendText($"{e.Name} is not a target file ... discarding \n");
                FileOperations.Instance.MoveFiles(e, $"{txSource.Text}\\discarded");
            }
        }

        private void ButtonsConfig(bool pAction)
        {
            btnStart.Enabled = pAction;
            btnSource.Enabled = pAction;
            btnTarget.Enabled = pAction;
            btnStop.Enabled=!pAction;
        }
    }
}
