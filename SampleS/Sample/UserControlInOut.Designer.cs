
namespace PmacIO
{
    partial class UserControlInOut
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tLP = new System.Windows.Forms.TableLayoutPanel();
            this.btnNo0 = new System.Windows.Forms.Button();
            this.btnNo1 = new System.Windows.Forms.Button();
            this.btnNo2 = new System.Windows.Forms.Button();
            this.btnNo3 = new System.Windows.Forms.Button();
            this.btnNo4 = new System.Windows.Forms.Button();
            this.btnNo5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.tLP.SuspendLayout();
            this.SuspendLayout();
            // 
            // dGV
            // 
            this.dGV.AllowUserToAddRows = false;
            this.dGV.AllowUserToDeleteRows = false;
            this.dGV.AllowUserToResizeColumns = false;
            this.dGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewButtonColumn1});
            this.tLP.SetColumnSpan(this.dGV, 6);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGV.DefaultCellStyle = dataGridViewCellStyle2;
            this.dGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGV.Location = new System.Drawing.Point(3, 5);
            this.dGV.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dGV.MultiSelect = false;
            this.dGV.Name = "dGV";
            this.dGV.RowHeadersVisible = false;
            this.dGV.RowHeadersWidth = 62;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            this.dGV.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dGV.RowTemplate.Height = 20;
            this.dGV.RowTemplate.ReadOnly = true;
            this.dGV.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dGV.Size = new System.Drawing.Size(520, 323);
            this.dGV.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Index";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Name";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dataGridViewButtonColumn1.HeaderText = "Output";
            this.dataGridViewButtonColumn1.MinimumWidth = 8;
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Width = 80;
            // 
            // tLP
            // 
            this.tLP.ColumnCount = 6;
            this.tLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tLP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tLP.Controls.Add(this.dGV, 0, 0);
            this.tLP.Controls.Add(this.btnNo0, 0, 1);
            this.tLP.Controls.Add(this.btnNo1, 1, 1);
            this.tLP.Controls.Add(this.btnNo2, 2, 1);
            this.tLP.Controls.Add(this.btnNo3, 3, 1);
            this.tLP.Controls.Add(this.btnNo4, 4, 1);
            this.tLP.Controls.Add(this.btnNo5, 5, 1);
            this.tLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLP.Location = new System.Drawing.Point(0, 0);
            this.tLP.Name = "tLP";
            this.tLP.RowCount = 2;
            this.tLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.58823F));
            this.tLP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.41177F));
            this.tLP.Size = new System.Drawing.Size(526, 385);
            this.tLP.TabIndex = 5;
            // 
            // btnNo0
            // 
            this.btnNo0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNo0.Location = new System.Drawing.Point(3, 336);
            this.btnNo0.Name = "btnNo0";
            this.btnNo0.Size = new System.Drawing.Size(81, 46);
            this.btnNo0.TabIndex = 4;
            this.btnNo0.Tag = "0";
            this.btnNo0.Text = "NO.0";
            this.btnNo0.UseVisualStyleBackColor = true;
            this.btnNo0.Click += new System.EventHandler(this.btn_NoPage_Click);
            // 
            // btnNo1
            // 
            this.btnNo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNo1.Location = new System.Drawing.Point(90, 336);
            this.btnNo1.Name = "btnNo1";
            this.btnNo1.Size = new System.Drawing.Size(81, 46);
            this.btnNo1.TabIndex = 5;
            this.btnNo1.Tag = "1";
            this.btnNo1.Text = "NO.1";
            this.btnNo1.UseVisualStyleBackColor = true;
            this.btnNo1.Click += new System.EventHandler(this.btn_NoPage_Click);
            // 
            // btnNo2
            // 
            this.btnNo2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNo2.Location = new System.Drawing.Point(177, 336);
            this.btnNo2.Name = "btnNo2";
            this.btnNo2.Size = new System.Drawing.Size(81, 46);
            this.btnNo2.TabIndex = 6;
            this.btnNo2.Tag = "2";
            this.btnNo2.Text = "NO.2";
            this.btnNo2.UseVisualStyleBackColor = true;
            this.btnNo2.Click += new System.EventHandler(this.btn_NoPage_Click);
            // 
            // btnNo3
            // 
            this.btnNo3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNo3.Location = new System.Drawing.Point(264, 336);
            this.btnNo3.Name = "btnNo3";
            this.btnNo3.Size = new System.Drawing.Size(81, 46);
            this.btnNo3.TabIndex = 7;
            this.btnNo3.Tag = "3";
            this.btnNo3.Text = "NO.3";
            this.btnNo3.UseVisualStyleBackColor = true;
            this.btnNo3.Click += new System.EventHandler(this.btn_NoPage_Click);
            // 
            // btnNo4
            // 
            this.btnNo4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNo4.Location = new System.Drawing.Point(351, 336);
            this.btnNo4.Name = "btnNo4";
            this.btnNo4.Size = new System.Drawing.Size(81, 46);
            this.btnNo4.TabIndex = 8;
            this.btnNo4.Tag = "4";
            this.btnNo4.Text = "NO.4";
            this.btnNo4.UseVisualStyleBackColor = true;
            this.btnNo4.Click += new System.EventHandler(this.btn_NoPage_Click);
            // 
            // btnNo5
            // 
            this.btnNo5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNo5.Location = new System.Drawing.Point(438, 336);
            this.btnNo5.Name = "btnNo5";
            this.btnNo5.Size = new System.Drawing.Size(85, 46);
            this.btnNo5.TabIndex = 9;
            this.btnNo5.Tag = "5";
            this.btnNo5.Text = "NO.5";
            this.btnNo5.UseVisualStyleBackColor = true;
            this.btnNo5.Click += new System.EventHandler(this.btn_NoPage_Click);
            // 
            // UserControlInOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLP);
            this.Name = "UserControlInOut";
            this.Size = new System.Drawing.Size(526, 385);
            this.Load += new System.EventHandler(this.UserControlInOut_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).EndInit();
            this.tLP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.TableLayoutPanel tLP;
        private System.Windows.Forms.Button btnNo0;
        private System.Windows.Forms.Button btnNo1;
        private System.Windows.Forms.Button btnNo2;
        private System.Windows.Forms.Button btnNo3;
        private System.Windows.Forms.Button btnNo4;
        private System.Windows.Forms.Button btnNo5;
    }
}
