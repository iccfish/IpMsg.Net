using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Dialogs.Notify
{
	partial class FetchFolderInfo
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.statusText = new System.Windows.Forms.Label();
			this.silder = new SlideComponent();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(3, 24);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(241, 16);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar1.TabIndex = 0;
			// 
			// label1
			// 
			this.statusText.AutoSize = true;
			this.statusText.Location = new System.Drawing.Point(4, 7);
			this.statusText.Name = "label1";
			this.statusText.Size = new System.Drawing.Size(191, 12);
			this.statusText.TabIndex = 2;
			this.statusText.Text = "正在扫描文件数据准备发送.......";
			// 
			// slideComponent1
			// 
			this.silder.AlwaysSetLocation = false;
			this.silder.AttachedForm = this;
			this.silder.DirectX = SlideComponent.FlyXDirection.None;
			this.silder.MoveSpeedX = 0;
			this.silder.MoveSpeedY = 7;
			// 
			// FetchFolderInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(247, 41);
			this.ControlBox = false;
			this.Controls.Add(this.statusText);
			this.Controls.Add(this.progressBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FetchFolderInfo";
			this.ShowInTaskbar = false;
			this.TopMost = true;
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private SlideComponent silder;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label statusText;
	}
}