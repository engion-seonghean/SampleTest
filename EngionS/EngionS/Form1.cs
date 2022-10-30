using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engion;
namespace EngionS
{
    public partial class Form1 : Form
    {
        LogEx log => Vars.log;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitLog();
            
        }
        private void InitLog()
        {
            log.OnLogEvent += log_OnLogEvent;
            logViewer1.Log = Vars.log;
            log.AddLogMessage(LogType.Information, 0, "Program Start");
        }
        void log_OnLogEvent(object sender, LogEventArgs e)
        {
            if (e.LogType == LogType.Error)
            {
                BeginInvoke((Action)delegate
                {
                    //tabControlBaseStatus.SelectedIndex = 0 //0= 기본 상태 표시 전환 
                    // tabControlBaseStatus.SelectedIndex = 1;//1= LogView 전환
                });
            }
            logViewer1.ManageLog(e);
        }

        private void cBzoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap _bmp = new Bitmap(@"C:\TestPadData_32.bmp");
            imageViewerEx1.Image = BitmapBuf.FromBitmap(_bmp);
        }

        private void imageViewerEx1_Paint(object sender, PaintEventArgs e)
        {
            if (imageViewerEx1.Image == null) return;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CoordiConvert coorCon = new CoordiConvert();
            PointF[] EQBase1 = new PointF[] { new PointF(0, 0), new PointF(100, 0), new PointF(100, 100) };//Real Pos
            PointF[] EQBase2 = new PointF[] { new PointF(0, 0), new PointF(1000, 0), new PointF(1000, 1000) };//Real Pos
            coorCon.MakeFactor(EQBase1, EQBase2);

            var tmp  = coorCon.SourceToTarget(new PointF(50,50));
        }

        private void imageViewerEx1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
