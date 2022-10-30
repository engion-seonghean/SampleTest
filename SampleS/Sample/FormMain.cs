using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Management;
using FuncEvent;
using System.Drawing.Drawing2D;
using System.IO;
using static FuncEvent.ClassJson;
using System.Xml.Linq;
using System.Xml;
using Microsoft.XmlDiffPatch;
using System.Reflection;
using System.Linq.Expressions;

namespace PmacIO
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

        }
        private ClassMotion mot
        {
            get { return Vars.motion; }
        }
        Class_Clipper pmc;
        BackgroundWorker worker;
        // 인스턴스 생성
        ClassTest TClass = new ClassTest();
        ClassTest1 TClass1 = new ClassTest1();
        Log log => Vars.log;
        private void comboBoxLoadFormTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.tabNo = comboBoxLoadFormTab.SelectedIndex;
            Properties.Settings.Default.Save();
        }
        private void InitLog()
        {
            Vars.log.OnLogEvent += log_OnLogEvent;
            logViewer1.log = Vars.log;
            Vars.log.AddLogMessage(LogType.Information, 0, "Program Start");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitLog();

            for (int i = 0; i < 10; i++) comboBoxLoadFormTab.Items.Add(i.ToString());
            var tmp = Properties.Settings.Default.tabNo;
            //tabControl1.SelectedIndex = 1;
            tCMain.SelectedIndex = comboBoxLoadFormTab.SelectedIndex = Properties.Settings.Default.tabNo;

            timer1.Enabled = true;

            WorkStart();

            string[] token = new string[100];
            for (int i = 0; i < token.Length; i++)
            {
                token[i] = i.ToString();
            }
            int AXISINFO_M4000 = 0; //1~10축 Y:$C0,24 -> 0:In-Pos, 3:AmpFault, 10: HomeComplete

            for (int i = 0; i < 10; i++)
            {
                var mm1 = uint.Parse(token[(AXISINFO_M4000 + 0) + (i * 10)]);
                var mm2 = uint.Parse(token[(AXISINFO_M4000 + 1) + (i * 10)]);
                var mm3 = uint.Parse(token[(AXISINFO_M4000 + 2) + (i * 10)]);
            }
            initMotion();
        }
        void AddLogInfo(string msg)
        {
            Vars.log.AddLogMessage(LogType.Information, 0, msg);

        }
        void AddLogErr(string msg)
        {
            Vars.log.AddLogMessage(LogType.Error, 0, msg);
        }
        public void WorkStart()
        {
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
        }
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    worker.ReportProgress(0);

                    Thread.Sleep(500);

                }
            }
            catch (Exception ex)
            {
                AddLogErr(ex.Message);
            }
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                //Task.Run(() => { MemoryCheck(); });
                //6MemoryCheck();
                //MemoryCheck2();
                //MemoryCheck3();
            }
            catch (Exception ex)
            {
                AddLogErr(ex.Message);
            }
        }
        ClassPcStatus pcStatus = new ClassPcStatus();
        private void MemoryCheck3()
        {
            float cpu = pcStatus.GetCPURate();
            //AddLogInfo(string.Format("CPU 사용률 : {0}", cpu));
            float memory = pcStatus.GetMemoryRate();
            //AddLogInfo(string.Format("메모리 사용률 : {0}", memory));
            float disk = pcStatus.GetMemoryRate();
            //AddLogInfo(string.Format("디스크 I/O 사용률 : {0}",disk));
            lblTotal.Text = string.Format($"CPU 사용률: { cpu}");
            lblFree.Text = string.Format($"메모리사용률: {memory}");
            lblUse.Text = string.Format($"디스크 I/O 사용률: {disk}");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkPulseOn();
            var tmp = scanReadyDone.SafeWaitHandle.Equals(true);
            buttonResetEventWaitReset.BackColor = (scanReadyDone.Equals(true)) ? Color.Red : Color.Transparent;
            BeginInvoke((Action)delegate
            {
                //  MemoryCheck();
            });
        }

        bool CheckTimeout(Func<bool> func, Func<bool> cancel, int timeout, string msg)
        {
            var watch = new Stopwatch();
            //log.AddLogMessage(LogType.Information, 0, $"대기중: {msg}");
            watch.Start();
            while (true)
            {
                Application.DoEvents();
                if (watch.ElapsedMilliseconds > timeout)
                {
                    // log.AddLogMessage(LogType.Error, 0, $"Timeout Error: {msg}");
                    return true;
                }
                if (cancel())
                {
                    // log.AddLogMessage(LogType.Error, 0, $"User Cancel: {msg}");
                    return true;
                }
                if (func())
                {
                    break;
                }
                Thread.Sleep(100);
            }
            // log.AddLogMessage(LogType.Information, 0, $"확인완료: {msg}");
            return false;
        }
        //public static AutoResetEvent scanReadyDone = new AutoResetEvent(false);


        void log_OnLogEvent(object sender, LogEventArgs e)
        {
            //if (e.LogType == LogType.Error)
            //{
            //    BeginInvoke((Action)delegate
            //    {
            //        //tabControlBaseStatus.SelectedIndex = 0 //0= 기본 상태 표시 전환 
            //        // tabControlBaseStatus.SelectedIndex = 1;//1= LogView 전환
            //    });
            //}
            //logViewer1.ManageLog(e);

            BeginInvoke((Action)delegate
            {
                if (e.LogType == LogType.Error)
                {
                    //tabControlBaseStatus.SelectedIndex = 0 //0= 기본 상태 표시 전환 
                    // tabControlBaseStatus.SelectedIndex = 1;//1= LogView 전환
                }
                logViewer1.ManageLog(e);

            });
        }

        void Func_CheckTimeout()
        {
            Invoke((Action)delegate { AddLogInfo("작업시작"); });

            bool timeout = CheckTimeout(() => scanReadyDone.WaitOne(100), () => false, 60000, "[Exchange] Scan Ready Done");
            if (timeout) { return; }
            Invoke((Action)delegate { AddLogInfo("작업종료"); });
        }


        //static void Run()
        //{
        //    var obj = new LargeDataClass();
        //    obj.Set(1, 10);
        //}

        Task[] clampSingleAction = new Task[16];
        Stopwatch[] sw = new Stopwatch[16];
        string ActionTime(string jobStr, int jobNo, ref bool sig1, ref bool sig2)
        {
            int CheckTime = 5000;
            sw[jobNo] = new Stopwatch();
            sw[jobNo].Restart();

            while (true)
            {
                if (sw[jobNo].ElapsedMilliseconds > CheckTime
                && (sig1 == false || sig2 == false))
                    return string.Format($"{jobStr} failed : {sw[jobNo].ElapsedMilliseconds.ToString()}msec");
                else if (sig1 == true && sig2 == true)
                    return string.Format($"{jobStr} success : {sw[jobNo].ElapsedMilliseconds.ToString()}msec");
                Thread.Sleep(100);
            }
        }
        private void buttonTaskCheck_Click(object sender, EventArgs e)
        {
            int jobNo = Convert.ToInt32((sender as Button).Tag);
            string jobStr = (sender as Button).Text;

            try
            {
                switch (jobNo)
                {
                    case 1:  // LEFT Z UP
                        clampSingleAction[jobNo] = Task.Run((Action)delegate
                         {
                             bool sig1 = false;
                             bool sig2 = false;
                             MessageBox.Show(ActionTime(jobStr, jobNo, ref sig1, ref sig2));
                         }
                       );
                        break;
                    case 2:  // LEFT Z UP
                        clampSingleAction[jobNo] = Task.Run((Action)delegate
                        {
                            bool sig1 = false;
                            bool sig2 = false;
                            MessageBox.Show(ActionTime(jobStr, jobNo, ref sig1, ref sig2));

                        });
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }

        }
        public void Setinstance()
        {
            var mm = TClass.Output[0];
            TClass.Output[0] = 10;
            AddLogInfo($"Press any key...{ TClass.Output[0] }");

            TClass.input[0] = 1;
            AddLogInfo($"Press any key...{TClass.input[0]}");

            GetValuesTest GVT = new GetValuesTest();
            GVT.Main();

        }

        private void buttonProperty_Click(object sender, EventArgs e)
        {
            Setinstance();
        }

        public static AutoResetEvent scanReadyDone = new AutoResetEvent(false);

        private void buttonResetEventWaitOne_Click(object sender, EventArgs e)
        {
            var ctl = sender as Button;
            switch (ctl.Name)
            {
                case "buttonResetEventWaitOne":
                    scanReadyDone.WaitOne(100);
                    break;
                case "buttonResetEventWaitOne2":

                    bool timeout = true;
                    while (true)
                    {
                        for (int i = 0; i < 6000; i++)
                        {
                            if (scanReadyDone.WaitOne(100))
                            { timeout = false; break; }
                        }
                        if (timeout == true)
                            AddLogInfo($"ResetEvent wait");
                        else
                            break;
                    }
                    break;
                case "buttonResetEventSet":
                    scanReadyDone.Set();
                    break;
                case "buttonResetEventReset":
                    scanReadyDone.Reset();
                    Task.Run(() => { Func_CheckTimeout(); });
                    break;
            }
        }


        private void buttonCoodinate_Click(object sender, EventArgs e)
        {
            double Ang60 = 60;
            double Ang30 = 30;
            double Ang60Pos = -78000;
            double Ang30Pos = 213000;

            double CurAng = ((Ang60 - Ang30) / (Ang60Pos - Ang30Pos)) * (-70000 - Ang30Pos) + Ang30;
            string tmp = CurAng.ToString("#.#");
            AddLogInfo($"변환 축 좌표 {CurAng}");
        }

        private void buttonstruct_Click(object sender, EventArgs e)
        {

        }

        Image img = null;
        private void buttonZoom_Click(object sender, EventArgs e)
        {
            img = Image.FromFile(@"C:\TestPadData_32.bmp");
            pB.SizeMode = PictureBoxSizeMode.Zoom;
            if (img != null)
            {

                Graphics g = Graphics.FromImage(img);
                int Wz = Convert.ToInt32(img.Width * (100 / 100));
                int Hz = Convert.ToInt32(img.Height * (100 / 100));

                int newwidth = Convert.ToInt32(Wz);
                int newheight = Convert.ToInt32(Hz);
                //Rectangle r = new Rectangle(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, newwidth, newheight);
                Rectangle r = new Rectangle(0, 0, img.Width, img.Height);
                pB.Invalidate();
                g.DrawImage(img, r);
                pB.Image = img;
            }
        }

        private void pB_Paint(object sender, PaintEventArgs e)
        {
            //    if (pB.Image != null)
            //    {
            //        e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //        e.Graphics.DrawImage(pB.Image, new PointF(0,0));
            //        pB.Focus();
            //    }
        }

        private void buttonShowForm_Click(object sender, EventArgs e)
        {
            Bitmap _bmp = new Bitmap(@"C:\TestPadData_32.bmp");
            this.imageViewer1.DoubleClick += imageViewer1_DoubleClick;


            imageViewer1.Image = _bmp;
            AddLogInfo($"W:{imageViewer1.Image.Width}, H{imageViewer1.Image.Height}");
        }

        private void imageViewer1_DoubleClick(object sender, MouseEventArgs e)
        {
            var ctl = sender as UserControl;
            var Click = imageViewer1.ClientToImage(new Point(e.X, e.Y));
            AddLogInfo($"W:{Click.X}, H{Click.Y}");

        }

        ClassJson Cjson = new ClassJson() { ConfigDir = @"C:\Temp" };

        private void buttonJsonModify_Click(object sender, EventArgs e)
        {
            Cjson.air.High = 989898;
            Cjson.SaveJson();

        }
        private void buttonJsonSave_Click(object sender, EventArgs e)
        {
            if ((ClassJson)propertyGrid1.SelectedObject != null)
                Cjson = (ClassJson)propertyGrid1.SelectedObject;
            Dictionary<string, HighLowTemps> tmp = new Dictionary<string, HighLowTemps>();
            //tmp.Add("010", new HighLowTemps() { Low = 10, High = 15 });
            //Cjson.TestList.Add (new HighLowTemps() { High = 1, Low = 5 }); 
            Cjson.SaveJson();
        }
        int ML3FindCount = 100;
        int ML3SetCount = 200;
        bool ml3Bak = false;
        private void buttonJsonOpen_Click(object sender, EventArgs e)
        {
            var tmp = Cjson.LoadJson();
            propertyGrid1.SelectedObject = tmp;

        }
        void checkPulseOn()
        {

            if (ML3FindCount == ML3SetCount && ml3Bak == false)
            {
                AddLogInfo("On ");
                ml3Bak = true;
            }
            if (ML3FindCount != ML3SetCount && ml3Bak == true)
            {
                AddLogInfo("Off ");

                ml3Bak = false;
            }
        }
        private void btnMatrox_Click(object sender, EventArgs e)
        {
            int[,] A = new int[2, 2] { { 1, 1 }, { 10, 10 } };
            int[,] B = new int[2, 2] { { 10, 10 }, { 100, 100 } };
            int[,] C = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        C[i, j] += (A[i, k] * B[k, i]);
                    }
                    AddLogInfo(string.Format("{0},{1} : {2}", i, j, C[i, j]));
                    Console.WriteLine("{0},{1} : {2}", i, j, C[i, j]);

                }

                //AddLogInfo(string.Format("{0},{1} : {2}", i, j, C[i, j]));

                //Console.WriteLine();
            }
        }

        private void buttonXmlCompare_Click(object sender, EventArgs e)
        {
            TestXml tx = new TestXml();
            tx.Form1_Load(sender, e);
            AddLogInfo("end");
        }

        private void buttonIntToBit_Click(object sender, EventArgs e)
        {
            bool[] chek = new bool[10];
            int InReceive = 6;
            chek[0] = (InReceive & (uint)0x01 << 0) != 0 ? true : false;
            chek[1] = (InReceive & (uint)0x01 << 1) != 0 ? true : false;
            chek[2] = (InReceive & (uint)0x01 << 2) != 0 ? true : false;

            chek[3] = (InReceive & (uint)0x01) != 0 ? true : false;
            chek[4] = (InReceive & (uint)0x02) != 0 ? true : false;
            chek[5] = (InReceive & (uint)0x03) != 0 ? true : false;
        }
        public bool IsPaused { get { return !pause.WaitOne(0); } }
        ManualResetEvent pause;
        private void btnManualResetEvent_Click(object sender, EventArgs e)
        {
            pause = new ManualResetEvent(false);
            AddLogInfo($"{IsPaused}");

            Task.Run(() =>
            {
                Thread.Sleep(1000);
                pause.Set();    // 대기 해제
                AddLogInfo($"Set {IsPaused}");
            });

            // Check pause state
            //task.run(() => {
            //    thread.sleep(2000);
            //    pause.reset();    // another thread releases paused thread
            //    addloginfo($"reset {ispaused}");
            //});

            pause.WaitOne(); // 신호 대기
            AddLogInfo($"WaitOne {IsPaused}");




        }

        private void btnListTest_Click(object sender, EventArgs e)
        {
            List<List<check>> tet = new List<List<check>>();
            List<check> No1 = new List<check>();
            check ch1 = new check { boolean = false, intlean = 1 };
            check ch11 = new check { boolean = false, intlean = 11 };
            No1.Add(ch1);
            No1.Add(ch11);

            List<check> No2 = new List<check>();
            check ch2 = new check { boolean = false, intlean = 2 };
            check ch22 = new check { boolean = false, intlean = 22 };
            No2.Add(ch2);
            No2.Add(ch22);

            tet.Add(No1);
            tet.Add(No2);
        }

        private void btnFindValue_Click(object sender, EventArgs e)
        {
            string[] stringArray = new string[] { "1", "5", "4" };
            var check = Array.Exists(stringArray, x => Convert.ToInt32(x) < 1);
            if (check == true)
                log.AddLogMessage(LogType.Error, 0, $"Pmac1 Speed 설정 오류");
        }

        private void btnArrayTest_Click(object sender, EventArgs e)
        {
            bool[] chk = new bool[] { false, true, false };
            var aab = chk.Contains(true);
            //IncMMove(1000, 10);
        }


        private void btnTaskAwait_Click(object sender, EventArgs e)
        {
            log.Information($"{DateTime.Now.ToString("hh: mm:ss.FFF")} Tast Click Start");
            Console.WriteLine($"{DateTime.Now.ToString("hh: mm:ss.FFF")} = Tast Click Start");


            TaskTest_NoWait();

            //TaskTest_Wait(5);

            //Task<bool> sum1 = Task.Run(() => TaskFuc());
            //if (sum1.Result == true)
            //    log.Infomation($"Task Return true");
            AutoResetEvent ReadyDone = new AutoResetEvent(false);
            bool Cancel = false;

            //Task.Run(() => {
            //    Thread.Sleep(3000);
            //    ReadyDone.Set();
            //});
            //  bool check = CheckTimeoutTask(() => ReadyDone.WaitOne(3000), () => Cancel, 5000);

            log.Information($"{DateTime.Now.ToString("hh: mm:ss.FFF")}  Tast Click end");
            Console.WriteLine($"{DateTime.Now.ToString("hh: mm:ss.FFF")} = Tast Click End");

        }
        void TaskTest_NoWait()
        {
            Task.Run(() =>
            {
                log.Information($"TaskTest_NoWait Start");
                Thread.Sleep(2000);
                log.Information($"TaskTest_NoWait End");
            });
        }
        async static void TaskTest_Wait(int count)
        {
            Vars.log.Information($"TaskTest_Wait Start");

            await Task.Run(async () =>
            {
                for (int i = 1; i <= count; i++)
                {
                    Vars.log.Information($"{i}/{count} ...TaskTest_Wait");
                    await Task.Delay(100); // Thread.Sleep()의 비동기 버전
                }
            });
            Vars.log.Information($"TaskTest_Wait end");

        }
        public async Task<bool> TaskFuc()
        {
            log.Information($"TaskFuc Start");
            await Task.Delay(5000); // Thread.Sleep()의 비동기 버전
            log.Information($"TaskFuc end");
            return true;
        }
        public bool CheckTimeoutTask(Func<bool> func1, Func<bool> cancel, int timeout)
        {
            var watch = new Stopwatch();
            Console.WriteLine($"{DateTime.Now.ToString("hh: mm:ss.FFF")} = CheckTimeoutTask Start ");

            Task<bool> sum1 = Task.Run(async () =>
           {
               watch.Start();
               while (true)
               {
                   if (watch.ElapsedMilliseconds > timeout)
                   {//TimeOver
                       Console.WriteLine($"{DateTime.Now.ToString("hh: mm:ss.FFF")} = CheckTimeoutTask Time Over");
                       return true;
                   }
                   if (cancel())
                   {//진행 중 STop 
                       Console.WriteLine($"{DateTime.Now.ToString("hh: mm:ss.FFF")} = CheckTimeoutTask 긴급정지");
                       return true;
                   }
                   if (func1())
                   {// Time 안에 동작 성공 했을때
                       Console.WriteLine($"{DateTime.Now.ToString("hh: mm:ss.FFF")} = CheckTimeoutTask 성공");
                       return false;
                   }
                   await Task.Delay(100);
               }
           });
            sum1.Wait();
            //watch.Stop();
            Console.WriteLine($"{DateTime.Now.ToString("hh: mm:ss.FFF")} = CheckTimeoutTask end");

            return sum1.Result;
        }
        public async Task<bool> CheckTimeout(Func<bool> func1, Func<bool> func2, Func<bool> cancel, int timeout)
        {
            var watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                if (watch.ElapsedMilliseconds > timeout)
                {
                    return true;
                }
                if (cancel())
                {
                    return true;
                }
                if (func1() && func2())
                {
                    break;
                }
                await Task.Delay(100);
            }
            return false;
        }
        CancellationTokenSource cancelSource = new CancellationTokenSource();
        FormTest dialog;
        private async void btnFormTest_Click(object sender, EventArgs e)
        {
            dialog = new FormTest();
            Task.Run(() => { dialog.ShowDialog(); });

            Thread.Sleep(1000);
            Console.WriteLine("MessageBox Shown1");
            Thread.Sleep(1000);
            //Console.WriteLine("MessageBox Shown2");
            ////Application.OpenForms["FormTest"].Close();
            //dialog.CloseEvent();
            //Console.WriteLine("Dialog Closed");
            //   cancelSource = new CancellationTokenSource();
            //   var cancelToken = cancelSource.Token;
            //   //Task.Factory.StartNew(() =>
            //   // {
            //   //     Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:f") + "formOpen");
            //   //     var res = dialog.ShowDialog();
            //   // }, cancelToken, TaskCreationOptions.None,
            //   // TaskScheduler.FromCurrentSynchronizationContext());
            //   //var res = dialog.ShowDialog();



            //await   Task.Run(() => {
            //       Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:f") + "Teset1");
            //       Thread.Sleep(3000);
            //       Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:f") + "Teset2");
            //   });

            //   //if (!flowForm.IsDisposed && flowForm.Visible)
            //   if (!dialog.IsDisposed)
            //   {

            //       dialog.Close();
            //       Console.WriteLine(DateTime.Now.ToString("hh:mm:ss:f") + "formClose");

            //       //log.Information($"End FlowAction, StickNo {stickNo}, Flow {flow}");
            //   }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dialog.CloseEvent();
        }
        class mv
        {
            public int Gro { get; set; }
            public int index { get; set; }
            public string Name { get; set; }
            public double Position { get; set; }
            public int Speed { get; set; }
            public string neNm
            {
                get
                {
                    return index.ToString();

                }
            }
        }
        List<mv> LoadUnload_Postions()
        {
            List<mv> data = new List<mv>();
            data.Add(new mv() { Gro = 1, Name = "N1", Position = 2000, Speed = 20 });
            data.Add(new mv() { Gro = 1, Name = "N2", Position = 1000, Speed = 20 });
            data.Add(new mv() { Gro = 2, Name = "N3", Position = 2000, Speed = 20 });
            data.Add(new mv() { Gro = 2, Name = "N4", Position = 3000, Speed = 20 });
            data.Add(new mv() { Gro = 3, Name = "N5", Position = 4000, Speed = 20 });
            data.Add(new mv() { Gro = 3, Name = "N6", Position = 1000, Speed = 20 });
            return data;
        }
        private async void GroupAxis_Click(object sender, EventArgs e)
        {
            try
            {
                btnAxisGroub.BackColor = Color.YellowGreen;
               await Task.Run(() =>
                {
                    MovePositions(LoadUnload_Postions());
                    MovePositions(LoadUnload_Postions());
                });
                btnAxisGroub.BackColor = Color.Transparent;

            }
            catch (Exception ex)
            {
                log.AddLogMessage(LogType.Error, 0, ex.Message);
            }
        }
        private async Task MovePositions(List<mv> data)
        {
            //var task1 = Task.Run(() => {

            List<mv> List = data.OrderBy(x => x.Gro).ToList();//오름 차순 정렬
            var query = data.GroupBy(x => x.Gro);

            foreach (var result in query)
            {
                log.Information(result.Key.ToString());
                var moveList = new List<Task>();
                foreach (var person in result)
                {
                    log.Information(person.Name);
                    var t = Task.Run(() => moveAxis(person));
                    moveList.Add(t);
                }
                Task.WaitAll(moveList.ToArray());
            }
            //});

            // task1이 끝나길 기다렸다가 끝나면 결과치를 sum에 할당
            // await task1;

        }
        void moveAxis(mv ax)
        {
            string tmp = MethodBase.GetCurrentMethod().Name;
            string tmie = DateTime.Now.ToString("HH:mm:ss") + tmp + ax.Name;
            log.Information(tmie);
            Console.WriteLine(tmie);
            Thread.Sleep((int)ax.Position);

            tmie = DateTime.Now.ToString("HH:mm:ss") + tmp + ax.Name + "동작 완료";
            log.Information(tmie);
            Console.WriteLine(tmie);

        }
        void wait(Func<bool> rtn)
        {
            string msg = MethodBase.GetCurrentMethod().Name;// 현재 함수 이름 가져오기
            var nm = MethodBase.GetCurrentMethod();

            //var tmp000= MethodBase.GetCurrentMethod().
            //IEnumerable<bool> tmp = GetData<rtn>;
            ////MyMethod(svc => svc.GetCode(rtn.GetObjectData.));
            //var tmp0 =  MethodBase.GetCurrentMethod().rtn;
            var tmp1 = rtn.GetMethodInfo();
            var tmp2 = rtn.GetHashCode();
            var tmp3 = rtn.GetMethodInfo();

            Object mailMessageBody = rtn;
            Console.WriteLine(nameof(mailMessageBody)); // "greeting" 이 아닌 "mailMessageBody" 를 반환함에 유의하
            while (rtn() == false)
            {
            }


            //var trm4 = rtn.Method.GetCustomAttribute;
        }
        bool funcMsh() { return false; }
        bool funcNew() { return true; }
        public void Update<T>(T entity, object modify)
        {
            foreach (PropertyInfo p in modify.GetType().GetProperties())
            {

            }
        }
        public IEnumerable<T> GetData<T>(Expression<Func<IEnumerable<T>>> callbackExpression)
