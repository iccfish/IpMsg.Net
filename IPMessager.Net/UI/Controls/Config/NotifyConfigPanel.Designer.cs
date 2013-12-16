namespace IPMessagerNet.UI.Controls.Config
{
	partial class NotifyConfigPanel
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnOTSpec = new System.Windows.Forms.Button();
			this.rbOTSpec = new System.Windows.Forms.RadioButton();
			this.rbOTAll = new System.Windows.Forms.RadioButton();
			this.rbOTNone = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnFTSpec = new System.Windows.Forms.Button();
			this.rbFTSpec = new System.Windows.Forms.RadioButton();
			this.rbFTAll = new System.Windows.Forms.RadioButton();
			this.rbFTNone = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.chkDOTInQuite = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.chkAutoChange = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnOTSpec);
			this.groupBox1.Controls.Add(this.rbOTSpec);
			this.groupBox1.Controls.Add(this.rbOTAll);
			this.groupBox1.Controls.Add(this.rbOTNone);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(261, 152);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "上线提示";
			// 
			// btnOTSpec
			// 
			this.btnOTSpec.Location = new System.Drawing.Point(167, 122);
			this.btnOTSpec.Name = "btnOTSpec";
			this.btnOTSpec.Size = new System.Drawing.Size(88, 24);
			this.btnOTSpec.TabIndex = 2;
			this.btnOTSpec.Text = "设置主机...";
			this.btnOTSpec.UseVisualStyleBackColor = true;
			this.btnOTSpec.Click += new System.EventHandler(this.btnOTSpec_Click);
			// 
			// rbOTSpec
			// 
			this.rbOTSpec.AutoSize = true;
			this.rbOTSpec.Location = new System.Drawing.Point(8, 126);
			this.rbOTSpec.Name = "rbOTSpec";
			this.rbOTSpec.Size = new System.Drawing.Size(155, 16);
			this.rbOTSpec.TabIndex = 1;
			this.rbOTSpec.TabStop = true;
			this.rbOTSpec.Text = "对我选定的主机进行提示";
			this.rbOTSpec.UseVisualStyleBackColor = true;
			// 
			// rbOTAll
			// 
			this.rbOTAll.AutoSize = true;
			this.rbOTAll.Location = new System.Drawing.Point(8, 104);
			this.rbOTAll.Name = "rbOTAll";
			this.rbOTAll.Size = new System.Drawing.Size(71, 16);
			this.rbOTAll.TabIndex = 1;
			this.rbOTAll.TabStop = true;
			this.rbOTAll.Text = "全部提示";
			this.rbOTAll.UseVisualStyleBackColor = true;
			// 
			// rbOTNone
			// 
			this.rbOTNone.AutoSize = true;
			this.rbOTNone.Location = new System.Drawing.Point(8, 82);
			this.rbOTNone.Name = "rbOTNone";
			this.rbOTNone.Size = new System.Drawing.Size(83, 16);
			this.rbOTNone.TabIndex = 1;
			this.rbOTNone.TabStop = true;
			this.rbOTNone.Text = "全部不提示";
			this.rbOTNone.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(249, 51);
			this.label1.TabIndex = 0;
			this.label1.Text = "上线提示功能是当有新的主机上线时，在托盘区显示气泡提示。\r\n你可以在这里设置自己需要的提示方式。";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnFTSpec);
			this.groupBox2.Controls.Add(this.rbFTSpec);
			this.groupBox2.Controls.Add(this.rbFTAll);
			this.groupBox2.Controls.Add(this.rbFTNone);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(270, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(261, 152);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "下线提示";
			// 
			// btnFTSpec
			// 
			this.btnFTSpec.Location = new System.Drawing.Point(167, 122);
			this.btnFTSpec.Name = "btnFTSpec";
			this.btnFTSpec.Size = new System.Drawing.Size(88, 24);
			this.btnFTSpec.TabIndex = 2;
			this.btnFTSpec.Text = "设置主机...";
			this.btnFTSpec.UseVisualStyleBackColor = true;
			this.btnFTSpec.Click += new System.EventHandler(this.btnFTSpec_Click);
			// 
			// rbFTSpec
			// 
			this.rbFTSpec.AutoSize = true;
			this.rbFTSpec.Location = new System.Drawing.Point(8, 126);
			this.rbFTSpec.Name = "rbFTSpec";
			this.rbFTSpec.Size = new System.Drawing.Size(155, 16);
			this.rbFTSpec.TabIndex = 1;
			this.rbFTSpec.TabStop = true;
			this.rbFTSpec.Text = "对我选定的主机进行提示";
			this.rbFTSpec.UseVisualStyleBackColor = true;
			// 
			// rbFTAll
			// 
			this.rbFTAll.AutoSize = true;
			this.rbFTAll.Location = new System.Drawing.Point(8, 104);
			this.rbFTAll.Name = "rbFTAll";
			this.rbFTAll.Size = new System.Drawing.Size(71, 16);
			this.rbFTAll.TabIndex = 1;
			this.rbFTAll.TabStop = true;
			this.rbFTAll.Text = "全部提示";
			this.rbFTAll.UseVisualStyleBackColor = true;
			// 
			// rbFTNone
			// 
			this.rbFTNone.AutoSize = true;
			this.rbFTNone.Location = new System.Drawing.Point(8, 82);
			this.rbFTNone.Name = "rbFTNone";
			this.rbFTNone.Size = new System.Drawing.Size(83, 16);
			this.rbFTNone.TabIndex = 1;
			this.rbFTNone.TabStop = true;
			this.rbFTNone.Text = "全部不提示";
			this.rbFTNone.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(249, 51);
			this.label2.TabIndex = 0;
			this.label2.Text = "下线提示功能是当有主机下线时，在托盘区显示气泡提示。\r\n你可以在这里设置自己需要的提示方式。";
			// 
			// chkDOTInQuite
			// 
			this.chkDOTInQuite.AutoSize = true;
			this.chkDOTInQuite.DataInstance = null;
			this.chkDOTInQuite.DataMemberName = "DisableHostTipInQuite";
			this.chkDOTInQuite.Location = new System.Drawing.Point(11, 175);
			this.chkDOTInQuite.Name = "chkDOTInQuite";
			this.chkDOTInQuite.Size = new System.Drawing.Size(264, 16);
			this.chkDOTInQuite.TabIndex = 1;
			this.chkDOTInQuite.Text = "当我设置静音模式时，禁止所有的上下线提示";
			this.chkDOTInQuite.UseVisualStyleBackColor = true;
			// 
			// chkAutoChange
			// 
			this.chkAutoChange.AutoSize = true;
			this.chkAutoChange.DataInstance = null;
			this.chkAutoChange.DataMemberName = "AutoChangeCurrentTabToNew";
			this.chkAutoChange.Location = new System.Drawing.Point(11, 197);
			this.chkAutoChange.Name = "chkAutoChange";
			this.chkAutoChange.Size = new System.Drawing.Size(312, 16);
			this.chkAutoChange.TabIndex = 2;
			this.chkAutoChange.Text = "当收到新信息时，自动切换当前窗口到有新信息的窗口";
			this.chkAutoChange.UseVisualStyleBackColor = true;
			// 
			// NotifyConfigPanel
			// 
			this.Controls.Add(this.chkAutoChange);
			this.Controls.Add(this.chkDOTInQuite);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "NotifyConfigPanel";
			this.Load += new System.EventHandler(this.NotifyConfigPanel_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOTSpec;
		private System.Windows.Forms.RadioButton rbOTSpec;
		private System.Windows.Forms.RadioButton rbOTAll;
		private System.Windows.Forms.RadioButton rbOTNone;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnFTSpec;
		private System.Windows.Forms.RadioButton rbFTSpec;
		private System.Windows.Forms.RadioButton rbFTAll;
		private System.Windows.Forms.RadioButton rbFTNone;
		private System.Windows.Forms.Label label2;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkDOTInQuite;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkAutoChange;
	}
}
