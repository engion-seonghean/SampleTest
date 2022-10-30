using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PmacIO
{
    public class ClassMotion
    {
        public event EventHandler<string> logOn = delegate { };
        public List<MotInfo> motInfos = new List<MotInfo>();
        public ClassMotion()
        {
            init();
        }
        public void init()
        {
            for (int i = 0; i < Enum.GetValues(typeof(Axises)).Length; i++)
            {
                motInfos.Add(new MotInfo()
                {
                    AxisName = (Axises)i,
                    AxisNo = i,
                });
            }
        }

        internal void absMove(Axises axName, double movePos, int Speed)
        {
            Task.Run(() =>
            {
                SimulMove(axName, movePos, Speed);
                //sitest();
            });

        }
        void sitest()
        {
            double tarPos = 1000000;
            double ActPos = 0;
            Stopwatch stopwatch = new Stopwatch(); //객체 선언
            stopwatch.Start(); // 시간측정 시작
            while (true)
            {
                Thread.Sleep(10);
                    ActPos = ActPos + 10000;
                
                if (tarPos <= ActPos)
                    break;

            }
            stopwatch.Stop(); //시간측정 끝
            Vars.log.Information($" test time : { stopwatch.ElapsedMilliseconds} ms");
        }
        void SimulMove(Axises axisNo, double pos, int Speed)
        {
            //속력 = 거리 / 시간
            //시간 = 거리 / 속력
            //거리 = 속력 * 시간
            int div = 10;
            int mode = 0;
            MotInfo Status = motInfos.Find(d => d.AxisName == axisNo);

            if (Status.ActPos < pos) mode = 0;
            else if (Status.ActPos > pos) mode = 1;
            else mode = 2;

            Status.Vel0 = true;
            Status.InPosition = false;

            double distance = Math.Abs((pos - Status.ActPos));
            double 구동시간 = distance / (Speed * 1000);
            int SleepTime = Convert.ToInt32(구동시간);//10000/(int)Speed;

            Stopwatch stopwatch = new Stopwatch(); //객체 선언
            stopwatch.Start(); // 시간측정 시작
            while (true)
            {
                Thread.Sleep(SleepTime);
                switch (mode)
                {
                    case 0:
                        Status.ActPos = Status.ActPos + distance / 100;
                        break;
                    case 1:
                        Status.ActPos = Status.ActPos - distance /100 ;// - 시간차증감;
                        break;
                    case 2:
                        Status.ActPos = pos;
                        break;
                }
                if (CheckPostion(axisNo, pos) == true)
                    break;
                
            }
            Status.Vel0 = false;
            Status.InPosition = true;
            stopwatch.Stop(); //시간측정 끝
            Vars.log.Information($"time : { stopwatch.ElapsedMilliseconds} ms");


        }

        private bool CheckPostion(Axises axisNo, double pos)
        {
            MotInfo Status = motInfos.Find(d => d.AxisName == axisNo);

            double curPos = Status.ActPos;
            double marjin = 10;

            if (Math.Abs(curPos - pos) < marjin && Status.Vel0)
                return true;
            else
                return false;
        }
    }
    public enum Axises
    {
        UnitX = 0,//PWM
        ReviewY = 1,//PWM
        StageX = 2,//ML3
        UnitY = 3,//ML3
        UnitZ = 4,//ML3
        LiftZ = 5,//ML3
        StageU = 6,//ML3
        StageV = 7,//ML3
        StageW = 8,//ML3
        UnitCheckY = 9,//ML3
        ScanX = 10,//PWM
        ScanY = 11,//PWM
        num

    }
    public class MotInfo
    {

        public Axises AxisName { get; set; }
        public int AxisNo { get; set; }
        public int InnerAxisNo { get; set; }
        public bool AmpEnable { get; set; }
        public bool PlusLimit { get; set; }
        public bool MinusLimit { get; set; }
        public bool HomeSensor { get; set; }
        public bool Vel0 { get; set; } = true; // 움직임 여부 true 정지, false 움직임.
        public bool HomeComplete { get; set; }
        public bool AmpFault { get; set; }
        public bool InPosition { get; set; }
        public bool IsActivated { get; set; }
        public double CmdSpeed { get; set; }
        public double ActSpeed { get; set; }
        public double CmdPos { get; set; }
        public double ActPos { get; set; }
        public double DestPos { get; set; }
        public bool OpenLoop { get; set; }
        public bool Pos0DegreeSensor { get; set; }
        public bool Pos90DegreeSensor { get; set; }
        public bool IsHomeMove { get; set; } // 홈 플로우를 시작하고 끝났는지 확인 피맥에서 ena 시작 쪽에 1 쓰고 끝나면 0쓴다.
        public double LoadFactor { get; set; }
        public int InitSpeed { get; set; } // 티치 모드 상태 시 속도
    }
}
