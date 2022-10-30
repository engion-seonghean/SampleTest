
namespace PmacIO
{
    partial class ucRecipeShow
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbCurRecipe = new System.Windows.Forms.Label();
            this.btnLoadRecipe = new System.Windows.Forms.Button();
            this.btnRecipeSaveAs = new System.Windows.Forms.Button();
            this.btnRecipeSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 53);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(383, 395);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Recipe List Name ";
            this.columnHeader1.Width = 500;
            // 
            // lbCurRecipe
            // 
            this.lbCurRecipe.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbCurRecipe.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbCurRecipe.Location = new System.Drawing.Point(140, 16);
            this.lbCurRecipe.Name = "lbCurRecipe";
            this.lbCurRecipe.Size = new System.Drawing.Size(246, 26);
            this.lbCurRecipe.TabIndex = 31;
            this.lbCurRecipe.Text = "Current Recipe";
            this.lbCurRecipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLoadRecipe
            // 
            this.btnLoadRecipe.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnLoadRecipe.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnLoadRecipe.ForeColor = System.Drawing.Color.White;
            this.btnLoadRecipe.Location = new System.Drawing.Point(411, 344);
            this.btnLoadRecipe.Name = "btnLoadRecipe";
            this.btnLoadRecipe.Size = new System.Drawing.Size(168, 49);
            this.btnLoadRecipe.TabIndex = 29;
            this.btnLoadRecipe.Text = "선택 Recipe 적용";
            this.btnLoadRecipe.UseVisualStyleBackColor = false;
            this.btnLoadRecipe.Click += new System.EventHandler(this.btnLoadRecipe_Click);
            // 
            // btnRecipeSaveAs
            // 
            this.btnRecipeSaveAs.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRecipeSaveAs.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRecipeSaveAs.ForeColor = System.Drawing.Color.White;
            this.btnRecipeSaveAs.Location = new System.Drawing.Point(411, 71);
            this.btnRecipeSaveAs.Name = "btnRecipeSaveAs";
            this.btnRecipeSaveAs.Size = new System.Drawing.Size(168, 49);
            this.btnRecipeSaveAs.TabIndex = 28;
            this.btnRecipeSaveAs.Text = "Save As";
            this.btnRecipeSaveAs.UseVisualStyleBackColor = false;
            // 
            // btnRecipeSave
            // 
            this.btnRecipeSave.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRecipeSave.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnRecipeSave.ForeColor = System.Drawing.Color.White;
            this.btnRecipeSave.Location = new System.Drawing.Point(411, 399);
            this.btnRecipeSave.Name = "btnRecipeSave";
            this.btnRecipeSave.Size = new System.Drawing.Size(168, 49);
            this.btnRecipeSave.TabIndex = 27;
            this.btnRecipeSave.Text = "Save";
            this.btnRecipeSave.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(3, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 26);
            this.label2.TabIndex = 33;
            this.label2.Text = "현재 Recipe명:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnDelete.Font = new System.Drawing.Font("굴림", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(411, 126);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(168, 49);
            this.btnDelete.TabIndex = 34;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ucRecipeShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbCurRecipe);
            this.Controls.Add(this.btnLoadRecipe);
            this.Controls.Add(this.btnRecipeSaveAs);
            this.Controls.Add(this.btnRecipeSave);
            this.Controls.Add(this.listView1);
            this.Name = "ucRecipeShow";
            this.Size = new System.Drawing.Size(624, 463);
            this.Load += new System.EventHandler(this.ucRecipeShow_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label lbCurRecipe;
        private System.Windows.Forms.Button btnLoadRecipe;
        private System.Windows.Forms.Button btnRecipeSaveAs;
        private System.Windows.Forms.Button btnRecipeSave;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDelete;
    }
}
