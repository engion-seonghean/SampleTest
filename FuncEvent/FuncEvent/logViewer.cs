using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuncEvent
{
    public partial class logViewer : UserControl
    {
        public Log log;
        public logViewer()
        {
            InitializeComponent();
        }

        private void logViewer_Load(object sender, EventArgs e)
        {

        }
        public void ManageLog(LogEventArgs e)
        {
            switch (e.LogType)
            {
                case LogType.Error:
                    rTBLog.SelectionColor = Color.Red;
                    break;
                case LogType.Information:
                    rTBLog.SelectionColor = Color.Black;
                    break;
                case LogType.Result:
                    rTBLog.SelectionColor = Color.Blue;
                    break;
                default:
                    break;
            }
            string TimeLog = DateTime.Now.ToString("MM-dd hh:mm:ss.fff");
            rTBLog.AppendText(string.Format($"[{TimeLog}] {e.Message}")+ "\r\n");
            rTBLog.ScrollToCaret();
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTBLog.Clear();
        }

        private void showLogFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           System.Diagnostics.Process.Start("Notepad.exe", log.CurFilePath);
        }

        private void openLogFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string lootPath = Path.GetFullPath(Path.Combine(log.CurFilePath,@"..\..\"));
            //System.Diagnostics.Process.Start(lootPath);
            
            string lootPath =  Path.GetDirectoryName(log.CurFilePath);
            System.Diagnostics.Process.Start(lootPath);
        }
    }
}
