using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PmacIO
{
   public class Ethernet_Client
    {
        BackgroundWorker CIM_EQState; 
        
        public Ethernet_Client()
        {
            init();
        }
        void init()
        {
            this.CIM_EQState = new BackgroundWorker();
            this.CIM_EQState.WorkerReportsProgress = true;
            this.CIM_EQState.WorkerSupportsCancellation = true;
            this.CIM_EQState.DoWork += new System.ComponentModel.DoWorkEventHandler(this.CIM_EQState_DoWork);
            this.CIM_EQState.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.CIM_EQState_ProgressChanged);
            this.CIM_EQState.RunWorkerAsync();
        }
        private void CIM_EQState_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                Vars.log.Information("server 0");
                Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, Vars.Ecat_ClientPort);
                server.Bind(ep);
                server.Listen(5);
                Vars.log.Information("server 1");
                while (true)
                {
                    Socket socket = server.Accept();
                    socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                    NetworkStream networkStream = new NetworkStream(socket);
                    Vars.log.Information("server 2");

                    try
                    {
                        BinaryFormatter fmt = new BinaryFormatter();
                        CIMToHostData ControlCommand = (CIMToHostData)fmt.Deserialize(networkStream);
                       // worker.ReportProgress(0, ControlCommand);
                        Vars.log.Information("server 3");

                    }
                    catch (Exception ee)
                    {
                        Vars.log.AddLogMessage(FuncEvent.LogType.Error, 0, "1CIM_EQState_DoWork Error\r\n" + ee.Message);
                        // break;
                    }
                    if (CIM_EQState.CancellationPending)
                        return;
                    
                }
            }
            catch (Exception ee)
            {
                Vars.log.AddLogMessage(FuncEvent.LogType.Error, 0, "2CIM_EQState_DoWork Error\r\n" + ee.Message);
            }
        }
        private void CIM_EQState_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Thread thread;
            thread = new Thread(new ParameterizedThreadStart(CIM_EQStateAsync));
            thread.Start(e.UserState);
        }
        HostToCIMData CIM_SendCMD2 = new HostToCIMData();

        private void CIM_EQStateAsync(object commandParams)
        {
            CIMToHostData ConCommand = commandParams as CIMToHostData;

            try
            {
                switch (ConCommand.Command)
                {
                    case CIMToHostCommand.ReqState:
                        CIM_SendCMD2.Command = HostToCIMCommand.EQ_State; //현재 다른 스테이트 사용할수 있을지....현재는 하나임
                      
                        CIM_SendCMD2.IsAutoRun = true;
                        CIM_SendCMD2.ConsoleAutoRun = true;
                        CIM_SendCMD2.HasGlass = true;

                        CIM_SendCMD2.AMAU_IO = new int[400];

                        SendCIMCmd2(CIM_SendCMD2);
                        break;
                }
            }
            catch (Exception ee)
            {
                Vars.log.AddLogMessage(FuncEvent.LogType.Error, 0, "CIM_EQStateAsync Error \r\n" + ee.Message);
            }
        }
        public void SendCIMCmd2(HostToCIMData cmdParams)
        {
            cmdParams.IsAutoRun = true;
            Thread t = new Thread(new ParameterizedThreadStart(_SendCIMCmd2));
            t.Start(cmdParams);
        }
        private void _SendCIMCmd2(object cmdParamObj)
        {
            HostToCIMData cmdParams = (HostToCIMData)cmdParamObj;
            Socket socket = null;
            NetworkStream stream = null;

            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect("localhost", Vars.Ecat_ServerPort);
                stream = new NetworkStream(socket, true);
                BinaryFormatter fmt = new BinaryFormatter();
                fmt.Serialize(stream, cmdParams);
                if (cmdParams.Command != HostToCIMCommand.EQ_State)
                    Vars.log.Information(string.Format("[_SendHostCmd] : {0}", cmdParams.Command));
                Vars.log.Information("Client 1");



                //byte[] buffer = new byte[1024];
                //int offset = 0;
                //int count = 1024;
                //stream.Read(buffer, offset, count);

                //XmlSerializer xs = new XmlSerializer(typeof(CIMToHostData));
                //using (StreamReader sr = new StreamReader(stream))
                //{
                //    CIMToHostData messageList = (CIMToHostData)xs.Deserialize(sr);
                //}

                //XmlSerializer xmlSerializer = new XmlSerializer(typeof(CIMToHostData));
                //string content = "";//we are going to deserialize the xml file here.
                //content = (string)xmlSerializer.Deserialize(stream);//deserialize data.

                CIMToHostData packet = new CIMToHostData();
                IFormatter formatter = new BinaryFormatter();
                 packet = (CIMToHostData)formatter.Deserialize(stream);
                Vars.log.Information(packet.Console_ver);
                Vars.log.Information("Client 2");


            }
            catch (Exception ee)
            {
                Vars.log.AddLogMessage(FuncEvent.LogType.Error, 0, string.Format("[_SendHostCmd] Error : {0}", ee.Message));
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                if (socket != null)
                    socket.Close();
            }
        }

    }

    [Serializable]
    public enum HostToCIMCommand //Control → CIM PGM     >> >> ControlToCimCmd
    {
        Ready,
        Response, // 그냥 의미 없는 데이터, 살아 있음을 표시
        InspectStart, // 검사 시작
        InspectDone,  // 검사 끝
        ScanReadyStart,  // Lift 하강중
        ScanReadyDone, // 하강완료(Scan Ready)
        LoadReadyStart,    // Lift 상승중
        LoadReadyDone, // 상승완료(Load Ready)
        UnloadReadyStart,
        UnloadReadyDone, // 상승완료(Unload Ready)
        ScanStart,
        ScanEnd,
        RecipeReq,
        CurrentRecipeByEqp,
        CurrentRecipeByOp,
        SetPM,    // 장비 PM 모드로 변경할때
        EQ_State, //장비 상태 보고 할때 사용
        Alarm, //알람 발생
        AlarmClear, //알람 클리어
        Recipe_Create,  //레시피 변경
        Recipe_Change, //레시피 변경
        Recipe_Delete //레시피 변경
    }
    [Serializable]
    public class CIMToHostData  //CIM → Control PGM ↔ Console PGM   >> CimToControlData
    {
        public CIMToHostData()
        {
            LotID = "LotID";
            PPID = "PPID";
            PortCasetteID = "CasetteID";
            PanelID = "PanelID";
            FlowID = "ACT";
            StepID = "0123";
            EQPState = "R";
          
            AMAUAxisPosition = new long[10];  //20140721 bsC Cim 축 포지션 Display 관련

            Buzzer = false;
            Lamp_Red = false;
            Lamp_Yellow = false;
            Lamp_Green = false;
            GlassCount = 0;
            Panel_ScanNo = 0;
            Back_Scan = false;
            Control_ver = "";
            Console_ver = "";
            Next_Move_Direction = true;
           
            LastGlass = false;
            REF_EQPID = "";
            REF_STEPID = "";
            REF_LAYERID = "";
            REGISTERID = "";

            
        }
        public CIMToHostCommand Command { get; set; }
        public Inspect_Commands EQ_CMD { get; set; }

        public string LotID { get; set; }
        public string PPID { get; set; }
        public string PortCasetteID { get; set; }
        public string ProdType { get; set; }
        public string ProdID { get; set; }
        public string FlowID { get; set; }
        public string SlotNo { get; set; }
        public string PanelID { get; set; }
        public string StepID { get; set; }
      
        public long[] AMAUAxisPosition;  //축 포지션 Display 관련
        public bool Buzzer { get; set; } //사용하고 싶은면 true
        public bool Lamp_Red { get; set; } //사용하고 싶은면 true
        public bool Lamp_Yellow { get; set; } //사용하고 싶은면 true
        public bool Lamp_Green { get; set; } //사용하고 싶은면 true
        public int Panel_ScanNo { get; set; }
        public int GlassCount { get; set; }
        public string EQPState { get; set; }
        public bool Back_Scan { get; set; } //사용하고 싶은면 true
      
        public string Control_ver { get; set; }
        public string Console_ver { get; set; }
        public bool AutoRun { get; set; }
        public bool Next_Move_Direction { get; set; } //'true 정스캔' 'fasle 백스캔'
        public bool LastGlass { get; set; } //20181008 Last Glass 요청으로 진행함.
       
        public string REF_EQPID { get; set; } //20181101 A2 추가 요청으로...
        public string REF_STEPID { get; set; } //20181101 A2 추가 요청으로...
        public string REF_LAYERID { get; set; } //20181101 A2 추가 요청으로...
        //HSH 박건영 프로 요청사항 - CIM - CONSOLE - MRS Message전달
        public string REGISTERID { get; set; }

    
    }

}

