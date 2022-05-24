using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FileGuard.Helpers
{
    public sealed class FileOperations : IDisposable
    {
        private List<string> extensions = new List<string>() { ".xls", ".xlsx" };
        private Workbook masterWorkbook { get; set; }
        private Application xlsApplication { get; set; }
        private Form1 Parent { get; set; }

        private readonly static FileOperations _instance = new FileOperations();

        private FileOperations() { }
        public static FileOperations Instance { get { return _instance; } }

        public void SetParentForm(Form1 parent)
        {
            Parent= parent;
        }
        public bool FilterFiles(string filename, string fullpath)
        {
            FileInfo fi = new FileInfo(fullpath);
            if (!((fi.Attributes | FileAttributes.Hidden) == fi.Attributes))
            {
                return false;
            }
            return true;
        }

        public void ImportSheets(FileSystemEventArgs e)
        {
            try
            {
                Workbook tmp = xlsApplication.Workbooks.Open(e.FullPath);
                int index = 1;
                foreach (Worksheet sheet in tmp.Worksheets)
                {
                    sheet.Copy(After: masterWorkbook.Worksheets[1]);
                    Worksheet h = masterWorkbook.Worksheets[2];
                    string sheetName = $"h{index}_{DateTime.Now.ToString("MMddyyyy.hhmmss.ffffff")}";
                    h.Name = sheetName;
                    Parent.WriteTextSafe($"Importing Sheet: {sheet.Name} as {h.Name}\n");
                    index++;
                }
                tmp.Close();
                masterWorkbook.Save();
                Parent.WriteTextSafe($"All sheets from {e.Name} has been imported\n");
            }
            catch (Exception ex)
            {
                Parent.WriteTextSafe($"Error: {ex.Message}\n");
            }
        }

        public void MoveFiles(FileSystemEventArgs e, string path)
        {
            try
            {
                if (!System.IO.Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (IsFileLocked(e.FullPath))
                {
                    Parent.WriteTextSafe($"{e.Name} is being used by another process \n");
                    return;
                }
                if (File.Exists($"{path}\\{e.Name}"))
                {
                    Parent.WriteTextSafe($"{e.Name} already exists into destination directory. Imposible to move\n");
                    return;
                }
                File.Move(e.FullPath, $"{path}\\{e.Name}");
                Parent.WriteTextSafe($"{e.Name} has been moved \n");
            }
            catch (Exception ex)
            {
                Parent.WriteTextSafe($"Error: {ex.Message}\n");
            }
        }

        public bool IsFileLocked(string file)
        {
            try
            {
                FileInfo info = new FileInfo(file);
                using (FileStream stream = info.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
        public bool IsTargetFile(string extension)
        {
            return extensions.Contains(extension); 
        }
        public void EnsureExcel(string masterWorkbookPath)
        {
            if (xlsApplication == null) { xlsApplication = new Microsoft.Office.Interop.Excel.Application(); }
            if (masterWorkbook == null) { masterWorkbook = xlsApplication.Workbooks.Open(masterWorkbookPath); }
        }
        public void Dispose()
        {
            if (Instance != null)
            {
                if (masterWorkbook != null) { Marshal.ReleaseComObject(masterWorkbook); }
                if (xlsApplication != null) { xlsApplication.Quit(); }
            }
        }

        public void ReleaseExcel()
        {
            if (masterWorkbook != null) { masterWorkbook.Close(); Marshal.ReleaseComObject(masterWorkbook); }
            if (xlsApplication != null) { xlsApplication.Quit(); }
        }
    }
}
