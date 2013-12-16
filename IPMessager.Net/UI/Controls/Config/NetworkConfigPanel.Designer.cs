using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Controls.Config
{
	partial class NetworkConfigPanel
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.horizontalLine1 = new HorizontalLine();
			this.horizontalLine2 = new HorizontalLine();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.nudPort = new System.Windows.Forms.NumericUpDown();
			this.cbBip = new System.Windows.Forms.ComboBox();
			this.lstDialup = new System.Windows.Forms.ListBox();
			this.txtDialIp = new System.Windows.Forms.TextBox();
			this.btnDialAdd = new System.Windows.Forms.Button();
			this.btnDialDelete = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.tcpTimeout = new IPMessagerNet.UI.EditorControls.NumbericUpDownEditor();
			this.chkFilterLocalMsg = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.nudPort)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tcpTimeout)).BeginInit();
			this.SuspendLayout();
			// 
			// horizontalLine1
			// 
			this.horizontalLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine1.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine1.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.horizontalLine1.LineColor = System.Drawing.SystemColors.ControlDark;
			this.horizontalLine1.Location = new System.Drawing.Point(0, 3);
			this.horizontalLine1.Name = "horizontalLine1";
			this.horizontalLine1.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine1.Size = new System.Drawing.Size(550, 16);
			this.horizontalLine1.TabIndex = 0;
			this.horizontalLine1.Text = "常规网络设置";
			this.horizontalLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.horizontalLine1.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// horizontalLine2
			// 
			this.horizontalLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine2.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine2.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine2.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.horizontalLine2.LineColor = System.Drawing.SystemColors.ControlDark;
			this.horizontalLine2.Location = new System.Drawing.Point(0, 129);
			this.horizontalLine2.Name = "horizontalLine2";
			this.horizontalLine2.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine2.Size = new System.Drawing.Size(264, 21);
			this.horizontalLine2.TabIndex = 1;
			this.horizontalLine2.Text = "拨号列表";
			this.horizontalLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine2.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 34);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "绑定IP地址";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(244, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(303, 31);
			this.label2.TabIndex = 2;
			this.label2.Text = "如果您的网卡有多个IP地址，可以在此选择将飞鸽传书绑定到其中一个IP上（此改动需要重启才能生效）";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 3;
			this.label3.Text = "通信端口";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(244, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(233, 24);
			this.label4.TabIndex = 3;
			this.label4.Text = "默认为2425，不同端口的飞鸽传书无法通信\r\n（此改动需要重启才能生效）";
			// 
			// nudPort
			// 
			this.nudPort.Location = new System.Drawing.Point(83, 82);
			this.nudPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudPort.Name = "nudPort";
			this.nudPort.Size = new System.Drawing.Size(140, 21);
			this.nudPort.TabIndex = 4;
			this.nudPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// cbBip
			// 
			this.cbBip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbBip.FormattingEnabled = true;
			this.cbBip.Location = new System.Drawing.Point(83, 31);
			this.cbBip.Name = "cbBip";
			this.cbBip.Size = new System.Drawing.Size(140, 20);
			this.cbBip.TabIndex = 5;
			// 
			// lstDialup
			// 
			this.lstDialup.FormattingEnabled = true;
			this.lstDialup.ItemHeight = 12;
			this.lstDialup.Location = new System.Drawing.Point(5, 156);
			this.lstDialup.Name = "lstDialup";
			this.lstDialup.Size = new System.Drawing.Size(259, 184);
			this.lstDialup.TabIndex = 6;
			// 
			// txtDialIp
			// 
			this.txtDialIp.Location = new System.Drawing.Point(5, 345);
			this.txtDialIp.Name = "txtDialIp";
			this.txtDialIp.Size = new System.Drawing.Size(163, 21);
			this.txtDialIp.TabIndex = 7;
			// 
			// btnDialAdd
			// 
			this.btnDialAdd.Location = new System.Drawing.Point(172, 345);
			this.btnDialAdd.Name = "btnDialAdd";
			this.btnDialAdd.Size = new System.Drawing.Size(42, 22);
			this.btnDialAdd.TabIndex = 8;
			this.btnDialAdd.Text = "添加";
			this.btnDialAdd.UseVisualStyleBackColor = true;
			// 
			// btnDialDelete
			// 
			this.btnDialDelete.Location = new System.Drawing.Point(220, 345);
			this.btnDialDelete.Name = "btnDialDelete";
			this.btnDialDelete.Size = new System.Drawing.Size(42, 22);
			this.btnDialDelete.TabIndex = 8;
			this.btnDialDelete.Text = "删除";
			this.btnDialDelete.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.tcpTimeout);
			this.groupBox1.Controls.Add(this.chkFilterLocalMsg);
			this.groupBox1.Location = new System.Drawing.Point(270, 128);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(277, 238);
			this.groupBox1.TabIndex = 9;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "高级网络选项";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 28);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(113, 12);
			this.label5.TabIndex = 3;
			this.label5.Text = "文件传输连接超时：";
			// 
			// tcpTimeout
			// 
			this.tcpTimeout.DataInstance = null;
			this.tcpTimeout.DataMemberName = "ConnectionTimeout";
			this.tcpTimeout.Location = new System.Drawing.Point(176, 26);
			this.tcpTimeout.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.tcpTimeout.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.tcpTimeout.Name = "tcpTimeout";
			this.tcpTimeout.Size = new System.Drawing.Size(55, 21);
			this.tcpTimeout.TabIndex = 2;
			this.tcpTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.tcpTimeout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// chkFilterLocalMsg
			// 
			this.chkFilterLocalMsg.AutoSize = true;
			this.chkFilterLocalMsg.Location = new System.Drawing.Point(6, 215);
			this.chkFilterLocalMsg.Name = "chkFilterLocalMsg";
			this.chkFilterLocalMsg.Size = new System.Drawing.Size(168, 16);
			this.chkFilterLocalMsg.TabIndex = 1;
			this.chkFilterLocalMsg.Text = "忽略来自本地IP发送的消息";
			this.toolTip1.SetToolTip(this.chkFilterLocalMsg, "默认情况下，飞鸽传书会接收来自自己的消息，因此你能在列表中看到自己。\r\n如果启用此选项，将会忽略这些消息。\r\n\r\n此选项需要安装“本地回发消息过滤插件”。");
			this.chkFilterLocalMsg.UseVisualStyleBackColor = true;
			// 
			// NetworkConfigPanel
			// 
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnDialDelete);
			this.Controls.Add(this.btnDialAdd);
			this.Controls.Add(this.txtDialIp);
			this.Controls.Add(this.lstDialup);
			this.Controls.Add(this.cbBip);
			this.Controls.Add(this.nudPort);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.horizontalLine2);
			this.Controls.Add(this.horizontalLine1);
			this.Name = "NetworkConfigPanel";
			this.Load += new System.EventHandler(this.NetworkConfigPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.nudPort)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tcpTimeout)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private HorizontalLine horizontalLine1;
		private HorizontalLine horizontalLine2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown nudPort;
		private System.Windows.Forms.ComboBox cbBip;
		private System.Windows.Forms.ListBox lstDialup;
		private System.Windows.Forms.TextBox txtDialIp;
		private System.Windows.Forms.Button btnDialAdd;
		private System.Windows.Forms.Button btnDialDelete;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkFilterLocalMsg;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label5;
		private EditorControls.NumbericUpDownEditor tcpTimeout;
	}
}
