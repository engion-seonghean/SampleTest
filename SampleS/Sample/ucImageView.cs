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
    public partial class ucImageView : UserControl
    {
        public ucImageView()
        {
            InitializeComponent();
        }

        private void buttonLoadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPEN = new OpenFileDialog();
            OPEN.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png|모든 파일(*.*)|*.*";
            //OPEN.Filter = "이미지 파일(.jpg)|*.jpg|모든 파일(*.*)|*.*";
            OPEN.Title = "이미지 열기";
            OPEN.FileName = "";
            OPEN.ShowDialog();
            
            imageViewer1.Image = new Bitmap(OPEN.FileName);
        }
    }
}
