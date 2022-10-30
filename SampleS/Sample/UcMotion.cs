using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PmacIO
{
    public partial class UcMotion : UserControl
    {
        public UcMotion()
        {
            InitializeComponent();
        }
        List<Motion> moti = new List<Motion>();
        private void UcMotion_Load(object sender, EventArgs e)
        {
            EvantB();
            for (int i = 0; i < 3; i++)
            {
                moti.Add(new Motion() { Name = $"Name{i}", No = i, Position = i, abs = i });
            }

            bSMot.DataSource = moti;
        }

        private void contextMenuStrip1_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                button1.BackColor = dlg.Color;
            }

        }


        private void dGV_Click(object sender, EventArgs e)
        {

        }
        void check(int idx)
        {
            Vars.log.AddLogMessage(FuncEvent.LogType.Information, 0, $"Pos{idx} click");
        }
        void EvantB()
        {
            //선택된 아이템의 Text를 저장해 놓습니다. 중요한 부분.
            //string selectedNickname = ctr.GetItemAt(e.X, e.Y).Text;
            //오른쪽 메뉴를 만듭니다
            //ContextMenu m = new ContextMenu();
            //메뉴에 들어갈 아이템을 만듭니다
            MenuItem[] mB = new MenuItem[5];
            for (int i = 0; i < mB.Length; i++)
            {
                mB[i] = new MenuItem() { Text = string.Format($"Pos{i}") };

                mB[i].Text = string.Format($"Pos{i}");
            }

            mB[0].Click += (senders, es) => { check(0); };
            mB[1].Click += (senders, es) => { check(1); };
            mB[2].Click += (senders, es) => { check(2); };
            mB[3].Click += (senders, es) => { check(3); };
            mB[4].Click += (senders, es) => { check(4); };
            for (int i = 0; i < mB.Length; i++)
            {
                m.MenuItems.Add(mB[i]);
            }

        }
        ContextMenu m = new ContextMenu();
        private void dGV_MouseClick(object sender, MouseEventArgs e)
        {
            var ctr = sender as DataGridView;
            int rowIndex = ctr.CurrentCell.RowIndex;

            int columnIndex = ctr.CurrentCell.ColumnIndex;


            //오른쪽 클릭일 경우
            if (e.Button.Equals(MouseButtons.Right) && columnIndex > 3)
            {

                //현재 마우스가 위치한 장소에 메뉴를 띄워줍니다
                m.Show(ctr, new Point(e.X, e.Y));
                //GetRecommendFriends();
            }
        }
    }
    enum DGVCol { Axis, Position, Speed, NLimit, HomeSen, PLimit, Fault, HomeComplete };

    public class Motion
    {
        public string Name { get; set; }
        public int No { get; set; }
        public double Position { get; set; }
        public string Speed { get; set; }
        public double abs { get; set; }
        public double Rel { get; set; }

        public Motion()
        {

        }

    }
}
