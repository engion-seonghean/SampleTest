namespace EngionS
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitCMain = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.cBzoom = new System.Windows.Forms.ComboBox();
            this.imageViewerEx1 = new Engion.ImageViewerEx();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.logViewer1 = new Engion.LogViewer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitCMain)).BeginInit();
            this.splitCMain.Panel1.SuspendLayout();
            this.splitCMain.Panel2.SuspendLayout();
            this.splitCMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitCMain
            // 
            this.splitCMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitCMain.Location = new System.Drawing.Point(0, 0);
            this.splitCMain.Name = "splitCMain";
            this.splitCMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitCMain.Panel1
            // 
            this.splitCMain.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitCMain.Panel2
            // 
            this.splitCMain.Panel2.Controls.Add(this.logViewer1);
            this.splitCMain.Size = new System.Drawing.Size(823, 595);
            this.splitCMain.SplitterDistance = 432;
            this.splitCMain.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(823, 432);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.cBzoom);
            this.tabPage1.Controls.Add(this.imageViewerEx1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(815, 406);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(720, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cBzoom
            // 
            this.cBzoom.FormattingEnabled = true;
            this.cBzoom.Location = new System.Drawing.Point(721, 10);
            this.cBzoom.Name = "cBzoom";
            this.cBzoom.Size = new System.Drawing.Size(84, 20);
            this.cBzoom.TabIndex = 1;
            this.cBzoom.SelectedIndexChanged += new System.EventHandler(this.cBzoom_SelectedIndexChanged);
            // 
            // imageViewerEx1
            // 
            this.imageViewerEx1.BackColor = System.Drawing.SystemColors.Control;
            this.imageViewerEx1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageViewerEx1.DisplayImageRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.imageViewerEx1.DrawShape = null;
            this.imageViewerEx1.EnhanceImage = false;
            this.imageViewerEx1.EnhanceLevel = 1D;
            this.imageViewerEx1.EnhanceOffset = 0;
            this.imageViewerEx1.EnhanceRefLevel = 100;
            this.imageViewerEx1.EnhanceWhenMouseClick = true;
            this.imageViewerEx1.Image = null;
            this.imageViewerEx1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.imageViewerEx1.Location = new System.Drawing.Point(6, 3);
            this.imageViewerEx1.MouseMode = Engion.ImageViewerEx.MouseModes.Panning;
            this.imageViewerEx1.Name = "imageViewerEx1";
            this.imageViewerEx1.ScrollPosition = new System.Drawing.Point(0, 0);
            this.imageViewerEx1.ShowDrawShape = false;
            this.imageViewerEx1.Size = new System.Drawing.Size(711, 400);
            this.imageViewerEx1.TabIndex = 0;
            this.imageViewerEx1.UseFastDisplay = false;
            this.imageViewerEx1.Zoom = 1D;
            this.imageViewerEx1.Paint += new System.Windows.Forms.PaintEventHandler(this.imageViewerEx1_Paint);
            this.imageViewerEx1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.imageViewerEx1_MouseClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(815, 406);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // logViewer1
            // 
            this.logViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logViewer1.Location = new System.Drawing.Point(0, 0);
            this.logViewer1.Log = null;
            this.logViewer1.Name = "logViewer1";
            this.logViewer1.ReadOnly = true;
            this.logViewer1.Size = new System.Drawing.Size(823, 159);
            this.logViewer1.TabIndex = 0;
            this.logViewer1.Text = "";
            this.logViewer1.WordWrap = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(723, 90);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 22);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 595);
            this.Controls.Add(this.splitCMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitCMain.Panel1.ResumeLayout(false);
            this.splitCMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitCMain)).EndInit();
            this.splitCMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitCMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Engion.LogViewer logViewer1;
        private Engion.ImageViewerEx imageViewerEx1;
        private System.Windows.Forms.ComboBox cBzoom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

