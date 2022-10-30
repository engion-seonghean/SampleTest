using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FuncEvent;
namespace PmacIO
{
    public partial class UserControlRate : UserControl
    {
        public UserControlRate()
        {
            InitializeComponent();
        }
        /*
            1. 전체값에서 일부값은 몇 퍼센트
            =일부값 / 전체값 * 100
            2. 전체값의 몇 퍼센트는 얼마
            =전체값 * 퍼센트 /100
            3. 숫자를 몇 퍼센트 증가
            =숫자*(1+퍼센트/100)
            4. 숫자를 몇 퍼센트 감소
            =숫자*(1-퍼센트/100)
         */
        private void buttonPer_Click(object sender, EventArgs e)
        {
            double 일부값 = Convert.ToInt32(tB1.Text.Trim());
            double 전체값 = Convert.ToInt32(tB2.Text.Trim());
            double tmp = (일부값 / 전체값) * 100;
            Vars.log.AddLogMessage(LogType.Result,0,$"(일부값 / 전체값) * 100 = [ {tmp} ]");
        }

        private void buttonValue_Click(object sender, EventArgs e)
        {
            double 퍼센트 = Convert.ToInt32(tB4.Text.Trim());
            double 전체값 = Convert.ToInt32(tB3.Text.Trim());
            double tmp = (전체값 * 퍼센트) / 100;
            Vars.log.AddLogMessage(LogType.Result, 0, $"(전체값 * 퍼센트) / 100 = [ {tmp} ]");
        }

        private void buttonNumP_Click(object sender, EventArgs e)
        {
            double 퍼센트 = Convert.ToInt32(tB6.Text.Trim());
            double 숫자 = Convert.ToInt32(tB5.Text.Trim());
            double tmp = 숫자 * (1 + 퍼센트 / 100);
            Vars.log.AddLogMessage(LogType.Result, 0, $"숫자*(1+퍼센트/100) = [ {tmp} ]");
        }

        private void buttonNumN_Click(object sender, EventArgs e)
        {
            double 퍼센트 = Convert.ToInt32(tB6.Text.Trim());
            double 숫자 = Convert.ToInt32(tB5.Text.Trim());
            double tmp = 숫자 * (1 - 퍼센트 / 100);
            Vars.log.AddLogMessage(LogType.Result, 0, $"숫자*(1-퍼센트/100) = [ {tmp} ]");
        }
    }
}
