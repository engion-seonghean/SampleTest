using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engion;
namespace EngionS
{
    public class Vars
    {
        public static LogEx log;
        public static string RootFolder = string.Empty;


        public Vars()
        {
            RootFolder = TryGetSolutionDirectoryInfo().FullName;
        }
        public void Init()
        {
            try
            {
                log = new LogEx(Path.Combine(RootFolder, "Log"));

            }
            catch (System.Exception ex)
            {
                string s = "[Init Object]" + ex.Message;
                MessageBox.Show(s);
            }
        }
        public DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {// sln 파일 경로 가져오기
            var directory = new DirectoryInfo(currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }
    }
}
