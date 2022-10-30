using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuncEvent
{
    public enum PaletteType
    {
        GRAY = 0,
        RED = 1,
        GREEN = 2,
        BLUE = 3
    }
    public partial class ImageViewer : UserControl
    {
        public event EventHandler<MouseEventArgs> DoubleClick = delegate { };

        Bitmap OrigBM = null;

        //public InterpolationMode InterpolationMode { get; set; }
        public Rectangle DrawRect { get; set; }
        public bool ShowDrawRect { get; set; }
        public MouseModes MouseMode { get; set; }
        public bool UseFastDisplay { get; set; }
        public Rectangle DisplayImageRect { get; set; }
        public Point ScrollPosition { get; set; }

        int _Zoom;
        public int Zoom
        {
            get { return _Zoom; }
            set
            {
                if (pB.Image != null)
                {
                    //pB.Image = SetZoom(pB.Image, value);
                    //SetZoom(pB.Image, value);
                }
            }
        }
        public double ZoomX { get; }
        public double ZoomY { get; }

        public Image Image
        {
            get { return pB.Image; }
            set
            {
                
                OrigBM = (Bitmap)value;
                pB.Image = value;
                SetScroll();
            }
        }
        public Size ImageSize { get; }


        Bitmap SetZoom(Image img, double zoomValue)
        {
            //Bitmap _bmp = new Bitmap(@"C:\TestPadData_32.bmp");
            Bitmap _bmp = OrigBM;
            double dZoomprecent = zoomValue;//50;
            Bitmap _bm = null;
            if (zoomValue == 0)
            {
                pB.SizeMode = PictureBoxSizeMode.StretchImage;
                _bm = new Bitmap(_bmp, (int)(_bmp.Size.Width ), (int)(_bmp.Size.Height));
            }
            else
            {
                pB.SizeMode = PictureBoxSizeMode.Normal;
                _bm = new Bitmap(_bmp, (int)(_bmp.Size.Width * dZoomprecent / 100), (int)(_bmp.Size.Height * dZoomprecent / 100));
            }
            Graphics g = CreateGraphics();
            g.DrawImage(_bm, 0, 0);
            pB.Image = _bm;
            return _bm;
        
        }
        private void ImageViewer_Load(object sender, EventArgs e)
        {

        }
        public void ChangePalette(PaletteType paletteType)
        {
        }
        public void ChangePalette(Color[] color)
        {
        }
        public PointF ClientToImage(PointF screenPoint)
        {
           
            return new PointF();
        }
        public Point ClientToImage(Point screenPoint)
        {
            Point rtnPoint = coodiImage.DstToSrc(screenPoint);
            return rtnPoint;
        }
        public void DrawPointAtCenter(Point point)
        {

        }
        public Color GetValue(Point point)
        {
            return new Color();
        }
        public Rectangle ImageToClient(Rectangle realRect)
        {
            Rectangle rtnPoint = coodiImage.SrcToDst(realRect);
            return rtnPoint;
        }
        RectangleF ImageToClient(RectangleF realRect)
        {
            //Point rtnPoint = coodiImage.SrcToDst(realRect);
            return new RectangleF();
        }
        public Size ImageToClient(Size realSize)
        {
            Size rtnSize = coodiImage.SrcToDst(realSize);

            return rtnSize;
        }
        SizeF ImageToClient(SizeF realSize)
        {
        
            return new SizeF();
        }
        public Point ImageToClient(Point realPoint)
        {
            Point rtnPoint = coodiImage.SrcToDst(realPoint);
            return rtnPoint;
        }
         PointF ImageToClient(PointF realPoint)
        {
            return new PointF();
        }
         Point[] ImageToClient(Point[] realPoint)
        {
            Point[] point = new Point[3];
            return point;
        }
         PointF[] ImageToClient(PointF[] realPoint)
        {
            PointF[] point = new PointF[3];
            return point;
        }
   
        public ImageViewer()
        {
            InitializeComponent();
        }
        private void SetScroll()
        {
            if (pB.Image == null) return;
            hScrollBar1.Width = pB.Width;
            hScrollBar1.Left = pB.Left;
            hScrollBar1.Top = pB.Bottom;
            hScrollBar1.Maximum = pB.Image.Width - pB.Width;

            vScrollBar1.Height = pB.Height;
            vScrollBar1.Left = pB.Left + pB.Width;
            vScrollBar1.Top = pB.Top;
            vScrollBar1.Maximum = pB.Image.Height - pB.Height;
        }

        int ImgScrolX,ImgScrolY = 0;
        
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            ImgScrolY = (sender as VScrollBar).Value; 
            pB.Refresh();
        }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            ImgScrolX = (sender as HScrollBar).Value;
            pB.Refresh();
        }
        private void pB_Paint(object sender, PaintEventArgs e)
        {
            
            var pBox = sender as PictureBox;
            if (pBox.Image == null) return;
            e.Graphics.DrawImage(pBox.Image, e.ClipRectangle, ImgScrolX, ImgScrolY, e.ClipRectangle.Width,
              e.ClipRectangle.Height, GraphicsUnit.Pixel);
        }
        private void fitToScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ctr = sender as ToolStripMenuItem;
            switch (ctr.Name)
            {
                case "tSMZoom0":
                    break;
                case "tSMZoom10":
                    break;
                case "tSMZoom50":
                    break;
                case "tSMZoom100":
                    break;
                case "tSMZoom200":
                    break;
                case "tSMZoom400":
                    break;
            }
            if (pB.Image != null)
            {
                double zo = Convert.ToDouble(ctr.Tag);
               SetZoom(pB.Image, zo);
                ImageCoodinate(OrigBM);
            }
        }
        CoordinateConvert coodiImage = new CoordinateConvert();
        void ImageCoodinate(Bitmap img)
        {
            Rectangle srcImage = new Rectangle(0, 0, img.Width, img.Height);
            Rectangle dstImage = new Rectangle(0, 0, pB.Size.Width, pB.Size.Height);
            //Rectangle srcImage = new Rectangle(0, 0, 1000, 1000);
            //Rectangle dstImage = new Rectangle(0, 0, 100, 100);
            coodiImage.MakeFactor(srcImage,dstImage);

          
        }

        private void pB_Click(object sender, EventArgs e)
        {
            DoubleClick(sender, (MouseEventArgs)e);
        }

        private void imageResizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Bitmap sourceImage = (Bitmap)Image;


            //// 사이즈가 변경된 이미지(1/2로 축소)
            //int width = sourceImage.Width / 2;
            //int height = sourceImage.Height / 2;
            //Size resize = new Size(width, height);
            //Bitmap resizeImage = new Bitmap(sourceImage, resize);
            //resizeImage.Save(@"C:\tmp\TestResize.png");
        } 

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPEN = new OpenFileDialog();
            OPEN.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png|모든 파일(*.*)|*.*";
            //OPEN.Filter = "이미지 파일(.jpg)|*.jpg|모든 파일(*.*)|*.*";
            OPEN.Title = "이미지 열기";
            OPEN.FileName = "";
            //OPEN.ShowDialog();
            if (OPEN.ShowDialog() == DialogResult.Cancel)
                return;
            // pB.Image = new Bitmap(OPEN.FileName);
            Image = new Bitmap(OPEN.FileName);
        }

       
    }
    public enum MouseModes
    {
        None = 0,
        Panning = 1,
        DrawRect = 2,
        EditRect = 3
    }
}
