namespace IPMessagerNet.UI.Forms
{
	partial class FrameContainer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.sm_System = new System.Windows.Forms.ToolStripMenuItem();
			this.sm_System_Config = new System.Windows.Forms.ToolStripMenuItem();
			this.sm_System_SepConfig = new System.Windows.Forms.ToolStripSeparator();
			this.smSystem_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainContainer = new System.Windows.Forms.SplitContainer();
			this.chat = new IPMessagerNet.UI.Controls.ChatArea();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.menuStrip1.SuspendLayout();
			this.mainContainer.Panel2.SuspendLayout();
			this.mainContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 428);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(806, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sm_System});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(806, 25);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// sm_System
			// 
			this.sm_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sm_System_Config,
            this.sm_System_SepConfig,
            this.smSystem_Exit});
			this.sm_System.Name = "sm_System";
			this.sm_System.Size = new System.Drawing.Size(59, 21);
			this.sm_System.Text = "系统(&S)";
			// 
			// sm_System_Config
			// 
			this.sm_System_Config.Name = "sm_System_Config";
			this.sm_System_Config.Size = new System.Drawing.Size(172, 22);
			this.sm_System_Config.Text = "系统设置";
			// 
			// sm_System_SepConfig
			// 
			this.sm_System_SepConfig.Name = "sm_System_SepConfig";
			this.sm_System_SepConfig.Size = new System.Drawing.Size(169, 6);
			// 
			// smSystem_Exit
			// 
			this.smSystem_Exit.Name = "smSystem_Exit";
			this.smSystem_Exit.Size = new System.Drawing.Size(172, 22);
			this.smSystem_Exit.Text = "退出飞鸽传书.Net";
			// 
			// mainContainer
			// 
			this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainContainer.Location = new System.Drawing.Point(0, 25);
			this.mainContainer.Name = "mainContainer";
			// 
			// mainContainer.Panel2
			// 
			this.mainContainer.Panel2.Controls.Add(this.chat);
			this.mainContainer.Size = new System.Drawing.Size(806, 403);
			this.mainContainer.SplitterDistance = 245;
			this.mainContainer.TabIndex = 2;
			// 
			// chat
			// 
			this.chat.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chat.Location = new System.Drawing.Point(0, 0);
			this.chat.Name = "chat";
			this.chat.Size = new System.Drawing.Size(557, 403);
			this.chat.TabIndex = 1;
			// 
			// notifyIcon
			// 
			this.notifyIcon.Visible = true;
			// 
			// FrameContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(806, 450);
			this.Controls.Add(this.mainContainer);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "FrameContainer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "FrameContainer";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.mainContainer.Panel2.ResumeLayout(false);
			this.mainContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem sm_System;
		private System.Windows.Forms.SplitContainer mainContainer;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private IPMessagerNet.UI.Controls.ChatArea chat;
		private System.Windows.Forms.ToolStripMenuItem sm_System_Config;
		private System.Windows.Forms.ToolStripSeparator sm_System_SepConfig;
		private System.Windows.Forms.ToolStripMenuItem smSystem_Exit;
	}
}