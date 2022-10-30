using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PmacIO
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
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

            Application.Run(new FormMain());
        }
    }
}
