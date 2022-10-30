using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FuncEvent;
namespace PmacIO
{
    public class Vars
    {
     
        public static string RootFolder = string.Empty;
        public static Log log;
        public static ClassMotion motion= new ClassMotion();
        public static int Ecat_ServerPort = 12960;
        public static int Ecat_ClientPort = 12955;
        public static Ethernet_Server CommSevr;
        public static Ethernet_Client CommClie;
        public Vars()
        {
            RootFolder = TryGetSolutionDirectoryInfo().FullName;
        }
        public void Init()
        {
            try
            {
                // log = new LogM(Path.Combine(RootFolder, "Log"));
                log = new Log(Path.Combine(RootFolder, "Log"));

                log.AddLogMessage(LogType.Information,0,"Vars initialize OK");

                CommSevr = new Ethernet_Server();
                CommClie = new Ethernet_Client();
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
