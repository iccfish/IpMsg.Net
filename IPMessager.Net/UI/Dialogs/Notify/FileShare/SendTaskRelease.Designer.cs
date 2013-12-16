using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Dialogs.Notify.FileShare
{
	partial class SendTaskRelease
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
			this.lblDesc = new System.Windows.Forms.Label();
			this.fileList = new System.Windows.Forms.ListBox();
			this.slideComponent1 = new SlideComponent();
			this.pbTip = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbTip)).BeginInit();
			this.SuspendLayout();
			// 
			// lblDesc
			// 
			this.lblDesc.Location = new System.Drawing.Point(58, 9);
			this.lblDesc.Name = "lblDesc";
			this.lblDesc.Size = new System.Drawing.Size(326, 54);
			this.lblDesc.TabIndex = 0;
			this.lblDesc.Text = "LabelDesc";
			// 
			// fileList
			// 
			this.fileList.FormattingEnabled = true;
			this.fileList.ItemHeight = 12;
			this.fileList.Location = new System.Drawing.Point(60, 66);
			this.fileList.Name = "fileList";
			this.fileList.Size = new System.Drawing.Size(324, 76);
			this.fileList.TabIndex = 2;
			// 
			// slideComponent1
			// 
			this.slideComponent1.AlwaysSetLocation = false;
			this.slideComponent1.AttachedForm = this;
			this.slideComponent1.DirectX = SlideComponent.FlyXDirection.None;
			this.slideComponent1.MoveSpeedX = 20;
			this.slideComponent1.MoveSpeedY = 18;
			// 
			// pbTip
			// 
			this.pbTip.Location = new System.Drawing.Point(12, 9);
			this.pbTip.Name = "pbTip";
			this.pbTip.Size = new System.Drawing.Size(40, 40);
			this.pbTip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbTip.TabIndex = 1;
			this.pbTip.TabStop = false;
			// 
			// SendTaskRelease
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(396, 154);
			this.Controls.Add(this.fileList);
			this.Controls.Add(this.pbTip);
			this.Controls.Add(this.lblDesc);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SendTaskRelease";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "对方忽略了您发送的文件";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pbTip)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblDesc;
		private System.Windows.Forms.PictureBox pbTip;
		private System.Windows.Forms.ListBox fileList;
		private SlideComponent slideComponent1;
	}
}