using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PmacIO
{
    public class Ethernet_Server
    {
        BackgroundWorker listenWorker;
        public Ethernet_Server()
        {
            init();
        }
        void init()
        {
            this.listenWorker = new BackgroundWorker();
            this.listenWorker.WorkerReportsProgress = true;
            this.listenWorker.WorkerSupportsCancellation = true;
            this.listenWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.listenWorker_DoWork);
            this.listenWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.listenWorker_ProgressChanged);

            this.listenWorker.RunWorkerAsync();
        }
        private void listenWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int portNo= Vars.Ecat_ServerPort;
            try
            {
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, portNo);
                server.Bind(ep);
                server.Listen(50);

                while (true)
                {
                    Thread.Sleep(10);
                    Socket socket = server.Accept();
                    socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                    NetworkStream networkStream = new NetworkStream(socket);

                    try
                    {
                        BinaryFormatter fmt = new BinaryFormatter();
                        HostToCIMData cmdParams = (HostToCIMData)fmt.Deserialize(networkStream);
                        worker.ReportProgress(0, cmdParams);

                        CIMToHostData CimData = new CIMToHostData() { Console_ver = "잘받닸고,다시 준다."};
                        fmt.Serialize(networkStream, CimData);
                        //using (var ms = new MemoryStream())
                        //{
                        //    fmt.Serialize(ms, CimData);
                        //    //return ms.ToArray();
                        //    socket.Send(ms.ToArray(), ms.ToArray().Length, SocketFlags.None);
                        //}

                        worker.ReportProgress(0, cmdParams);
                    }
                    catch (Exception ex)
                    {
                        Vars.log.AddLogMessage(FuncEvent.LogType.Error, 0, ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Vars.log.AddLogMessage(FuncEvent.LogType.Error,0, ex.Message);
            }
        }
        private void listenWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Thread thread;
            thread = new Thread(new ParameterizedThreadStart(HostToCIMData_EQState));
            thread.Start(e.UserState);
        }
        void HostToCIMData_EQState(object commandParams)
        {
            HostToCIMData cimData = commandParams as HostToCIMData;

            Vars.log.Information(cimData.RawPath);
            //CIMToHostData data = null;
           
        }
    }

    [Serializable] // Console ↔ Control PGM CMD 관련
    public enum Inspect_Commands
    {
        None = 0, //그냥 응답용
        InitRequest,
        InitResponse,
        ScanStart_Request,
        ScanStart_Response,
        ScanEnd_Request,
        ScanEnd_Response,
        InspectStart,
        InspectDone,
        Auto_Recipe,     //자동 RECIPE만들때 사용예정
        CurrentPos,
        LotStart_Req,
        LotStart_Res,
        LotEnd_Req,
        LotEnd_Res,
        ManualScan_Request,
        ManualScan_Response,
        GlassInfo_Response,
        Alarm,
        GlassInfo,
        ReqUnloadReady,
        UnloadReadyDone,
        ScanReady_Request,
        ScanReadyDone
    }

    [Serializable]
    public enum CIMToHostCommand //CIM → Control PGM
    {
        ReqState,
        InspectStart,
        LotStart,
        LotEnd,
        ReqLoadReady,
        ReqUnloadReady,
        ReqScanReady,
        ChangeBuzzer,
        ChangeLamp,
        ChangeRecipe,
        ReqGlassInfo,
        OPCall,
        ReqPM,
        ReqNormal,
        ReqOPCall,
        Init,
        TerminalMessage,
        MCC_Send,
        ReqPause,
        Index_Busy,
        Index_Nomal,
        ReqUnload_Glassinfo
    }

    [Serializable]
    public class HostToCIMData //Control → CIM PGM  >> ControlToCimData
    {
        public HostToCIMData()
        {
            RawPath = "";
            ImgPath = "";
            SumPath = "";
            Disk = "";
            IsAutoRun = true;
            ConsoleAutoRun = true;
            //Command = new HostToCIMCommand();

            //AMAUAxisPosition = new long[10];  //20140721 bsC Cim 축 포지션 Display 관련
            //recipes = new List<recipeBase>();
            //systemState = SystemStates.Ready;
            //AlarmList = new AlarmManager();
            Current_ScanNo = 0;

            //2016/11/02 추가요======================
            AMAU_SystemInPut = new bool[352];
            AMAU_SystemOutPut = new bool[352];
            AMAU_EFUSpeed = new string[15];
            for (int i = 0; i < AMAU_SystemInPut.Length; i++)
            {
                AMAU_SystemInPut[i] = false;
                AMAU_SystemOutPut[i] = false;
            }
            //2016/11/02 추가요=====================

            for (int i = 0; i < AMAU_EFUSpeed.Length; i++)
            {
                AMAU_EFUSpeed[i] = "700";
            }

            //for (int i = 0; i < (int)eSVIDClamp.num; i++)
            //{
            //    AMAU_ClampActionTime.Add(new ClampInfoCim() { SVIDName = ((eSVIDClamp)i) });
            //    //AMAU_ClampActionTime.Add(new ClampInfoCim() { SVIDName = ((eSVIDClamp)i), InIndex = i, time = TimeSpan.FromMinutes(10) });
            //}
            Control_ver = "";
            Console_ver = "";

            //부하율 관련
            CAMERAT_LoadFactor = 0;
            LEDX_LoadFactor = 0;
            LEDZ_LoadFactor = 0;
        }
        //public AlarmManager AlarmList { get; set; }

        public HostToCIMCommand Command { get; set; }
        public bool IsAutoRun { get; set; }
        public bool ConsoleAutoRun { get; set; }
        public string RawPath { get; set; }
        public string ImgPath { get; set; }
        public string SumPath { get; set; }
        public string Disk { get; set; }  //뭔지 모름
        public bool HasGlass { get; set; }
        public bool IsLoadReady { get; set; }
        public bool IsUnloadReady { get; set; }
        public bool IsScanReady { get; set; }
        public long[] AMAUAxisPosition;  //축 포지션 Display 관련
        public int[] AMAU_IO { get; set; } = new int[400];               //EQ MCC Action 관련 IO들 old:100
        public int[] AMAU_SignalInfo { get; set; } = new int[100];    //EQ MCC Signal 관련 IO들
        public double[] AMAU_MotionInfo { get; set; } = new double[100];    //EQ MCC Info 관련 IO들
        public int[] AMAU_MCCError { get; set; } = new int[100];        //EQ MCC Info 관련 IO들
        public bool[] AMAU_SystemInPut;
        public bool[] AMAU_SystemOutPut;

        //public List<recipeBase> recipes { get; set; } //20150608 Recipe 항목 추가

        //public SystemStates systemState { get; set; }
        //public ModuleStates moduleState { get; set; }
        //public ProcessStates processState { get; set; }
        //public recipeBase CurrentRecipe { get; set; } = new recipeBase { RecipeName = "" };
        public int Current_ScanNo { get; set; }




        //**********//부하율 관련 //FDC*********************************
        //FDC Info 관련 IO들
        public string Control_ver { get; set; }  //Control 버전
        public string Console_ver { get; set; }  //Console 버전

        public string UPS01_MAIN_CIRCUITE_BREAKER_CHK { get; set; } = "0";

        public string AMAU_EFU_CONTR { get; set; } = "0"; // 동작 상태 FFU_CONTROLLER (0 : OK , 1 : NG)
        public string[] AMAU_EFUSpeed;

        public double ShuttleX1_LoadFactor { get; set; } = 0;
        public double ShuttleX2_LoadFactor { get; set; } = 0;
        public double CAMERAT_LoadFactor { get; set; } = 0;
        public double LEDX_LoadFactor { get; set; } = 0;
        public double LEDZ_LoadFactor { get; set; } = 0;
        public double LEDT_LoadFactor { get; set; } = 0;
        public double LIFTZ_LoadFactor { get; set; } = 0;
        public double LIFTT_LoadFactor { get; set; } = 0;
        public double SHUTTLE_Speed { get; set; } = 0;
        public double SHUTTLEX2_Speed { get; set; } = 0;
        public double CAMERA_T_Speed { get; set; } = 0;
        public double LED_X_Speed { get; set; } = 0;
        public double LED_Z_Speed { get; set; } = 0;
        public double LED_T_Speed { get; set; } = 0;
        public double LIFTZ_Speed { get; set; } = 0;
        public double LIFTT_Speed { get; set; } = 0;

        public int[] AMAU_LiftUpDownTime { get; set; } = new int[2];        //FDC Info 관련 IO들
        //public List<ClampInfoCim> AMAU_ClampActionTime { get; set; } = new List<ClampInfoCim>();        //FDC Info 관련 IO들

        //public FDC_Xray[] AMAU_XrayLIfeTime { get; set; } = new FDC_Xray[(int)eXrayList.num];
    }
}
