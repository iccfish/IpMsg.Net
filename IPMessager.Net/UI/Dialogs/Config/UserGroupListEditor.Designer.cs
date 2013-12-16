namespace IPMessagerNet.UI.Dialogs.Config
{
	partial class UserGroupListEditor
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
			this.label1 = new System.Windows.Forms.Label();
			this.hlist = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnClose = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(433, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "下面显示的是归于这个主机组的主机以及它们的在线状态。如果全部删除，则这个主机组将会消失。";
			// 
			// hlist
			// 
			this.hlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.hlist.FullRowSelect = true;
			this.hlist.Location = new System.Drawing.Point(12, 42);
			this.hlist.Name = "hlist";
			this.hlist.Size = new System.Drawing.Size(433, 176);
			this.hlist.SmallImageList = this.imageList1;
			this.hlist.TabIndex = 1;
			this.hlist.UseCompatibleStateImageBehavior = false;
			this.hlist.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "IP地址";
			this.columnHeader1.Width = 150;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "当前状态";
			this.columnHeader2.Width = 150;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "备注";
			this.columnHeader3.Width = 125;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(1, 20);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnClose
			// 
			this.btnClose.ImageIndex = 1;
			this.btnClose.Location = new System.Drawing.Point(362, 224);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(83, 29);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "  关闭";
			this.btnClose.UseVisualStyleBackColor = true;
			// 
			// btnRemove
			// 
			this.btnRemove.ImageIndex = 8;
			this.btnRemove.Location = new System.Drawing.Point(14, 224);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(69, 28);
			this.btnRemove.TabIndex = 3;
			this.btnRemove.Text = "  删除";
			this.btnRemove.UseVisualStyleBackColor = true;
			// 
			// btnClear
			// 
			this.btnClear.ImageIndex = 8;
			this.btnClear.Location = new System.Drawing.Point(89, 224);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(69, 28);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "  清空";
			this.btnClear.UseVisualStyleBackColor = true;
			// 
			// UserGroupListEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(457, 259);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.hlist);
			this.Controls.Add(this.label1);
			this.Name = "UserGroupListEditor";
			this.Text = "主机管理";
			this.Load += new System.EventHandler(this.UserGroupListEditor_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView hlist;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnClear;
	}
}