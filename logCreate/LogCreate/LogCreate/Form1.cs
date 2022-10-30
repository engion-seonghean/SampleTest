using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogCreate
{
    public partial class Form1 : Form
    {
        Logview log = new Logview();
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Oncyclelist();
        }
     

 
        public void Oncyclelist()
        {
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Cim -> Control] ReqLoadReady", addTime = new TimeSpan(00, 00, 00, 00, 0000) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Control -> Cim] ResLoadReady", addTime = new TimeSpan(00, 00, 00, 00, 0040) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Load Start", addTime = new TimeSpan(00, 00, 00, 00, 1000) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Shuttle Scan Start위치 시작", addTime = new TimeSpan(00, 00, 00, 00, 0500) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Cam/LED관련축 정위치이동", addTime = new TimeSpan(00, 00, 00, 00, 1000) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Clamp Open상태확인", addTime = new TimeSpan(00, 00, 00, 00, 0500) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Glass 받기 위한 동작 완료", addTime = new TimeSpan(00, 00, 00, 00, 1000) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Load End", addTime = new TimeSpan(00, 00, 00, 00, 3500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Control -> Cim] ReqLoadReadyEnd", addTime = new TimeSpan(00, 00, 00, 00, 0040) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Cim -> Control] ResLoadReadyEnd", addTime = new TimeSpan(00, 00, 00, 00, 0040) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Cim -> Control] ReqScanReady", addTime = new TimeSpan(00, 00, 00, 00, 1000) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Control -> Cim] ResScanReady", addTime = new TimeSpan(00, 00, 00, 00, 1040) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "ScanReady Start", addTime = new TimeSpan(00, 00, 00, 0, 00500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "ScanReady 진행중", addTime = new TimeSpan(00, 00, 00, 0, 90000) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "ScanReady End", addTime = new TimeSpan(00, 00, 00, 0, 00500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Control -> Cim] ReqLoadReadyEnd", addTime = new TimeSpan(00, 00, 00, 00, 0040) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Cim -> Control] ResLoadReadyEnd", addTime = new TimeSpan(00, 00, 00, 00, 0040) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Scan Start", addTime = new TimeSpan(00, 00, 00, 0, 0500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Scan 진행중", addTime = new TimeSpan(00, 00, 00, 0, 9500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Scan End", addTime = new TimeSpan(00, 00, 00, 0, 0500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Cim -> Control] ReqUnloadStart", addTime = new TimeSpan(00, 00, 00, 00, 0000) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "[Control -> Cim] ResRepLoadReady", addTime = new TimeSpan(00, 00, 00, 00, 0040) });//(1초)
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Unload Start", addTime = new TimeSpan(00, 00, 00, 0, 0500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Unload 진행중", addTime = new TimeSpan(00, 00, 00, 0, 9500) });
            log.LogLists.Add(new ItemTimeSpan() { time = new TimeSpan(), Desc = "Unload End", addTime = new TimeSpan(00, 00, 00, 0, 0500) });
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> sLog = new List<string>();
            string path = System.IO.Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            try
            {
                if (ckWriteTEXT.Checked == false)
                {
                    DateTime inittime = DateTime.Now;
                    for (int i = 0; i < 300; i++)
                    {
                        sLog.Clear();

                        foreach (var item in log.LogLists)
                        {
                            inittime = inittime + item.addTime;
                            string tmp = string.Format($"{inittime.ToString("MM-dd HH:mm:ss.f")} , {item.Desc}");

                            listBox1.Items.Add(tmp);
                            sLog.Add(tmp);
                        }
                        listBox1.Items.Add($"NO.{i} Cycle End");
                        sLog.Add($"NO.{i} Cycle End");
                        log.FlushLogFile(path, sLog);
                    }
                }
                else
                {
                   
                    log.FlushLogFile(path);
                }
            }
            catch (Exception ex)
            {

                listBox1.Items.Add($" {ex.Message}");
            }
            

        }

   
    }
}
