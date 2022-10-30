
namespace FuncEvent
{
    partial class ImageViewer
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.pB = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loadImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMZoom0 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMZoom1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSMZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageResizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pB)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 255);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(330, 22);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(310, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(20, 255);
            this.vScrollBar1.TabIndex = 2;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // pB
            // 
            this.pB.ContextMenuStrip = this.contextMenuStrip1;
            this.pB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pB.Location = new System.Drawing.Point(0, 0);
            this.pB.Name = "pB";
            this.pB.Size = new System.Drawing.Size(310, 255);
            this.pB.TabIndex = 3;
            this.pB.TabStop = false;
            this.pB.Click += new System.EventHandler(this.pB_Click);
            this.pB.Paint += new System.Windows.Forms.PaintEventHandler(this.pB_Paint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageToolStripMenuItem,
            this.zoomToolStripMenuItem,
            this.imageResizeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // loadImageToolStripMenuItem
            // 
            this.loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            this.loadImageToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadImageToolStripMenuItem.Text = "LoadImage";
            this.loadImageToolStripMenuItem.Click += new System.EventHandler(this.loadImageToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSMZoom0,
            this.tSMZoom1,
            this.tSMZoom25,
            this.tSMZoom50,
            this.tSMZoom100,
            this.tSMZoom200,
            this.tSMZoom400});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // tSMZoom0
            // 
            this.tSMZoom0.Name = "tSMZoom0";
            this.tSMZoom0.Size = new System.Drawing.Size(180, 22);
            this.tSMZoom0.Tag = "0";
            this.tSMZoom0.Text = "Fit to Screen";
            this.tSMZoom0.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // tSMZoom1
            // 
            this.tSMZoom1.Name = "tSMZoom1";
            this.tSMZoom1.Size = new System.Drawing.Size(180, 22);
            this.tSMZoom1.Tag = "10";
            this.tSMZoom1.Text = "10%";
            this.tSMZoom1.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // tSMZoom25
            // 
            this.tSMZoom25.Name = "tSMZoom25";
            this.tSMZoom25.Size = new System.Drawing.Size(180, 22);
            this.tSMZoom25.Tag = "25";
            this.tSMZoom25.Text = "25%";
            this.tSMZoom25.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // tSMZoom50
            // 
            this.tSMZoom50.Name = "tSMZoom50";
            this.tSMZoom50.Size = new System.Drawing.Size(180, 22);
            this.tSMZoom50.Tag = "50";
            this.tSMZoom50.Text = "50%";
            this.tSMZoom50.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // tSMZoom100
            // 
            this.tSMZoom100.Name = "tSMZoom100";
            this.tSMZoom100.Size = new System.Drawing.Size(180, 22);
            this.tSMZoom100.Tag = "100";
            this.tSMZoom100.Text = "100%";
            this.tSMZoom100.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // tSMZoom200
            // 
            this.tSMZoom200.Name = "tSMZoom200";
            this.tSMZoom200.Size = new System.Drawing.Size(180, 22);
            this.tSMZoom200.Tag = "200";
            this.tSMZoom200.Text = "200%";
            this.tSMZoom200.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // tSMZoom400
            // 
            this.tSMZoom400.Name = "tSMZoom400";
            this.tSMZoom400.Size = new System.Drawing.Size(180, 22);
            this.tSMZoom400.Tag = "400";
            this.tSMZoom400.Text = "400%";
            this.tSMZoom400.Click += new System.EventHandler(this.fitToScreenToolStripMenuItem_Click);
            // 
            // imageResizeToolStripMenuItem
            // 
            this.imageResizeToolStripMenuItem.Name = "imageResizeToolStripMenuItem";
            this.imageResizeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.imageResizeToolStripMenuItem.Text = "ImageResize";
            this.imageResizeToolStripMenuItem.Click += new System.EventHandler(this.imageResizeToolStripMenuItem_Click);
            // 
            // ImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pB);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Name = "ImageViewer";
            this.Size = new System.Drawing.Size(330, 277);
            this.Load += new System.EventHandler(this.ImageViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pB)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.PictureBox pB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tSMZoom0;
        private System.Windows.Forms.ToolStripMenuItem tSMZoom1;
        private System.Windows.Forms.ToolStripMenuItem tSMZoom25;
        private System.Windows.Forms.ToolStripMenuItem tSMZoom50;
        private System.Windows.Forms.ToolStripMenuItem tSMZoom100;
        private System.Windows.Forms.ToolStripMenuItem tSMZoom200;
        private System.Windows.Forms.ToolStripMenuItem tSMZoom400;
        private System.Windows.Forms.ToolStripMenuItem imageResizeToolStripMenuItem;
    }
}
