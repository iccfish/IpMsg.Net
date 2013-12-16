using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Forms
{
	partial class SysConfig
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
			this.lstMenu = new IPMessagerNet.UI.Controls.Config.ExListBox();
			this.horizontalLine1 = new HorizontalLine();
			this.panConfig = new System.Windows.Forms.Panel();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnAbout = new System.Windows.Forms.Button();
			this.btnRestart = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstMenu
			// 
			this.lstMenu.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.lstMenu.FormattingEnabled = true;
			this.lstMenu.ItemHeight = 12;
			this.lstMenu.Location = new System.Drawing.Point(12, 12);
			this.lstMenu.Name = "lstMenu";
			this.lstMenu.Size = new System.Drawing.Size(117, 412);
			this.lstMenu.TabIndex = 0;
			// 
			// horizontalLine1
			// 
			this.horizontalLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine1.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine1.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.horizontalLine1.LineColor = System.Drawing.SystemColors.ButtonShadow;
			this.horizontalLine1.Location = new System.Drawing.Point(135, 389);
			this.horizontalLine1.Name = "horizontalLine1";
			this.horizontalLine1.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine1.Size = new System.Drawing.Size(550, 10);
			this.horizontalLine1.TabIndex = 1;
			this.horizontalLine1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.horizontalLine1.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// panConfig
			// 
			this.panConfig.AutoScroll = true;
			this.panConfig.Location = new System.Drawing.Point(135, 12);
			this.panConfig.Name = "panConfig";
			this.panConfig.Size = new System.Drawing.Size(550, 371);
			this.panConfig.TabIndex = 2;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.ImageIndex = 0;
			this.btnOk.Location = new System.Drawing.Point(599, 397);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(86, 27);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "  关闭";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnAbout
			// 
			this.btnAbout.ImageIndex = 5;
			this.btnAbout.Location = new System.Drawing.Point(135, 397);
			this.btnAbout.Name = "btnAbout";
			this.btnAbout.Size = new System.Drawing.Size(86, 27);
			this.btnAbout.TabIndex = 5;
			this.btnAbout.Text = "  关于...";
			this.btnAbout.UseVisualStyleBackColor = true;
			this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
			// 
			// btnRestart
			// 
			this.btnRestart.ImageIndex = 1;
			this.btnRestart.Location = new System.Drawing.Point(227, 397);
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(115, 27);
			this.btnRestart.TabIndex = 6;
			this.btnRestart.Text = "  重启飞鸽传书";
			this.btnRestart.UseVisualStyleBackColor = true;
			this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
			// 
			// SysConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(697, 435);
			this.Controls.Add(this.btnRestart);
			this.Controls.Add(this.btnAbout);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.panConfig);
			this.Controls.Add(this.horizontalLine1);
			this.Controls.Add(this.lstMenu);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SysConfig";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "自定义您的飞鸽传书.Net";
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.Config.ExListBox lstMenu;
		private HorizontalLine horizontalLine1;
		private System.Windows.Forms.Panel panConfig;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnAbout;
		private System.Windows.Forms.Button btnRestart;
	}
}