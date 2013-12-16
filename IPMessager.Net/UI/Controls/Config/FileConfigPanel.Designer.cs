namespace IPMessagerNet.UI.Controls.Config
{
	partial class FileConfigPanel
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
			this.chkFileBpC = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numTaskThreadCount = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.numTaskTimeout = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTaskThreadCount)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numTaskTimeout)).BeginInit();
			this.SuspendLayout();
			// 
			// chkFileBpC
			// 
			this.chkFileBpC.AutoSize = true;
			this.chkFileBpC.Location = new System.Drawing.Point(17, 13);
			this.chkFileBpC.Name = "chkFileBpC";
			this.chkFileBpC.Size = new System.Drawing.Size(480, 16);
			this.chkFileBpC.TabIndex = 0;
			this.chkFileBpC.Text = "开启断点续传功能（需要对方的飞鸽传书支持断点续传，且不支持文件夹的断点续传）";
			this.chkFileBpC.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "文件接收同时接收的任务数";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(173, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "文件发送任务超时时间（分钟）";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numTaskTimeout);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.numTaskThreadCount);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(17, 70);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(519, 90);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "文件传输配置";
			// 
			// numTaskThreadCount
			// 
			this.numTaskThreadCount.Location = new System.Drawing.Point(194, 29);
			this.numTaskThreadCount.Name = "numTaskThreadCount";
			this.numTaskThreadCount.Size = new System.Drawing.Size(72, 21);
			this.numTaskThreadCount.TabIndex = 3;
			this.numTaskThreadCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(280, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(125, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "(默认为1，0为不限制)";
			// 
			// numTaskTimeout
			// 
			this.numTaskTimeout.Location = new System.Drawing.Point(194, 56);
			this.numTaskTimeout.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numTaskTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numTaskTimeout.Name = "numTaskTimeout";
			this.numTaskTimeout.Size = new System.Drawing.Size(72, 21);
			this.numTaskTimeout.TabIndex = 5;
			this.numTaskTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.numTaskTimeout.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(280, 58);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(197, 12);
			this.label4.TabIndex = 4;
			this.label4.Text = "(超时的任务会自动删除，默认为10)";
			// 
			// FileConfigPanel
			// 
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.chkFileBpC);
			this.Name = "FileConfigPanel";
			this.Load += new System.EventHandler(this.FileConfigPanel_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numTaskThreadCount)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numTaskTimeout)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkFileBpC;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown numTaskTimeout;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numTaskThreadCount;
	}
}
