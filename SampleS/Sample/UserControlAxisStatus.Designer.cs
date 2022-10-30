
namespace PmacIO
{
    partial class UserControlAxisStatus
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
            this.dGV = new System.Windows.Forms.DataGridView();
            this.motInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.axisNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.axisNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actPosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vel0DataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.plusLimitDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.minusLimitDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.homeSensorDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ampEnableDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.homeCompleteDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ampFaultDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.inPositionDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isActivatedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmdSpeedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actSpeedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdPosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destPosDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openLoopDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isHomeMoveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.motInfoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dGV
            // 
            this.dGV.AutoGenerateColumns = false;
            this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.axisNameDataGridViewTextBoxColumn,
            this.axisNoDataGridViewTextBoxColumn,
            this.actPosDataGridViewTextBoxColumn,
            this.vel0DataGridViewCheckBoxColumn,
            this.plusLimitDataGridViewCheckBoxColumn,
            this.minusLimitDataGridViewCheckBoxColumn,
            this.homeSensorDataGridViewCheckBoxColumn,
            this.ampEnableDataGridViewCheckBoxColumn,
            this.homeCompleteDataGridViewCheckBoxColumn,
            this.ampFaultDataGridViewCheckBoxColumn,
            this.inPositionDataGridViewCheckBoxColumn,
            this.isActivatedDataGridViewCheckBoxColumn,
            this.cmdSpeedDataGridViewTextBoxColumn,
            this.actSpeedDataGridViewTextBoxColumn,
            this.cmdPosDataGridViewTextBoxColumn,
            this.destPosDataGridViewTextBoxColumn,
            this.openLoopDataGridViewCheckBoxColumn,
            this.isHomeMoveDataGridViewCheckBoxColumn});
            this.dGV.DataSource = this.motInfoBindingSource;
            this.dGV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dGV.Location = new System.Drawing.Point(0, 0);
            this.dGV.Name = "dGV";
            this.dGV.RowTemplate.Height = 23;
            this.dGV.Size = new System.Drawing.Size(471, 356);
            this.dGV.TabIndex = 138;
            // 
            // motInfoBindingSource
            // 
            this.motInfoBindingSource.DataSource = typeof(PmacIO.MotInfo);
            // 
            // axisNameDataGridViewTextBoxColumn
            // 
            this.axisNameDataGridViewTextBoxColumn.DataPropertyName = "AxisName";
            this.axisNameDataGridViewTextBoxColumn.HeaderText = "AxisName";
            this.axisNameDataGridViewTextBoxColumn.Name = "axisNameDataGridViewTextBoxColumn";
            // 
            // axisNoDataGridViewTextBoxColumn
            // 
            this.axisNoDataGridViewTextBoxColumn.DataPropertyName = "AxisNo";
            this.axisNoDataGridViewTextBoxColumn.HeaderText = "AxisNo";
            this.axisNoDataGridViewTextBoxColumn.Name = "axisNoDataGridViewTextBoxColumn";
            this.axisNoDataGridViewTextBoxColumn.Width = 50;
            // 
            // actPosDataGridViewTextBoxColumn
            // 
            this.actPosDataGridViewTextBoxColumn.DataPropertyName = "ActPos";
            this.actPosDataGridViewTextBoxColumn.HeaderText = "ActPos";
            this.actPosDataGridViewTextBoxColumn.Name = "actPosDataGridViewTextBoxColumn";
            this.actPosDataGridViewTextBoxColumn.Width = 120;
            // 
            // vel0DataGridViewCheckBoxColumn
            // 
            this.vel0DataGridViewCheckBoxColumn.DataPropertyName = "Vel0";
            this.vel0DataGridViewCheckBoxColumn.HeaderText = "Vel0";
            this.vel0DataGridViewCheckBoxColumn.Name = "vel0DataGridViewCheckBoxColumn";
            this.vel0DataGridViewCheckBoxColumn.Width = 40;
            // 
            // plusLimitDataGridViewCheckBoxColumn
            // 
            this.plusLimitDataGridViewCheckBoxColumn.DataPropertyName = "PlusLimit";
            this.plusLimitDataGridViewCheckBoxColumn.HeaderText = "PlusLimit";
            this.plusLimitDataGridViewCheckBoxColumn.Name = "plusLimitDataGridViewCheckBoxColumn";
            this.plusLimitDataGridViewCheckBoxColumn.Width = 40;
            // 
            // minusLimitDataGridViewCheckBoxColumn
            // 
            this.minusLimitDataGridViewCheckBoxColumn.DataPropertyName = "MinusLimit";
            this.minusLimitDataGridViewCheckBoxColumn.HeaderText = "MinusLimit";
            this.minusLimitDataGridViewCheckBoxColumn.Name = "minusLimitDataGridViewCheckBoxColumn";
            this.minusLimitDataGridViewCheckBoxColumn.Width = 40;
            // 
            // homeSensorDataGridViewCheckBoxColumn
            // 
            this.homeSensorDataGridViewCheckBoxColumn.DataPropertyName = "HomeSensor";
            this.homeSensorDataGridViewCheckBoxColumn.HeaderText = "HomeSensor";
            this.homeSensorDataGridViewCheckBoxColumn.Name = "homeSensorDataGridViewCheckBoxColumn";
            this.homeSensorDataGridViewCheckBoxColumn.Width = 40;
            // 
            // ampEnableDataGridViewCheckBoxColumn
            // 
            this.ampEnableDataGridViewCheckBoxColumn.DataPropertyName = "AmpEnable";
            this.ampEnableDataGridViewCheckBoxColumn.HeaderText = "AmpEnable";
            this.ampEnableDataGridViewCheckBoxColumn.Name = "ampEnableDataGridViewCheckBoxColumn";
            this.ampEnableDataGridViewCheckBoxColumn.Width = 40;
            // 
            // homeCompleteDataGridViewCheckBoxColumn
            // 
            this.homeCompleteDataGridViewCheckBoxColumn.DataPropertyName = "HomeComplete";
            this.homeCompleteDataGridViewCheckBoxColumn.HeaderText = "HomeComplete";
            this.homeCompleteDataGridViewCheckBoxColumn.Name = "homeCompleteDataGridViewCheckBoxColumn";
            this.homeCompleteDataGridViewCheckBoxColumn.Width = 40;
            // 
            // ampFaultDataGridViewCheckBoxColumn
            // 
            this.ampFaultDataGridViewCheckBoxColumn.DataPropertyName = "AmpFault";
            this.ampFaultDataGridViewCheckBoxColumn.HeaderText = "AmpFault";
            this.ampFaultDataGridViewCheckBoxColumn.Name = "ampFaultDataGridViewCheckBoxColumn";
            // 
            // inPositionDataGridViewCheckBoxColumn
            // 
            this.inPositionDataGridViewCheckBoxColumn.DataPropertyName = "InPosition";
            this.inPositionDataGridViewCheckBoxColumn.HeaderText = "InPosition";
            this.inPositionDataGridViewCheckBoxColumn.Name = "inPositionDataGridViewCheckBoxColumn";
            // 
            // isActivatedDataGridViewCheckBoxColumn
            // 
            this.isActivatedDataGridViewCheckBoxColumn.DataPropertyName = "IsActivated";
            this.isActivatedDataGridViewCheckBoxColumn.HeaderText = "IsActivated";
            this.isActivatedDataGridViewCheckBoxColumn.Name = "isActivatedDataGridViewCheckBoxColumn";
            // 
            // cmdSpeedDataGridViewTextBoxColumn
            // 
            this.cmdSpeedDataGridViewTextBoxColumn.DataPropertyName = "CmdSpeed";
            this.cmdSpeedDataGridViewTextBoxColumn.HeaderText = "CmdSpeed";
            this.cmdSpeedDataGridViewTextBoxColumn.Name = "cmdSpeedDataGridViewTextBoxColumn";
            // 
            // actSpeedDataGridViewTextBoxColumn
            // 
            this.actSpeedDataGridViewTextBoxColumn.DataPropertyName = "ActSpeed";
            this.actSpeedDataGridViewTextBoxColumn.HeaderText = "ActSpeed";
            this.actSpeedDataGridViewTextBoxColumn.Name = "actSpeedDataGridViewTextBoxColumn";
            // 
            // cmdPosDataGridViewTextBoxColumn
            // 
            this.cmdPosDataGridViewTextBoxColumn.DataPropertyName = "CmdPos";
            this.cmdPosDataGridViewTextBoxColumn.HeaderText = "CmdPos";
            this.cmdPosDataGridViewTextBoxColumn.Name = "cmdPosDataGridViewTextBoxColumn";
            // 
            // destPosDataGridViewTextBoxColumn
            // 
            this.destPosDataGridViewTextBoxColumn.DataPropertyName = "DestPos";
            this.destPosDataGridViewTextBoxColumn.HeaderText = "DestPos";
            this.destPosDataGridViewTextBoxColumn.Name = "destPosDataGridViewTextBoxColumn";
            // 
            // openLoopDataGridViewCheckBoxColumn
            // 
            this.openLoopDataGridViewCheckBoxColumn.DataPropertyName = "OpenLoop";
            this.openLoopDataGridViewCheckBoxColumn.HeaderText = "OpenLoop";
            this.openLoopDataGridViewCheckBoxColumn.Name = "openLoopDataGridViewCheckBoxColumn";
            // 
            // isHomeMoveDataGridViewCheckBoxColumn
            // 
            this.isHomeMoveDataGridViewCheckBoxColumn.DataPropertyName = "IsHomeMove";
            this.isHomeMoveDataGridViewCheckBoxColumn.HeaderText = "IsHomeMove";
            this.isHomeMoveDataGridViewCheckBoxColumn.Name = "isHomeMoveDataGridViewCheckBoxColumn";
            // 
            // UserControlAxisStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dGV);
            this.Name = "UserControlAxisStatus";
            this.Size = new System.Drawing.Size(471, 356);
            this.Load += new System.EventHandler(this.UserControlAxisStatus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.motInfoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dGV;
        private System.Windows.Forms.BindingSource motInfoBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn axisNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actPosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn vel0DataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn plusLimitDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn minusLimitDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn homeSensorDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ampEnableDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn homeCompleteDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ampFaultDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn inPositionDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActivatedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdSpeedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn actSpeedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cmdPosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn destPosDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn openLoopDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isHomeMoveDataGridViewCheckBoxColumn;
    }
}