where T : class
        {
            var methodCall = callbackExpression.Body as MethodCallExpression;
            if (methodCall != null)
            {
                string methodName = methodCall.Method.Name;
            }

            return callbackExpression.Compile()();
        }
        public static TResult MyMethod<TResult>(Expression<Func<bool, TResult>> myFunction)

        {

            var parameterName = myFunction.Parameters.First().Name;

            var methodCallExpr = myFunction.Body as MethodCallExpression;

            var methodName = methodCallExpr.Method.Name;

            var methodArgumentName = methodCallExpr.Arguments.First().ToString();

            var entireExp = myFunction.ToString();

            return default(TResult);

        }

        private void btnFunc_Click(object sender, EventArgs e)
        {
            string greeting = "Hello!";
            Object mailMessageBody = greeting;
            Console.WriteLine(nameof(greeting)); // "greeting" 을 반환한다
            Console.WriteLine(nameof(mailMessageBody)); // "greeting" 이 아닌 "mailMessageBody" 를 반환함에 유의하

        }

        class LazyValue<T> where T : struct
        {
            private Nullable<T> val;
            private Func<T> getValue;

            // Constructor.
            public LazyValue(Func<T> func)
            {
                val = null;
                getValue = func;
            }

            public T Value
            {
                get
                {
                    if (val == null)
                        // Execute the delegate.
                        val = getValue();
                    return (T)val;
                }
            }
        }

        private void btnHardCheck_Click(object sender, EventArgs e)
        {
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                ProgressBar uiPgb_CDrive = new ProgressBar();
                ProgressBar uiPgb_DDrive = new ProgressBar();

                Label uiLb_CDrive_Title = new Label();
                Label uiLb_CDrive = new Label();
                
                foreach (DriveInfo drive in drives)
                {
                    if (drive.Name.Contains("D")) //C 드라이브
                    {
                        SetDriveSize(drive, uiPgb_CDrive, uiLb_CDrive_Title, uiLb_CDrive);
                    }
                    if (drive.DriveType == DriveType.Fixed)
                    {
                        //if (drive.Name.Contains("C")) //C 드라이브
                        //{
                        //    SetDriveSize(drive, uiPgb_CDrive, uiLb_CDrive_Title, uiLb_CDrive);
                        //}
                        //else //D 드라이브
                        //{
                        //    SetDriveSize(drive, uiPgb_DDrive, uiLb_CDrive_Title, uiLb_CDrive);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                log.Information(ex.Message);

            }
        }


        /// <summary>
        /// 드라이브 전체 용량, 사용량, 남은량 구하기
        /// 컨트롤에 값 저장
        /// </summary>
        /// <param name="drive"></param>
        public void SetDriveSize(DriveInfo drive, ProgressBar pb, Label title, Label lb)
        {
            string driveName = string.Empty;
            string totalSize = string.Empty;
            string freeSize = string.Empty;
            string usage = string.Empty;
            try
            {
                driveName = drive.Name.Substring(0, 1).ToString();
                totalSize = Convert.ToInt32(drive.TotalSize /1024 / 1024 / 1024).ToString(); //전체 사이즈
                freeSize = Convert.ToInt32(drive.AvailableFreeSpace/ 1024 / 1024 / 1024).ToString(); //남은 사이즈
                usage = (Convert.ToInt32(totalSize) -Convert.ToInt32(freeSize)).ToString(); //사용 용량
                //pb.Maximum = Convert.ToInt32(totalSize);
                //pb.Value = Convert.ToInt32(usage);
                log.Information( string.Format($"Disk[{driveName}:]  {totalSize}GB of {freeSize}GB available."));
            }
            catch { 
            
            }
        }

        private void btnIPchange_Click(object sender, EventArgs e)
        {
            string[] NewDNSSer = new string[] { "","","","" };
            //SetNicObject("","","","",NewDNSSer);
            string Name = "Realtek PCIe GbE Family Controller";
            //var tmp = ChangeIPAddress(Name, "192.168.0.251","255,255,255,0","192.168.0.1");
            var tmp = ChangeIPAddress(Name, "192.168.100.251","255,255,0,0","192.168.100.1");
        }
        #region IP 주소 변경하기 - ChangeIPAddress(sourceDescription, sourceIPAddress, sourceSubnetMask, sourceGateway)
        /// <summary> 
       /// IP 주소 변경하기 
       /// </summary>
       /// <param name="sourceDescription">소스 설명</param> 
       /// <param name="sourceIPAddress">소스 IP 주소</param>
       /// <param name="sourceSubnetMask">소스 서브넷 마스크</param> 
       /// <param name="sourceGateway">소스 게이트웨이</param> 
       /// <returns>처리 결과</returns> 
        public bool ChangeIPAddress(string sourceDescription, string sourceIPAddress, string sourceSubnetMask, string sourceGateway) 
       { 
            ManagementClass managementClass = new ManagementClass("Win32_NetworkAdapterConfiguration"); 
            ManagementObjectCollection managementObjectCollection = managementClass.GetInstances(); 
            foreach(ManagementObject managementObject in managementObjectCollection) 
            { 
                string description = managementObject["Description"] as string; 
                if(string.Compare(description, sourceDescription, StringComparison.InvariantCultureIgnoreCase) == 0) 
                {
                    try
                    {
                        ManagementBaseObject setGatewaysManagementBaseObject = managementObject.GetMethodParameters("SetGateways"); 
                        setGatewaysManagementBaseObject["DefaultIPGateway" ] = new string[] { sourceGateway }; 
                        setGatewaysManagementBaseObject["GatewayCostMetric"] = new int[] { 1 }; 
                        ManagementBaseObject enableStaticManagementBaseObject = managementObject.GetMethodParameters("EnableStatic"); 
                        enableStaticManagementBaseObject["IPaddress" ] = new string[] { sourceIPAddress }; 
                        enableStaticManagementBaseObject["SubnetMask"] = new string[] { sourceSubnetMask }; 
                        managementObject.InvokeMethod("EnableStatic", enableStaticManagementBaseObject, null);
                        managementObject.InvokeMethod("SetGateways" , setGatewaysManagementBaseObject , null); 
                        return true; 
                    } 
                    catch 
                    {
                        return false; 
                    } 
                } 
            } 
            return true;
        }
        #endregion

        
        public static void SetNicObject(string Caption, string IPAddress, string IPSubnet, string IPGateway, string[] DNSServer)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");

            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                // Make sure this is a IP enabled device. Not something like memory card or VM Ware
                if ((bool)mo["IPEnabled"])
                {
                    if (mo["Caption"].Equals(Caption))
                    {
                        ManagementBaseObject newIP = mo.GetMethodParameters("EnableStatic");
                        //newIP["IPAddress"] = IPAddress.Split(',');
                        //newIP["SubnetMask"] = new string[] { IPSubnet };
                        newIP["IPAddress"] = new string[] { IPAddress };
                        newIP["SubnetMask"] = new string[] { IPSubnet };
                        ManagementBaseObject setIP = mo.InvokeMethod("EnableStatic", newIP, null);

                        ManagementBaseObject newGate = mo.GetMethodParameters("SetGateways");
                        newGate["DefaultIPGateway"] = new string[] { IPGateway };
                        newGate["GatewayCostMetric"] = new int[] { 1 };
                        ManagementBaseObject setGateways = mo.InvokeMethod("SetGateways", newGate, null);

                        ManagementBaseObject newDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        newDNS["DNSServerSearchOrder"] = DNSServer;
                        ManagementBaseObject setDNS = mo.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);

                        break;
                    }
                }
            }
        }

        private void btndictionary_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("Cat","fa");
            values.Add("dog","can");

            if (values.ContainsKey("Cat"))
            { 
                           
            }
            var m = values.ToArray();
            Console.WriteLine(m[0]) ;
        }
        void initMotion()
        {
            for (int i = 0; i < (int)Axises.num; i++)
                cBAxis.Items.Add((Axises)i);
        }
        private void btnIncPlus_Click(object sender, EventArgs e)
        {
            Button ctr = sender as Button;
            double MovePos = Convert.ToInt32(txtMovePos.Text);
            int MoveSpd = Convert.ToInt32(txtSpeed.Text);
            Axises axName = (Axises)cBAxis.SelectedIndex;
            switch (ctr.Name)
            {
                case "btnabsMove":
                    mot.absMove(axName,MovePos,MoveSpd);

                    break;
                case "btnIncPlus":

                    break;
                case "btnIncMinus":
                    break;
            }
        }

        private void btnCase중복_Click(object sender, EventArgs e)
        {
            Axises mem = Axises.ScanX;
            switch (mem)
            {
                case Axises n when(n == Axises.ScanY || n == Axises.ScanX):
                    log.Information("선택한 축은 Scan입니다. ");
                    break;
                case Axises.ScanX:
                    log.Information("선택한 축은 Scan입니다. ???");
                    break;
                case Axises n when (n == Axises.StageU || n == Axises.StageV || n == Axises.StageW):
                    log.Information("선택한 축은 Stage입니다. ");
                    break;
            }
        }

        private void btnSendToCim_Click(object sender, EventArgs e)
        {
            HostToCIMData tmp = new HostToCIMData();
            tmp.RawPath = "데이터 받아라 서버야";
            Vars.CommClie.SendCIMCmd2(tmp);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HostToCIMData tmp = new HostToCIMData();
            tmp.RawPath = "테스트가 잘되가는구만";
       
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //int overTime = 60000 * 3;
                int overTime = 60 * 3;
                int checkTime = 0;
                Console.WriteLine(DateTime.Now + "시작");

                while (true)
                {
                    checkTime++;
                    Thread.Sleep(100);
                    if (overTime < checkTime)
                    {
                        throw new Exception("Shuttle Limit위치로 이동 중 Check TimeOver ");
                    }
                }
                Console.WriteLine(DateTime.Now + "끝");


            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "시간 오버");

            }

        }

        int procssCount = 0;
        System.Windows.Forms.Timer  tm = new System.Windows.Forms.Timer();
        
        private void btnProcessbar_Click(object sender, EventArgs e)
        {
            //progressBar1.Value = 100;
            //progressBar1.Visible = true;
            if (timer2.Enabled == true)
            {
                timer2.Enabled = false;
            }
            else
            {
                timer2.Enabled = true;

            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value >= 10)
                progressBar1.Value = 0;
            else
                progressBar1.Value += 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.SelectedPath = @"D:\OriginalImage\";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                log.AddLogMessage(LogType.Information, 0, fbd.SelectedPath);
            }
            else
            {
                log.AddLogMessage(LogType.Information, 0, "no Image");

            }
        }
    }
    public class TestXml
    {

        XmlDocument Doc1;
        XmlDocument Doc2;

        string checkPath = @"C:\tmp\ConfigT.xml";

        public void Form1_Load(object sender, System.EventArgs e)
        {
            Doc1 = new XmlDocument();
            Doc2 = new XmlDocument();
            Doc1.Load(@"C:\tmp\Config0.xml");
            Doc2.Load(@"C:\tmp\Config1.xml");
            Compare();
            //Doc1.Save(checkPath);
        }
        public void Compare()
        {
            foreach (XmlNode ChNode in Doc2.ChildNodes)
            {
                CompareLower(ChNode);
            }
        }
        public void CompareLower(XmlNode NodeName)
        {
            foreach (XmlNode ChlNode in NodeName.ChildNodes)
            {
                if (ChlNode.Name == "#text")
                {
                    continue;
                }
                string Path = CreatePath(ChlNode);

                if (Doc1.SelectNodes(Path).Count == 0)
                {
                    XmlNode TempNode = Doc1.ImportNode(ChlNode, true);
                    // Doc1.SelectSingleNode(Path.Substring(0, Path.LastIndexOf("/"))).AppendChild(TempNode);
                    Vars.log.AddLogMessage(LogType.Information, 0, $"{Path}");
                    Vars.log.AddLogMessage(LogType.Information, 0, $"{TempNode.LocalName}{TempNode.InnerXml}");
                    //Doc1.Save(checkPath);
                }
                else
                {
                    CompareLower(ChlNode);
                    //  Doc1.Save(checkPath);
                }
                foreach (XmlNode ChNode in Doc1.ChildNodes)
                {

                }

            }
        }

        public string CreatePath(XmlNode Node)
        {
            string Path = "/" + Node.Name;
            while (!(Node.ParentNode.Name == "#document"))
            {
                Path = "/" + Node.ParentNode.Name + Path;
                Node = Node.ParentNode;
            }
            Path = "/" + Path;
            return Path;
        }

      

    }
    public class check
    {
        public bool boolean { get; set; }
        public int intlean { get; set; }
    }
    public struct StruckT
    {
        public int Infocus;
        public int MotorBusy;

    };
    [Flags]
    public enum ClassIdx
    {
        no1,
        no2,
        no3,
        no4,
        no5

    }
    public class ClassTest
    {

        // 맴버 변수
        int[] _in = new int[10];
        int[] _out = new int[10];

        // 맴버 변수의 값을 설정하는 setter
        // 맴버 변수 data의 프로퍼티
        public int[] input
        {
            // getter와 같은 취득 함수
            get
            {
                return this._in;
            }
            // set을 제거하여 읽기 전용으로 만듬
        }

        public int[] Output
        {
            // getter와 같은 취득 함수
            get
            {
                return this._out;
            }
            // set을 제거하여 읽기 전용으로 만듬
        }
        public void SetOut()
        {

        }
    }
    public class ClassTest1
    {


    }
    public class GetValuesTest
    {
        enum Colors { Red, Green, Blue, Yellow };
        enum Styles { Plaid = 0, Striped = 23, Tartan = 65, Corduroy = 78 };
        ClassInput Nd = new ClassInput();

        public void Main()
        {
            bool aaaa = Nd[eIn.in0];
            Console.WriteLine(aaaa);
            Nd.Set(eIn.in0, true);
            Console.WriteLine(Nd[eIn.in0]);
            if (Nd[eIn.in0] == false)
            {

            }

            refTest(Nd[eIn.in0]);
            Task.Run(() => test());
            waittime(Nd[eIn.in0]);
            Console.WriteLine(Nd[eIn.in0]);
        }
        void test()
        {
            Thread.Sleep(3000);
            Nd.Set(eIn.in0, false);
        }
        void waittime(bool valueT)
        {
            while (valueT == true)
            {
                Thread.Sleep(100);
                bool aaaa = Nd[eIn.in0];
            }
        }
        public void refTest(bool valueT)
        {
            valueT = false;
        }

    }
    public class ClassInput
    {
        // 맴버 변수
        public bool[] data = new bool[10];
        // 생성자 (가변 파라미터로 데이터를 받는다.)
        public ClassInput(params eIn[] datas)
        {
            //for (int i = 0; i < datas.Length; i++)
            //{
            //    this[datas[i]] = i;
            //}
        }
        // 이것이 인덱서 문법이다.
        // 반환형과 함수명 자리에는 this를 넣고, 배열같이 대괄호로 데ㅇ이터를 받는다.
        public bool this[eIn keyword]
        {
            // 인덱서의 get입니다.
            get
            {
                return data[(int)keyword];
            }
            // 인덱서의 set입니다. (접근 제한자를 private로 설정함)
            private set
            {
                Set(keyword, value);
            }
        }
        public void Set(eIn idx, bool flag)
        {
            data[(int)idx] = flag;
        }

    }
    public class ClassOutput
    {
        // 맴버 변수
        public bool[] data = new bool[10];
        // 생성자 (가변 파라미터로 데이터를 받는다.)
        public ClassOutput(params eOut[] datas)
        {
            //for (int i = 0; i < datas.Length; i++)
            //{
            //    this[datas[i]] = i;
            //}
        }
        // 이것이 인덱서 문법이다.
        // 반환형과 함수명 자리에는 this를 넣고, 배열같이 대괄호로 데ㅇ이터를 받는다.
        public bool this[eOut keyword]
        {
            // 인덱서의 get입니다.
            get
            {
                return data[(int)keyword];
            }
            // 인덱서의 set입니다. (접근 제한자를 private로 설정함)
            private set
            {
                Set(keyword, value);
            }
        }
        public void Set(eOut idx, bool flag)
        {
            data[(int)idx] = flag;
        }

    }


}
