namespace IPMessagerNet.UI.Controls
{
	partial class ChatArea
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatArea));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tChat = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tBlock = new System.Windows.Forms.ToolStripButton();
			this.tDialup = new System.Windows.Forms.ToolStripButton();
			this.tModifyMemo = new System.Windows.Forms.ToolStripButton();
			this.chatContainer = new System.Windows.Forms.SplitContainer();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.tSendFileBtn = new System.Windows.Forms.ToolStripSplitButton();
			this.tSendFolder = new System.Windows.Forms.ToolStripMenuItem();
			this.chatPage = new IPMessagerNet.UI.Controls.Chat.ChatContainer();
			this.tSendFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1.SuspendLayout();
			this.chatContainer.Panel1.SuspendLayout();
			this.chatContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tChat,
            this.tSendFileBtn,
            this.toolStripSeparator1,
            this.tBlock,
            this.tDialup,
            this.tModifyMemo});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(456, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tChat
			// 
			this.tChat.Image = ((System.Drawing.Image)(resources.GetObject("tChat.Image")));
			this.tChat.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tChat.Name = "tChat";
			this.tChat.Size = new System.Drawing.Size(79, 22);
			this.tChat.Text = "和TA聊天";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tBlock
			// 
			this.tBlock.Image = ((System.Drawing.Image)(resources.GetObject("tBlock.Image")));
			this.tBlock.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tBlock.Name = "tBlock";
			this.tBlock.Size = new System.Drawing.Size(76, 22);
			this.tBlock.Text = "拖黑名单";
			// 
			// tDialup
			// 
			this.tDialup.Image = ((System.Drawing.Image)(resources.GetObject("tDialup.Image")));
			this.tDialup.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tDialup.Name = "tDialup";
			this.tDialup.Size = new System.Drawing.Size(76, 22);
			this.tDialup.Text = "加入拨号";
			// 
			// tModifyMemo
			// 
			this.tModifyMemo.Image = ((System.Drawing.Image)(resources.GetObject("tModifyMemo.Image")));
			this.tModifyMemo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tModifyMemo.Name = "tModifyMemo";
			this.tModifyMemo.Size = new System.Drawing.Size(76, 22);
			this.tModifyMemo.Text = "修改备注";
			// 
			// chatContainer
			// 
			this.chatContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatContainer.Location = new System.Drawing.Point(0, 25);
			this.chatContainer.Name = "chatContainer";
			this.chatContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// chatContainer.Panel1
			// 
			this.chatContainer.Panel1.Controls.Add(this.chatPage);
			this.chatContainer.Size = new System.Drawing.Size(456, 393);
			this.chatContainer.SplitterDistance = 300;
			this.chatContainer.TabIndex = 1;
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(13, 22);
			this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
			// 
			// tSendFileBtn
			// 
			this.tSendFileBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSendFile,
            this.tSendFolder});
			this.tSendFileBtn.Image = ((System.Drawing.Image)(resources.GetObject("tSendFileBtn.Image")));
			this.tSendFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSendFileBtn.Name = "tSendFileBtn";
			this.tSendFileBtn.Size = new System.Drawing.Size(88, 22);
			this.tSendFileBtn.Text = "发送文件";
			// 
			// tSendFolder
			// 
			this.tSendFolder.Name = "tSendFolder";
			this.tSendFolder.Size = new System.Drawing.Size(152, 22);
			this.tSendFolder.Text = "发送文件夹";
			// 
			// chatPage
			// 
			this.chatPage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatPage.Location = new System.Drawing.Point(0, 0);
			this.chatPage.Name = "chatPage";
			this.chatPage.SelectedIndex = 0;
			this.chatPage.Size = new System.Drawing.Size(456, 300);
			this.chatPage.TabIndex = 3;
			// 
			// tSendFile
			// 
			this.tSendFile.Name = "tSendFile";
			this.tSendFile.Size = new System.Drawing.Size(152, 22);
			this.tSendFile.Text = "发送文件";
			// 
			// ChatArea
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.chatContainer);
			this.Controls.Add(this.toolStrip1);
			this.Name = "ChatArea";
			this.Size = new System.Drawing.Size(456, 418);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.chatContainer.Panel1.ResumeLayout(false);
			this.chatContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tChat;
		private System.Windows.Forms.SplitContainer chatContainer;
		private Chat.ChatContainer chatPage;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tBlock;
		private System.Windows.Forms.ToolStripButton tDialup;
		private System.Windows.Forms.ToolStripButton tModifyMemo;
		private System.Windows.Forms.ToolStripSplitButton tSendFileBtn;
		private System.Windows.Forms.ToolStripMenuItem tSendFolder;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem tSendFile;
	}
}
