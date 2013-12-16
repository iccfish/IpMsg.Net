namespace IPMessagerNet.UI.Controls.Config
{
	partial class SecurityConfigPanel
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
			this.chkBlackList = new System.Windows.Forms.CheckBox();
			this.chkEncrypt = new System.Windows.Forms.CheckBox();
			this.chkFilter = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtFilterWords = new System.Windows.Forms.TextBox();
			this.gpBL = new System.Windows.Forms.GroupBox();
			this.lnkBLRemove = new System.Windows.Forms.LinkLabel();
			this.lnkBLAdd = new System.Windows.Forms.LinkLabel();
			this.lstBL = new System.Windows.Forms.ListBox();
			this.groupBox1.SuspendLayout();
			this.gpBL.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkBlackList
			// 
			this.chkBlackList.AutoSize = true;
			this.chkBlackList.Location = new System.Drawing.Point(16, 17);
			this.chkBlackList.Name = "chkBlackList";
			this.chkBlackList.Size = new System.Drawing.Size(216, 16);
			this.chkBlackList.TabIndex = 0;
			this.chkBlackList.Text = "启用黑名单功能（需要黑名单插件）";
			this.chkBlackList.UseVisualStyleBackColor = true;
			// 
			// chkEncrypt
			// 
			this.chkEncrypt.AutoSize = true;
			this.chkEncrypt.Location = new System.Drawing.Point(16, 39);
			this.chkEncrypt.Name = "chkEncrypt";
			this.chkEncrypt.Size = new System.Drawing.Size(396, 16);
			this.chkEncrypt.TabIndex = 0;
			this.chkEncrypt.Text = "启用RSA加密通讯（需要RSA加密插件，需要重新启动才能让更改有效）";
			this.chkEncrypt.UseVisualStyleBackColor = true;
			// 
			// chkFilter
			// 
			this.chkFilter.AutoSize = true;
			this.chkFilter.Location = new System.Drawing.Point(16, 61);
			this.chkFilter.Name = "chkFilter";
			this.chkFilter.Size = new System.Drawing.Size(240, 16);
			this.chkFilter.TabIndex = 0;
			this.chkFilter.Text = "启用关键字过滤（需要关键字过滤插件）";
			this.chkFilter.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtFilterWords);
			this.groupBox1.Location = new System.Drawing.Point(16, 83);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(518, 113);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "需要过滤的关键字";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 91);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(203, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "每个关键字之间以英文逗号“,”隔开";
			// 
			// txtFilterWords
			// 
			this.txtFilterWords.Location = new System.Drawing.Point(12, 28);
			this.txtFilterWords.Multiline = true;
			this.txtFilterWords.Name = "txtFilterWords";
			this.txtFilterWords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFilterWords.Size = new System.Drawing.Size(500, 60);
			this.txtFilterWords.TabIndex = 0;
			// 
			// gpBL
			// 
			this.gpBL.Controls.Add(this.lnkBLRemove);
			this.gpBL.Controls.Add(this.lnkBLAdd);
			this.gpBL.Controls.Add(this.lstBL);
			this.gpBL.Location = new System.Drawing.Point(18, 213);
			this.gpBL.Name = "gpBL";
			this.gpBL.Size = new System.Drawing.Size(214, 155);
			this.gpBL.TabIndex = 2;
			this.gpBL.TabStop = false;
			this.gpBL.Text = "黑名单设置";
			// 
			// lnkBLRemove
			// 
			this.lnkBLRemove.AutoSize = true;
			this.lnkBLRemove.Location = new System.Drawing.Point(165, 138);
			this.lnkBLRemove.Name = "lnkBLRemove";
			this.lnkBLRemove.Size = new System.Drawing.Size(41, 12);
			this.lnkBLRemove.TabIndex = 7;
			this.lnkBLRemove.TabStop = true;
			this.lnkBLRemove.Text = "- 删除";
			// 
			// lnkBLAdd
			// 
			this.lnkBLAdd.AutoSize = true;
			this.lnkBLAdd.Location = new System.Drawing.Point(118, 138);
			this.lnkBLAdd.Name = "lnkBLAdd";
			this.lnkBLAdd.Size = new System.Drawing.Size(41, 12);
			this.lnkBLAdd.TabIndex = 8;
			this.lnkBLAdd.TabStop = true;
			this.lnkBLAdd.Text = "+ 添加";
			// 
			// lstBL
			// 
			this.lstBL.FormattingEnabled = true;
			this.lstBL.ItemHeight = 12;
			this.lstBL.Location = new System.Drawing.Point(6, 20);
			this.lstBL.Name = "lstBL";
			this.lstBL.Size = new System.Drawing.Size(202, 112);
			this.lstBL.TabIndex = 6;
			// 
			// SecurityConfigPanel
			// 
			this.Controls.Add(this.gpBL);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.chkFilter);
			this.Controls.Add(this.chkEncrypt);
			this.Controls.Add(this.chkBlackList);
			this.Name = "SecurityConfigPanel";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.gpBL.ResumeLayout(false);
			this.gpBL.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkBlackList;
		private System.Windows.Forms.CheckBox chkEncrypt;
		private System.Windows.Forms.CheckBox chkFilter;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFilterWords;
		private System.Windows.Forms.GroupBox gpBL;
		private System.Windows.Forms.LinkLabel lnkBLRemove;
		private System.Windows.Forms.LinkLabel lnkBLAdd;
		private System.Windows.Forms.ListBox lstBL;
	}
}
