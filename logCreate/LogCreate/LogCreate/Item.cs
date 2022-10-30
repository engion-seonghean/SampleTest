using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogCreate
{
    public class Item
    {
        public Item()
        {

        }

        public DateTime time { get; set; }
        public string Desc { get; set; }
        public DateTime addTime { get; set; }
    }

    public class ItemTimeSpan
    {
        public ItemTimeSpan()
        {

        }

        public TimeSpan time { get; set; }
        public string Desc { get; set; }
        public TimeSpan addTime { get; set; }


    }
    public class Logview
    {
        private static object lockobj = new object();
        public Logview()
        {

        }
        public List<ItemTimeSpan> LogLists = new List<ItemTimeSpan>();
        public void FlushLogFile(string logDir)
        {

            DateTime dt = DateTime.Now;

            string fileName = Path.Combine(logDir, dt.ToString("yyyy-MM-dd") + ".txt");
            lock (lockobj)
            {
                using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.Default))
                {
                    foreach (ItemTimeSpan item in LogLists)
                    {
                        // sw.WriteLine(string.Format("{0}: {1}", item.time.ToString("MM-dd HH:mm:ss.FFF"), item.Desc.ToString()));

                        string stemp = item.time.ToString("hhmmss.f");

                        sw.WriteLine($"{stemp}");
                        //sw.WriteLine(string.Format("aaaa"));

                    }
                    // items.Clear();
                }
            }
        }
        public void FlushLogFile(string logDir, List<string> tlog)
        {

            DateTime dt = DateTime.Now;

            string fileName = Path.Combine(logDir, "Log_"+dt.ToString("yyyy-MM-dd") + ".txt");
            lock (lockobj)
            {
                using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.Default))
                {
                    foreach (var item in tlog)
                    {
                        // sw.WriteLine(string.Format("{0}: {1}", item.time.ToString("MM-dd HH:mm:ss.FFF"), item.Desc.ToString()));
                      
                        sw.WriteLine($"{item}");
                        //sw.WriteLine(string.Format("aaaa"));

                    }
                    // items.Clear();
                }
            }
        }
    }

    public class WriteLogTxt
    {


    }
}
