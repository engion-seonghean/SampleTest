using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FuncEvent;
namespace PmacIO
{
    public partial class UserControlAxisStatus : UserControl
    {
        enum DataGuidViewCol { Axis, Position, Speed, NLimit, HomeSen, PLimit, Fault,HomeComplete };
        BackgroundWorker bk_Update;
        Log log => Vars.log;
        public UserControlAxisStatus()
        {
            InitializeComponent();
        }
        private ClassMotion mot
        {
            get { return Vars.motion; }
        }

        private void UserControlAxisStatus_Load(object sender, EventArgs e)
        {
            designMotion();

            this.bk_Update = new BackgroundWorker();
            this.bk_Update.WorkerReportsProgress = true;
            this.bk_Update.WorkerSupportsCancellation = true;
            this.bk_Update.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bk_Update_DoWork);
            this.bk_Update.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bk_Update_ProgressChanged);

            this.bk_Update.RunWorkerAsync();
            //  Vars.Mot.DeviceOpen();
        }
        private void designMotion()
        {//InitializeComponent 부분에 추가해야 함

            //dGV.RowTemplate.Height = dGV.Height/((int)Axises.num)-2;// Row Cell 크기 조절하기
            //dGV.RowTemplate.Height = (this.dGV.Size.Height - this.dGV.ColumnHeadersHeight) / (int)Axises.num;
            //마우스로 Cell 크기 변경 못하도록 처리하기
            //this.dGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // this.dGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //this.dGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //this.dGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //this.dGV.ClearSelection();
            //toolTip1.SetToolTip(dataGridViewMain, "Axis[Red=HomeCompleteErr], Position[Red=Busy], -/H/+[Red=Detect]");
            this.dGV.Columns[(int)DataGuidViewCol.Axis].ToolTipText = "Red -> HomeComplet is not";
            this.dGV.Columns[(int)DataGuidViewCol.Position].ToolTipText = "Show Position Data";
            this.dGV.Columns[(int)DataGuidViewCol.Speed].ToolTipText = "Red -> Axis Busy";
            this.dGV.Columns[(int)DataGuidViewCol.NLimit].ToolTipText = "Red -> Minus(-) Limit Detect";
            this.dGV.Columns[(int)DataGuidViewCol.HomeSen].ToolTipText = "Red -> Home Limit Detect";
            this.dGV.Columns[(int)DataGuidViewCol.PLimit].ToolTipText = "Red -> Plus(+) Limit Detect";
            this.dGV.Columns[(int)DataGuidViewCol.Fault].ToolTipText = "Red ->Axis Fault Err";
            this.dGV.Columns[(int)DataGuidViewCol.HomeComplete].ToolTipText = "Red -> Not Home Complete";

            //foreach (Axises enumItem in Enum.GetValues(typeof(Axises)))
            //{//행 아이템 추가
            //    this.dGV.Rows.Add(new object[] { enumItem.ToString() });
            //}


            //foreach (datagridviewcolumn i in dgv.columns)
            //{//header 클릭했을때 순열 변경되는 기능 없애기
            //    i.sortmode = datagridviewcolumnsortmode.notsortable;
            //}

            this.dGV.CurrentCell = null;//커서 선택 안되게 하기
            this.dGV.Rows[dGV.Rows.Count - 1].Selected = true;////커서 선택 안되게 하기
            this.dGV.ScrollBars = ScrollBars.Both;// 스크롤바 사용 유/무 설정하기

            motInfoBindingSource.DataSource = mot.motInfos;
        }
        private void bk_Update_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (bk_Update.CancellationPending == false)
                {
                    Thread.Sleep(300);
                    if (bk_Update.CancellationPending)
                        return;
                    bk_Update.ReportProgress(0);
                }
            }
            catch (Exception ex)
            {
                log.AddLogMessage(LogType.Error, 0, ex.Message);
            }

        }


        private void bk_Update_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                if (dGV.Rows.Count != 14) // (int)Axises.num
                    return;
                if (mot == null)
                    return;
                if (mot.motInfos == null)
                    return;
                for (int idx = 0; idx < dGV.Rows.Count; idx++)
                {
                    //var motInfo = mot.motInfos.Find(d => d.AxisNo == idx);
                    
                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.Axis].Style.BackColor = (motInfo.HomeComplete == false) ? Color.Red : Color.LightGray;

                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.Position].Value = motInfo.ActPos.ToString();//string.Format("{0:#,###}", motS.Position);//#,### mot.mMotorPos[idx].ToString();
                    ////dGV.Rows[idx].Cells[(int)DataGuidViewCol.Position].Value = 100000.ToString();//#,### mot.mMotorPos[idx].ToString();
                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.Speed].Style.BackColor = (motInfo.Vel0 == false) ? Color.Red : Color.LightGray;
                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.Speed].Value = Math.Abs(motInfo.ActSpeed).ToString("#");//string.Format("{0}", mot.mMotorSpeed[idx]);//
                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.NLimit].Style.BackColor = (motInfo.MinusLimit) ? Color.Red : Color.LightGray;
                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.HomeSen].Style.BackColor = (motInfo.HomeSensor) ? Color.Red : Color.LightGray;
                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.PLimit].Style.BackColor = (motInfo.PlusLimit) ? Color.Red : Color.LightGray;
                    //dGV.Rows[idx].Cells[(int)DataGuidViewCol.Fault].Style.BackColor = (motInfo.AmpFault) ? Color.Red : Color.LightGray;

                }
                dGV.Refresh();
            }
            catch (Exception ee)
            {
                log.AddLogMessage(LogType.Error, 0, ee.Message);

            }
        }

    

        private void dGV_SelectionChanged(object sender, EventArgs e)
        {
            //dGV.Rows[dGV.Rows.Count - 1].Selected = true;
            //dGV.Rows[0].Selected = true;
        }
    }
}
