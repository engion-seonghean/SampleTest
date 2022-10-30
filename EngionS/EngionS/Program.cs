using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngionS
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;

            Mutex mutex = new Mutex(true, "SimpleSerialTest", out createNew);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Vars var = new Vars();
            var.Init();

            Application.Run(new Form1());
        }
    }
}
