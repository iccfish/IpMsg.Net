namespace IPMessagerNet.UI.Dialogs
{
	partial class ModifyHostGroupAndMemo
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
			this.label2 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cbGroup = new System.Windows.Forms.ComboBox();
			this.txtMemo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "用户备注名称：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "设置用户组：";
			// 
			// btnOK
			// 
			this.btnOK.ImageIndex = 0;
			this.btnOK.Location = new System.Drawing.Point(225, 88);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(74, 25);
			this.btnOK.TabIndex = 1;
			this.btnOK.Tag = "";
			this.btnOK.Text = "  确定";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.ImageIndex = 7;
			this.btnCancel.Location = new System.Drawing.Point(305, 88);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(74, 25);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Tag = "";
			this.btnCancel.Text = "  取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// cbGroup
			// 
			this.cbGroup.FormattingEnabled = true;
			this.cbGroup.Location = new System.Drawing.Point(120, 19);
			this.cbGroup.Name = "cbGroup";
			this.cbGroup.Size = new System.Drawing.Size(259, 20);
			this.cbGroup.TabIndex = 2;
			// 
			// txtMemo
			// 
			this.txtMemo.Location = new System.Drawing.Point(120, 45);
			this.txtMemo.Name = "txtMemo";
			this.txtMemo.Size = new System.Drawing.Size(259, 21);
			this.txtMemo.TabIndex = 3;
			// 
			// ModifyHostGroupAndMemo
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(411, 128);
			this.Controls.Add(this.txtMemo);
			this.Controls.Add(this.cbGroup);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ModifyHostGroupAndMemo";
			this.Text = "修改主机分组和备注设置";
			this.Load += new System.EventHandler(this.ModifyHostGroupAndMemo_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cbGroup;
		private System.Windows.Forms.TextBox txtMemo;
	}
}