namespace IPMessagerNet.UI.Controls.Config
{
	partial class AudioConfigPanel
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
			this.chkHostOnline = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.gbEvents = new System.Windows.Forms.GroupBox();
			this.chkFileError = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.chkFileSucc = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.chkNewMsg = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.chkHostOffline = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.chkNewFile = new IPMessagerNet.UI.EditorControls.CheckBoxEditor();
			this.gbEvents.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkHostOnline
			// 
			this.chkHostOnline.AutoSize = true;
			this.chkHostOnline.DataInstance = null;
			this.chkHostOnline.DataMemberName = "EnableOnlineSound";
			this.chkHostOnline.Location = new System.Drawing.Point(21, 35);
			this.chkHostOnline.Name = "chkHostOnline";
			this.chkHostOnline.Size = new System.Drawing.Size(72, 16);
			this.chkHostOnline.TabIndex = 0;
			this.chkHostOnline.Text = "主机上线";
			this.chkHostOnline.UseVisualStyleBackColor = true;
			// 
			// gbEvents
			// 
			this.gbEvents.Controls.Add(this.chkFileError);
			this.gbEvents.Controls.Add(this.chkFileSucc);
			this.gbEvents.Controls.Add(this.chkNewMsg);
			this.gbEvents.Controls.Add(this.chkHostOffline);
			this.gbEvents.Controls.Add(this.chkNewFile);
			this.gbEvents.Controls.Add(this.chkHostOnline);
			this.gbEvents.Location = new System.Drawing.Point(28, 19);
			this.gbEvents.Name = "gbEvents";
			this.gbEvents.Size = new System.Drawing.Size(503, 112);
			this.gbEvents.TabIndex = 1;
			this.gbEvents.TabStop = false;
			this.gbEvents.Text = "事件声音";
			// 
			// chkFileError
			// 
			this.chkFileError.AutoSize = true;
			this.chkFileError.DataInstance = null;
			this.chkFileError.DataMemberName = "EnableFileErrorSound";
			this.chkFileError.Location = new System.Drawing.Point(383, 66);
			this.chkFileError.Name = "chkFileError";
			this.chkFileError.Size = new System.Drawing.Size(96, 16);
			this.chkFileError.TabIndex = 0;
			this.chkFileError.Text = "文件接收失败";
			this.chkFileError.UseVisualStyleBackColor = true;
			// 
			// chkFileSucc
			// 
			this.chkFileSucc.AutoSize = true;
			this.chkFileSucc.DataInstance = null;
			this.chkFileSucc.DataMemberName = "EnableFileSuccSound";
			this.chkFileSucc.Location = new System.Drawing.Point(257, 66);
			this.chkFileSucc.Name = "chkFileSucc";
			this.chkFileSucc.Size = new System.Drawing.Size(96, 16);
			this.chkFileSucc.TabIndex = 0;
			this.chkFileSucc.Text = "文件接收成功";
			this.chkFileSucc.UseVisualStyleBackColor = true;
			// 
			// chkNewMsg
			// 
			this.chkNewMsg.AutoSize = true;
			this.chkNewMsg.DataInstance = null;
			this.chkNewMsg.DataMemberName = "EnableNewMsgSound";
			this.chkNewMsg.Location = new System.Drawing.Point(21, 66);
			this.chkNewMsg.Name = "chkNewMsg";
			this.chkNewMsg.Size = new System.Drawing.Size(84, 16);
			this.chkNewMsg.TabIndex = 0;
			this.chkNewMsg.Text = "收到新信息";
			this.chkNewMsg.UseVisualStyleBackColor = true;
			// 
			// chkHostOffline
			// 
			this.chkHostOffline.AutoSize = true;
			this.chkHostOffline.DataInstance = null;
			this.chkHostOffline.DataMemberName = "EnableOfflineSound";
			this.chkHostOffline.Location = new System.Drawing.Point(143, 35);
			this.chkHostOffline.Name = "chkHostOffline";
			this.chkHostOffline.Size = new System.Drawing.Size(72, 16);
			this.chkHostOffline.TabIndex = 0;
			this.chkHostOffline.Text = "主机下线";
			this.chkHostOffline.UseVisualStyleBackColor = true;
			// 
			// chkNewFile
			// 
			this.chkNewFile.AutoSize = true;
			this.chkNewFile.DataInstance = null;
			this.chkNewFile.DataMemberName = "EnableNewFileSound";
			this.chkNewFile.Location = new System.Drawing.Point(143, 66);
			this.chkNewFile.Name = "chkNewFile";
			this.chkNewFile.Size = new System.Drawing.Size(84, 16);
			this.chkNewFile.TabIndex = 0;
			this.chkNewFile.Text = "收到新文件";
			this.chkNewFile.UseVisualStyleBackColor = true;
			// 
			// AudioConfigPanel
			// 
			this.Controls.Add(this.gbEvents);
			this.Name = "AudioConfigPanel";
			this.Load += new System.EventHandler(this.AudioConfigPanel_Load);
			this.gbEvents.ResumeLayout(false);
			this.gbEvents.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkHostOnline;
		private System.Windows.Forms.GroupBox gbEvents;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkFileError;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkFileSucc;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkNewMsg;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkHostOffline;
		private IPMessagerNet.UI.EditorControls.CheckBoxEditor chkNewFile;
	}
}
