namespace IPMessagerNet.UI.Controls.Config
{
	partial class ThemeConfigPanel
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
			this.label1 = new System.Windows.Forms.Label();
			this.cbTheme = new System.Windows.Forms.ComboBox();
			this.txtNote = new System.Windows.Forms.TextBox();
			this.imageLoader1 = new IPMessagerNet.UI.EditorControls.ImageLoader();
			this.panTip = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.pbPreview = new System.Windows.Forms.PictureBox();
			this.panTip.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(125, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "设置您要使用的主题：";
			// 
			// cbTheme
			// 
			this.cbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbTheme.FormattingEnabled = true;
			this.cbTheme.Location = new System.Drawing.Point(145, 18);
			this.cbTheme.Name = "cbTheme";
			this.cbTheme.Size = new System.Drawing.Size(273, 20);
			this.cbTheme.TabIndex = 1;
			// 
			// txtNote
			// 
			this.txtNote.Location = new System.Drawing.Point(16, 191);
			this.txtNote.Multiline = true;
			this.txtNote.Name = "txtNote";
			this.txtNote.ReadOnly = true;
			this.txtNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtNote.Size = new System.Drawing.Size(515, 177);
			this.txtNote.TabIndex = 2;
			// 
			// imageLoader1
			// 
			this.imageLoader1.ImageLocationDefinition = "Images,tip";
			this.imageLoader1.Location = new System.Drawing.Point(0, 0);
			this.imageLoader1.Name = "imageLoader1";
			this.imageLoader1.Size = new System.Drawing.Size(16, 16);
			this.imageLoader1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imageLoader1.TabIndex = 5;
			this.imageLoader1.TabStop = false;
			// 
			// panTip
			// 
			this.panTip.Controls.Add(this.label2);
			this.panTip.Controls.Add(this.imageLoader1);
			this.panTip.Location = new System.Drawing.Point(16, 169);
			this.panTip.Name = "panTip";
			this.panTip.Size = new System.Drawing.Size(515, 16);
			this.panTip.TabIndex = 6;
			this.panTip.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(245, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "更改主题需要重新启动飞鸽传书.Net才能起效";
			// 
			// pbPreview
			// 
			this.pbPreview.Location = new System.Drawing.Point(16, 44);
			this.pbPreview.Name = "pbPreview";
			this.pbPreview.Size = new System.Drawing.Size(514, 114);
			this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbPreview.TabIndex = 7;
			this.pbPreview.TabStop = false;
			// 
			// ThemeConfigPanel
			// 
			this.Controls.Add(this.pbPreview);
			this.Controls.Add(this.panTip);
			this.Controls.Add(this.txtNote);
			this.Controls.Add(this.cbTheme);
			this.Controls.Add(this.label1);
			this.Name = "ThemeConfigPanel";
			this.panTip.ResumeLayout(false);
			this.panTip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbTheme;
		private System.Windows.Forms.TextBox txtNote;
		private EditorControls.ImageLoader imageLoader1;
		private System.Windows.Forms.Panel panTip;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pbPreview;
	}
}
