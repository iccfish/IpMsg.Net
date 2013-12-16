namespace IPMessagerNet.UI.Dialogs
{
	partial class HostListEditor
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
			this.hlist = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.lvSelectedHostList = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.btnAdd1 = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnAdd2 = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtIP = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// hlist
			// 
			this.hlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
			this.hlist.FullRowSelect = true;
			this.hlist.Location = new System.Drawing.Point(12, 32);
			this.hlist.Name = "hlist";
			this.hlist.Size = new System.Drawing.Size(292, 176);
			this.hlist.TabIndex = 2;
			this.hlist.UseCompatibleStateImageBehavior = false;
			this.hlist.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "IP地址";
			this.columnHeader1.Width = 98;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "当前状态";
			this.columnHeader2.Width = 94;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "备注";
			this.columnHeader3.Width = 88;
			// 
			// lvSelectedHostList
			// 
			this.lvSelectedHostList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.lvSelectedHostList.FullRowSelect = true;
			this.lvSelectedHostList.Location = new System.Drawing.Point(350, 32);
			this.lvSelectedHostList.Name = "lvSelectedHostList";
			this.lvSelectedHostList.Size = new System.Drawing.Size(299, 176);
			this.lvSelectedHostList.TabIndex = 3;
			this.lvSelectedHostList.UseCompatibleStateImageBehavior = false;
			this.lvSelectedHostList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "IP地址";
			this.columnHeader4.Width = 98;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "当前状态";
			this.columnHeader5.Width = 99;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "备注";
			this.columnHeader6.Width = 88;
			// 
			// btnAdd1
			// 
			this.btnAdd1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAdd1.ImageIndex = 11;
			this.btnAdd1.Location = new System.Drawing.Point(310, 67);
			this.btnAdd1.Name = "btnAdd1";
			this.btnAdd1.Size = new System.Drawing.Size(33, 24);
			this.btnAdd1.TabIndex = 4;
			this.btnAdd1.UseVisualStyleBackColor = true;
			// 
			// btnRemove
			// 
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnRemove.ImageIndex = 10;
			this.btnRemove.Location = new System.Drawing.Point(310, 124);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(34, 24);
			this.btnRemove.TabIndex = 5;
			this.btnRemove.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "当前在线主机";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(348, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 7;
			this.label2.Text = "选定主机列表";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 223);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(101, 12);
			this.label3.TabIndex = 8;
			this.label3.Text = "或直接输入IP地址";
			// 
			// btnAdd2
			// 
			this.btnAdd2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btnAdd2.ImageIndex = 11;
			this.btnAdd2.Location = new System.Drawing.Point(311, 217);
			this.btnAdd2.Name = "btnAdd2";
			this.btnAdd2.Size = new System.Drawing.Size(33, 21);
			this.btnAdd2.TabIndex = 10;
			this.btnAdd2.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.ImageIndex = 0;
			this.btnOk.Location = new System.Drawing.Point(495, 212);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(71, 28);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "  确定";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.ImageIndex = 7;
			this.btnCancel.Location = new System.Drawing.Point(572, 212);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(71, 28);
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "  取消";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// txtIP
			// 
			this.txtIP.Location = new System.Drawing.Point(119, 218);
			this.txtIP.Name = "txtIP";
			this.txtIP.Size = new System.Drawing.Size(186, 21);
			this.txtIP.TabIndex = 12;
			// 
			// HostListEditor
			// 
			this.AcceptButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(655, 246);
			this.Controls.Add(this.txtIP);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnAdd2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd1);
			this.Controls.Add(this.lvSelectedHostList);
			this.Controls.Add(this.hlist);
			this.Name = "HostListEditor";
			this.Text = "编辑主机列表";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView hlist;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ListView lvSelectedHostList;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Button btnAdd1;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnAdd2;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtIP;

	}
}
