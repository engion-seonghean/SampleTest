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

namespace PmacIO
{
    public partial class UserControlInOut : UserControl
    {
        public UserControlInOut()
        {
            InitializeComponent();
        }
        private Button[] BtnList;
        BackgroundWorker bk_Update;
        [Category("Appearance"), Description("입력=0, 출력=1")]
        public bool AAInShow
        {
            get { return _InShow; }
            set { _InShow = value; } 
        }
        bool _InShow; //입력으로 보여줄지 출력으로 보여줄지 판단


        private void UserControlInOut_Load(object sender, EventArgs e)
        {
            BtnList = new Button[] { btnNo0, btnNo1, btnNo2, btnNo3, btnNo4, btnNo5 };

            this.bk_Update = new BackgroundWorker();
            this.bk_Update.WorkerReportsProgress = true;
            this.bk_Update.WorkerSupportsCancellation = true;
            this.bk_Update.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bk_Update_DoWork);
            this.bk_Update.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bk_Update_ProgressChanged);

            this.bk_Update.RunWorkerAsync();
            IOListLoad(0);
            IOBtnListUpdate(0, 0);
            designMotion();

        }
        private void designMotion()
        {//InitializeComponent 부분에 추가해야 함
            if (_InShow == true)
                dGV.Columns.RemoveAt(2);

            dGV.RowTemplate.Height = 20;// Row Cell 크기 조절하기

            //마우스로 Cell 크기 변경 못하도록 처리하기
            this.dGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.dGV.ClearSelection();

            foreach (DataGridViewColumn i in dGV.Columns)
            {//header 클릭했을때 순열 변경되는 기능 없애기
                i.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            this.dGV.CurrentCell = null;//커서 선택 안되게 하기
            this.dGV.Rows[dGV.Rows.Count - 1].Selected = true;////커서 선택 안되게 하기
            this.dGV.ScrollBars = ScrollBars.Both;// 스크롤바 사용 유/무 설정하기

        }
        private void IOListLoad(int index)
        {
            this.dGV.Rows.Clear();
            switch (_InShow)
            {
                case true:
                    for (int i = index * 16; i < (index + 1) * 16; i++)
                    {
                        this.dGV.Rows.Add(i.ToString(), ((eIn)i).ToString());
                    }
                    break;
                case false:
                    for (int i = index * 16; i < (index + 1) * 16; i++)
                    {
                        this.dGV.Rows.Add(i.ToString(), ((eOut)i).ToString());
                    }
                    break;
            }

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
                    //var tt = (motSta[0].Status.HomeComplete == false) ? Color.Red : Color.LightGray;
                    //if (mot != null)
                    //    bk_Update.ReportProgress(0);
                }
            }
            catch (Exception ex)
            {
                // log.AddLogMessage(LogType.Error, 0, ex.Message);
            }

        }


        private void bk_Update_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {

                for (int i = 0; i < 16; i++)
                {

                    //if (Vars.Motion.mOut[(CurrentOutPage * 16) + i] == true)
                    //    this.dGV.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
                    //else
                    //    this.dGV.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                dGV.Refresh();
            }
            catch (Exception ee)
            {
                //  log.AddLogMessage(LogType.Error, 0, ee.Message);
            }
        }
        int CurrentInPage = 0;
        int CurrentOutPage = 0;
        private void btn_NoPage_Click(object sender, EventArgs e)
        {
            int jobNo = Convert.ToInt16((sender as Button).Tag);
            //int ioNo = tabControl_IO.SelectedIndex;

            string InOut = (sender as Button).Name;
            string cmdname = (sender as Button).Text;
            CurrentInPage = jobNo;
            IOListLoad(jobNo);
            IOBtnListUpdate(0, jobNo);

        }
        private void IOBtnListUpdate(int IOindex, int index)
        {
            if (IOindex == 0)
            {
                for (int i = 0; i < BtnList.Length; i++)
                {
                    if (i == index)
                        BtnList[index].BackColor = Color.Red;
                    else
                        BtnList[i].BackColor = Color.Transparent;
                }
            }

        }
        private void dGV_Out_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 2)
            {
                //Output 눌러주기
                int index = -1;
                for (int i = 0; i < BtnList.Length; i++)
                {
                    if (BtnList[i].BackColor == Color.Red)
                        index = i;
                }
                int a = index * 16 + e.RowIndex;
                ////MessageBox.Show(string.Format("{0}", (index * 16 + e.RowIndex).ToString()));

                //bool Result = cm.Info($"{((eOut)(index * 16 + e.RowIndex)).ToString()}\r\nOutput 동작을 수행하겠습니까? \r\n *인터락 미적용*");
                //if (Result != true)
                //    return;

                if (this.dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor == Color.Lime)
                {
                    //Vars.Motion.SetOutput((index * 16 + e.RowIndex), false);
                    this.dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

                }
                else
                {
                    //Vars.Motion.SetOutput((index * 16 + e.RowIndex), true);
                    this.dGV.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Lime;

                }
            }
        }
        enum eIn
        {
            In_sp0,
            In_sp1,
            In_sp2,
            In_sp3,
            In_sp4,
            In_sp5,
            In_sp6,
            In_sp7,
            In_sp8,
            In_sp9,
            In_sp10,
            In_sp11,
            In_sp12,
            In_sp13,
            In_sp14,
            In_sp15,
            In_sp16,
            In_sp17,
            In_sp18,
            In_sp19,
            In_sp20,
            In_sp21,
            In_sp22,
            In_sp23,
            In_sp24,
            In_sp25,
            In_sp26,
            In_sp27,
            In_sp28,
            In_sp29,
            In_sp30,
            In_sp31,
        }
        enum eOut
        {
            Out_sp0,
            Out_sp1,
            Out_sp2,
            Out_sp3,
            Out_sp4,
            Out_sp5,
            Out_sp6,
            Out_sp7,
            Out_sp8,
            Out_sp9,
            Out_sp10,
            Out_sp11,
            Out_sp12,
            Out_sp13,
            Out_sp14,
            Out_sp15,
        }
    }
}
