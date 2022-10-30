
namespace PmacIO
{
    partial class UserControlRate
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
            this.gBrate = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tB2 = new System.Windows.Forms.TextBox();
            this.tB1 = new System.Windows.Forms.TextBox();
            this.buttonPer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tB3 = new System.Windows.Forms.TextBox();
            this.tB4 = new System.Windows.Forms.TextBox();
            this.buttonValue = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tB5 = new System.Windows.Forms.TextBox();
            this.tB6 = new System.Windows.Forms.TextBox();
            this.buttonNumP = new System.Windows.Forms.Button();
            this.buttonNumN = new System.Windows.Forms.Button();
            this.gBrate.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBrate
            // 
            this.gBrate.Controls.Add(this.buttonNumN);
            this.gBrate.Controls.Add(this.label5);
            this.gBrate.Controls.Add(this.label6);
            this.gBrate.Controls.Add(this.tB5);
            this.gBrate.Controls.Add(this.tB6);
            this.gBrate.Controls.Add(this.buttonNumP);
            this.gBrate.Controls.Add(this.label3);
            this.gBrate.Controls.Add(this.label4);
            this.gBrate.Controls.Add(this.tB3);
            this.gBrate.Controls.Add(this.tB4);
            this.gBrate.Controls.Add(this.buttonValue);
            this.gBrate.Controls.Add(this.label2);
            this.gBrate.Controls.Add(this.label1);
            this.gBrate.Controls.Add(this.tB2);
            this.gBrate.Controls.Add(this.tB1);
            this.gBrate.Controls.Add(this.buttonPer);
            this.gBrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gBrate.Location = new System.Drawing.Point(0, 0);
            this.gBrate.Name = "gBrate";
            this.gBrate.Size = new System.Drawing.Size(221, 385);
            this.gBrate.TabIndex = 3;
            this.gBrate.TabStop = false;
            this.gBrate.Text = "비율계산";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "전체값:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "일부값: ";
            // 
            // tB2
            // 
            this.tB2.Location = new System.Drawing.Point(73, 15);
            this.tB2.Name = "tB2";
            this.tB2.Size = new System.Drawing.Size(144, 21);
            this.tB2.TabIndex = 2;
            // 
            // tB1
            // 
            this.tB1.Location = new System.Drawing.Point(73, 37);
            this.tB1.Name = "tB1";
            this.tB1.Size = new System.Drawing.Size(144, 21);
            this.tB1.TabIndex = 1;
            // 
            // buttonPer
            // 
            this.buttonPer.Location = new System.Drawing.Point(13, 62);
            this.buttonPer.Name = "buttonPer";
            this.buttonPer.Size = new System.Drawing.Size(205, 27);
            this.buttonPer.TabIndex = 0;
            this.buttonPer.Text = "전체값에서 일부값 몇 퍼센트";
            this.buttonPer.UseVisualStyleBackColor = true;
            this.buttonPer.Click += new System.EventHandler(this.buttonPer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "전체값:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "퍼센트값: ";
            // 
            // tB3
            // 
            this.tB3.Location = new System.Drawing.Point(69, 102);
            this.tB3.Name = "tB3";
            this.tB3.Size = new System.Drawing.Size(144, 21);
            this.tB3.TabIndex = 7;
            // 
            // tB4
            // 
            this.tB4.Location = new System.Drawing.Point(69, 126);
            this.tB4.Name = "tB4";
            this.tB4.Size = new System.Drawing.Size(144, 21);
            this.tB4.TabIndex = 6;
            // 
            // buttonValue
            // 
            this.buttonValue.Location = new System.Drawing.Point(9, 151);
            this.buttonValue.Name = "buttonValue";
            this.buttonValue.Size = new System.Drawing.Size(205, 27);
            this.buttonValue.TabIndex = 5;
            this.buttonValue.Text = "전체값의 몇 퍼센트는 얼마";
            this.buttonValue.UseVisualStyleBackColor = true;
            this.buttonValue.Click += new System.EventHandler(this.buttonValue_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "숫자값:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "퍼센트값: ";
            // 
            // tB5
            // 
            this.tB5.Location = new System.Drawing.Point(69, 192);
            this.tB5.Name = "tB5";
            this.tB5.Size = new System.Drawing.Size(144, 21);
            this.tB5.TabIndex = 12;
            // 
            // tB6
            // 
            this.tB6.Location = new System.Drawing.Point(69, 216);
            this.tB6.Name = "tB6";
            this.tB6.Size = new System.Drawing.Size(144, 21);
            this.tB6.TabIndex = 11;
            // 
            // buttonNumP
            // 
            this.buttonNumP.Location = new System.Drawing.Point(9, 241);
            this.buttonNumP.Name = "buttonNumP";
            this.buttonNumP.Size = new System.Drawing.Size(205, 27);
            this.buttonNumP.TabIndex = 10;
            this.buttonNumP.Text = "숫자를 몇 퍼센트 증가";
            this.buttonNumP.UseVisualStyleBackColor = true;
            this.buttonNumP.Click += new System.EventHandler(this.buttonNumP_Click);
            // 
            // buttonNumN
            // 
            this.buttonNumN.Location = new System.Drawing.Point(9, 274);
            this.buttonNumN.Name = "buttonNumN";
            this.buttonNumN.Size = new System.Drawing.Size(205, 27);
            this.buttonNumN.TabIndex = 15;
            this.buttonNumN.Text = "숫자를 몇 퍼센트 감소";
            this.buttonNumN.UseVisualStyleBackColor = true;
            this.buttonNumN.Click += new System.EventHandler(this.buttonNumN_Click);
            // 
            // UserControlRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gBrate);
            this.Name = "UserControlRate";
            this.Size = new System.Drawing.Size(221, 385);
            this.gBrate.ResumeLayout(false);
            this.gBrate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gBrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB2;
        private System.Windows.Forms.TextBox tB1;
        private System.Windows.Forms.Button buttonPer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tB3;
        private System.Windows.Forms.TextBox tB4;
        private System.Windows.Forms.Button buttonValue;
        private System.Windows.Forms.Button buttonNumN;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tB5;
        private System.Windows.Forms.TextBox tB6;
        private System.Windows.Forms.Button buttonNumP;
    }
}
