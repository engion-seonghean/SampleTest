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
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        public void CloseEvent()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    //label1.Text = i.ToString();
                    this.Close();
                }));
            }
            else
            {
                //labe1l.Text = i.ToString();
                this.Close();

            }
        }
    }
}
