using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PmacIO
{
    public class PmacDeviceClass
    {
        public PmacDeviceClass()
        {

        }
        //public Log log = null;
        public int loglevel = 0;
        //void AddLogMessage(LogType logType, int id, string message)
        //{
        //    if (log != null && id >= loglevel)
        //    {
        //        log.AddLogMessage(logType, id, message);
        //    }
        //}
        public enum ErrList
        {

            ERR001 = 1, //Command not allowed during program execution (should halt program executionbefore issuing command)
            ERR002 = 2, //Password error (should enter the proper password)
            ERR003 = 3, //Data error or unrecognized command (should correct syntax of command)
            ERR004 = 4, //Illegal character: bad value (>127 ASCII) or serial parity/framing error(should correct the character and or check for noise on the serial cable)
            ERR005 = 5,// Command not allowed unless buffer is open(should open a buffer first)
            ERR006 = 6,// No room in buffer for command(should allow more room for buffer– DELETE or CLEAR other buffers)
            ERR007 = 7, //Buffer already in use(should CLOSE currently open buffer first)
            ERR008 = 8, //MACRO auxiliary communications error(should check MACRO ringhardware and software setup)
            ERR009 = 9, //Program structural error(e.g.ENDIF without IF)(should correct structure ofprogram)
            ERR010 = 10, //Both overtravel limits set for a motor in the C.S.(should correct or disable limits)
            ERR011 = 11, //Previous move not completed(should Abort it or allow it to complete)
            ERR012 = 12, //A motor in the coordinate system is open-loop(should close the loop on the motor)
            ERR013 = 13, //A motor in the coordinate system is not activated(should set Ix00 to 1 or remove motor from C.S.)
            ERR014 = 14, //No motors in the coordinate system(should define at least one motor in C.S.)
            ERR015 = 15, //Not pointing to valid program buffer(should use B command first, or clear out scrambled buffers)
            ERR016 = 16, //Running improperly structuredprogram(e.g.missing ENDWHILE)(should correct structure ofprogram)
            ERR017 = 17, //Trying to resume after / or \ with motors out of stopped position(should use J= to return motor
            ERRO = 1000 //Unknown Error
        }
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private string PmacIP = "192.6.94.5";//변경
                                             // private string PmacIP = "192.6.95.5";//변경 간혹 IP 다른 PMAC Controller 있음
        private int PmacPort = 1025;
        public void ConnectAsync()
        {
            //await System.Threading.Tasks.Task.Run(() => { PmacConnect(); });
            if (PingTest(PmacIP))
                PmacConnect();
            //else
            //    MessageBox.Show("Motion control 연결 실패, Motion Control을 확인해주세요");
        }
        public bool IsOpen { get; set; }

        public void PmacConnect()
        {
            try
            {
                socket.NoDelay = false;
                //socket.ReceiveTimeout = 5;
                //socket.SendTimeout = 5;
                socket.Connect(PmacIP, PmacPort);
                this.IsOpen = true;

            }
            catch (Exception)
            {
                //Vars.log.AddLogMessage(Engion.LogType.Error, 0, "Motion Can not connection, Pleas check Pmac Status");
                this.IsOpen = false;
                socket.Close();
            }
        }

        public void PmacClose()
        {
            this.IsOpen = false;
            socket.Close();
        }

        public byte[] GetPMACCommand(string command)
        {
            byte[] buff = new byte[8 + command.Length];
            buff[0] = 0x40;
            buff[1] = 0xbf;
            Buffer.BlockCopy(BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)command.Length)), 0, buff, 6, 2);
            Encoding.Default.GetBytes(command, 0, command.Length, buff, 8);
            return buff;
        }
        object lockobj = new object();
        public string GetResponse(string command)
        {

            string restring = string.Empty;

            try
            {
                lock (lockobj)
                {
                    byte[] buf = new byte[1500];
                    var writelen = socket.Send(GetPMACCommand(command));

                    int readlen = socket.Receive(buf, 1400, SocketFlags.None);

                    restring = Encoding.Default.GetString(buf, 0, readlen - 1);

                }
            }
            catch (Exception ex)
            {
                //throw;
                //Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            return restring;

        }

        static public string CheckError(string msg)
        {
            string errorString = msg;

            if (msg.Contains("ERR001"))
                errorString = msg + ": Command not allowed during program execution (should halt program executionbefore issuing command)";
            else if (msg.Contains("ERR002"))
                errorString = msg + ": Password error (should enter the proper password)";
            else if (msg.Contains("ERR003"))
                errorString = msg + ": Data error or unrecognized command (should correct syntax of command)";
            else if (msg.Contains("ERR004"))
                errorString = msg + "Illegal character: bad value (>127 ASCII) or serial parity/framing error(should correct the character and or check for noise on the serial cable)";
            else if (msg.Contains("ERR005"))
                errorString = msg + "Command not allowed unless buffer is open(should open a buffer first)";
            else if (msg.Contains("ERR006"))
                errorString = msg + "No room in buffer for command(should allow more room for buffer– DELETE or CLEAR other buffers)";
            else if (msg.Contains("ERR007"))
                errorString = msg + "Buffer already in use(should CLOSE currently open buffer first)";
            else if (msg.Contains("ERR008"))
                errorString = msg + "MACRO auxiliary communications error(should check MACRO ringhardware and software setup))";
            else if (msg.Contains("ERR009"))
                errorString = msg + "Program structural error(e.g.ENDIF without IF)(should correct structure ofprogram)";
            else if (msg.Contains("ERR010"))
                errorString = msg + "Both overtravel limits set for a motor in the C.S.(should correct or disable limits)";
            else if (msg.Contains("ERR011"))
                errorString = msg + "Previous move not completed(should Abort it or allow it to complete)";
            else if (msg.Contains("ERR012"))
                errorString = msg + "A motor in the coordinate system is open-loop(should close the loop on the motor)";
            else if (msg.Contains("ERR013"))
                errorString = msg + "A motor in the coordinate system is not activated(should set Ix00 to 1 or remove motor from C.S.)";
            else if (msg.Contains("ERR014"))
                errorString = msg + "No motors in the coordinate system(should define at least one motor in C.S.)";
            else if (msg.Contains("ERR015"))
                errorString = msg + "Not pointing to valid program buffer(should use B command first, or clear out scrambled buffers)";
            else if (msg.Contains("ERR016"))
                errorString = msg + "Running improperly structuredprogram(e.g.missing ENDWHILE)(should correct structure ofprogram)";
            else if (msg.Contains("ERR017"))
                errorString = msg + "Trying to resume after / or \\ with motors out of stopped position(should use J= to return motor";
            else if (msg.Contains("ERR0"))
                errorString = msg + "Unknown Error";

            return errorString;
        }
        public static bool PingTest(string ip)
        {
            bool result = false;
            try
            {
                Ping pp = new Ping();
                PingOptions po = new PingOptions(128, true);
                byte[] buf = Encoding.ASCII.GetBytes("PingConnectTest");
                PingReply reply = pp.Send(IPAddress.Parse(ip), 10, buf, po);
                result = (reply.Status == IPStatus.Success) ? true : false;
            }
            catch
            {
                //throw;
            }
            return result;
        }

    }
    public class Class_Clipper
    {

        private PmacDeviceClass mPmac = new PmacDeviceClass();
        //public bool[] mI = new bool[64];
        ClassInput mIn = new ClassInput();
        //public bool[] mO = new bool[64];
        ClassOutput mOut = new ClassOutput();

        public const int BASE_INPUT_ADDR = 2500;
        public const int BASE_OUTPUT_ADDR = 3500;

        public Class_Clipper()
        {
            Class_Clipper_Init();
        }
        public void Class_Clipper_Init()
        {

        }

        // Device 선택.
        public void SelectDevice(int DeviceNo)
        {

        }

        // UMAC 디바이스 오픈.
        public bool DeviceOpen()
        {
            //bool bRet = false;
            //if (mDeviceNo >= 0 && mDeviceNo < 255)
            //{
            //    mPmac.Open(mDeviceNo, out bRet);
            //}
            //mPmac.IsOpen= bRet;
            //if (bRet == true)
            //{
            //    Thread.Sleep(100);
            //    Start();
            //}
            //return bRet;

            {
                //mPmac.Open(mDeviceNo, out bRet);
                mPmac.ConnectAsync();
                if (mPmac.IsOpen)
                    StartMonitoring(null);
            }

            return mPmac.IsOpen = mPmac.IsOpen;
        }

        // Start Device + Monitoring
        public bool StartMonitoring(Form frm)
        {
            bool bRet;
            int deviceNo = 0;

            //mPmac.SelectDevice((int)frm.Handle, out deviceNo, out bRet);
            // bRet = DeviceOpen();

            if (mPmac.IsOpen == false)
            {
                MessageBox.Show("CLIPPER 오픈 에러");
            }
            else
            {
                // 변수 재선언 프로그램
                //SendCommand("ena plc2");
                Thread.Sleep(100);
                Start();
            }
            return mPmac.IsOpen;

        }
        private void Start()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
            BackgroundWorker MCCInformationLog = new BackgroundWorker();
            // MCCInformationLog.DoWork += new DoWorkEventHandler(MCCInformationLog_DoWork);
            //  MCCInformationLog.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)//수정이 필요함.
        {

            while (true)
            {
                try
                {
                    Thread.Sleep(30);

                    // 축정보 가져오기
                    if (AxisInOutStatus_RealTime() == false)
                        continue;
                    mappingIO();
                    // I/O Read
                    //ReadData();

                    //MCC
                    //Read_MCCData();


                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message);
                }
            }
        }

        public bool AxisInOutStatus_RealTime()
        {
            int InLength = 64;
            int OutLength = 32;
            string inputD = string.Format($"M{BASE_INPUT_ADDR},{InLength}");
            string OutputD = string.Format($"M{BASE_OUTPUT_ADDR},{OutLength}");
            string[] Data = GetResponse(inputD + OutputD).TrimEnd('\r').Split('\r');

            int index = 0;
            for (int i = 0; i < InLength; i++)
            {
                //mIn[i] = Data[index + i] == "1" ? true : false;
                mIn.Set((eIn)i, Data[index + i] == "1" ? true : false);
            }

            //index = InLength;
            for (int i = 0; i < OutLength; i++)
            { 
                //mOut[1, i] = Data[index + i] == "1" ? true : false;
                mOut.Set((eOut)i, Data[index + i] == "1" ? true : false);
            }


            return true;
        }
        public void mappingIO()
        {
            //string OutputD = string.Format($"M4000,10");
            //string[] Data = GetResponse(OutputD).TrimEnd('\r').Split('\r');
            //uint InputTmp = 0;
            //int idx = 0;

            //for (int k = 0; k < 24; k++)
            //    mO[idx + k] = (uint.Parse(Data[0]) & (uint)(0x1 << k)) != 0 ? true : false;

            //idx = 24;
            //for (int k = 0; k < 8; k++)
            //    mO[idx + k] = (uint.Parse(Data[1]) & (uint)(0x1 << k)) != 0 ? true : false;

            //idx = 24+8;
            //for (int k = 0; k < 24; k++)
            //    mO[idx + k] = (uint.Parse(Data[2]) & (uint)(0x1 << k)) != 0 ? true : false;
            //idx = 24 + 8+24;

            //for (int k = 0; k < 8; k++)
            //    mO[idx + k] = (uint.Parse(Data[3]) & (uint)(0x1 << k)) != 0 ? true : false;

            //if (i > 23 || i > (23 + 7))
            //{
            //    InputTmp = uint.Parse(Data[1]);// m2800(input)
            //    for (int k = 0; k < 24; k++)
            //        mIO[1, i] = (InputTmp & (uint)(0x1 << k)) != 0 ? true : false;
            //}
            //if (i <= (23 + 7) || i > (23 + 7 + 23))
            //{
            //    InputTmp = uint.Parse(Data[2]);// m2800(input)
            //    for (int k = 0; k < 24; k++)
            //        mIO[1, i] = (InputTmp & (uint)(0x1 << k)) != 0 ? true : false;
            //}
            //if (i <= (23 + 7 + 23) || i > (23 + 7 + 23 + 7))
            //{
            //    InputTmp = uint.Parse(Data[3]);// m2800(input)
            //    for (int k = 0; k < 24; k++)
            //        mIO[1, i] = (InputTmp & (uint)(0x1 << k)) != 0 ? true : false;
            //}



        }
        public string GetResponse(string command)
        {
            if (mPmac.IsOpen == false)
                return "";

            string strAnswer = "";
            try
            {
                //mPmac.GetResponse(mDeviceNo, command, out strAnswer);
                strAnswer = mPmac.GetResponse(command);
                strAnswer = strAnswer.TrimEnd();
            }
            catch
            {
            }
            return strAnswer;
        }
    }
    public enum eIn
    {
        in0,
        in1,
        in2,
        in3,
        in4,
    }
    public enum eOut
    {
        Out0,
        Out1,
        Out2,
        Out3,
        Out4,
    }
}
