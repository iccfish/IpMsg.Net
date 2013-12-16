namespace IPMessagerNet.UI.Dialogs.Notify
{
	partial class InitializeError
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(80, 80);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(94, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(242, 22);
			this.label1.TabIndex = 1;
			this.label1.Text = "很抱歉，飞鸽传书.Net 无法启动";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(98, 46);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(325, 132);
			this.textBox1.TabIndex = 2;
			this.textBox1.Text = "出现这个错误时，绝大部分情况下是因为网络端口被占用。\r\n\r\n导致网络端口被占用的原因通常是因为你不经意间打开了多个飞鸽传书（或飞鸽传书.Net）。\r\n\r\n仔细检查" +
				"你的托盘察看是否开了多个飞鸽传书。如果没有，尝试退出你打开的程序来确定是哪个应用程序占用了网络端口。\r\n\r\n如果还没有解决问题，请联系作者。";
			// 
			// btnOk
			// 
			this.btnOk.ImageIndex = 0;
			this.btnOk.Location = new System.Drawing.Point(340, 184);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(83, 25);
			this.btnOk.TabIndex = 3;
			this.btnOk.Tag = "Ok1";
			this.btnOk.Text = "  确  定";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.button1_Click);
			// 
			// InitializeError
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(435, 216);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "InitializeError";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "启动失败";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnOk;
	}
}