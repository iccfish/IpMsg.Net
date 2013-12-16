using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Controls.Config
{
	partial class HostConfigPanel
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
			this.components = new System.ComponentModel.Container();
			this.horizontalLine1 = new HorizontalLine();
			this.lvUserGroup = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.horizontalLine2 = new HorizontalLine();
			this.lvMemo = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.imageLoader2 = new IPMessagerNet.UI.EditorControls.ImageLoader();
			this.imageLoader1 = new IPMessagerNet.UI.EditorControls.ImageLoader();
			this.lnkGroupEdit = new System.Windows.Forms.LinkLabel();
			this.lnkGroupDelete = new System.Windows.Forms.LinkLabel();
			this.lnkGroupHostManage = new System.Windows.Forms.LinkLabel();
			this.lnkMemoDelete = new System.Windows.Forms.LinkLabel();
			this.lnkMemoEdit = new System.Windows.Forms.LinkLabel();
			this.lnkMemoClear = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.imageLoader2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.imageLoader1)).BeginInit();
			this.SuspendLayout();
			// 
			// horizontalLine1
			// 
			this.horizontalLine1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine1.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine1.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine1.LineColor = System.Drawing.SystemColors.ControlDark;
			this.horizontalLine1.Location = new System.Drawing.Point(3, 3);
			this.horizontalLine1.Name = "horizontalLine1";
			this.horizontalLine1.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine1.Size = new System.Drawing.Size(544, 17);
			this.horizontalLine1.TabIndex = 0;
			this.horizontalLine1.Text = "用户分组和关联设置";
			this.horizontalLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine1.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// lvUserGroup
			// 
			this.lvUserGroup.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lvUserGroup.FullRowSelect = true;
			this.lvUserGroup.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvUserGroup.LabelEdit = true;
			this.lvUserGroup.Location = new System.Drawing.Point(3, 26);
			this.lvUserGroup.MultiSelect = false;
			this.lvUserGroup.Name = "lvUserGroup";
			this.lvUserGroup.Size = new System.Drawing.Size(544, 117);
			this.lvUserGroup.SmallImageList = this.imageList1;
			this.lvUserGroup.TabIndex = 1;
			this.lvUserGroup.UseCompatibleStateImageBehavior = false;
			this.lvUserGroup.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "分组名";
			this.columnHeader1.Width = 420;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "归组主机数";
			this.columnHeader2.Width = 100;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(5, 20);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// horizontalLine2
			// 
			this.horizontalLine2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.horizontalLine2.BackColor = System.Drawing.Color.Transparent;
			this.horizontalLine2.Font = new System.Drawing.Font("Tahoma", 9F);
			this.horizontalLine2.LineColor = System.Drawing.SystemColors.ControlDark;
			this.horizontalLine2.Location = new System.Drawing.Point(3, 180);
			this.horizontalLine2.Name = "horizontalLine2";
			this.horizontalLine2.PanelAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine2.Size = new System.Drawing.Size(544, 14);
			this.horizontalLine2.TabIndex = 2;
			this.horizontalLine2.Text = "用户备注设置";
			this.horizontalLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.horizontalLine2.TextFont = new System.Drawing.Font("Tahoma", 9F);
			// 
			// lvMemo
			// 
			this.lvMemo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.lvMemo.FullRowSelect = true;
			this.lvMemo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvMemo.LabelEdit = true;
			this.lvMemo.Location = new System.Drawing.Point(3, 200);
			this.lvMemo.Name = "lvMemo";
			this.lvMemo.Size = new System.Drawing.Size(544, 140);
			this.lvMemo.SmallImageList = this.imageList1;
			this.lvMemo.TabIndex = 3;
			this.lvMemo.UseCompatibleStateImageBehavior = false;
			this.lvMemo.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "备注名";
			this.columnHeader3.Width = 150;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "IP地址";
			this.columnHeader4.Width = 120;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "当前状态";
			this.columnHeader5.Width = 250;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 151);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(269, 12);
			this.label1.TabIndex = 5;
			this.label1.Text = "更改分组设置需要刷新在线用户列表才能完全起效";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(25, 351);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(137, 12);
			this.label2.TabIndex = 7;
			this.label2.Text = "备注过多会影响运行效率";
			// 
			// imageLoader2
			// 
			this.imageLoader2.ImageLocationDefinition = "Images,tip";
			this.imageLoader2.Location = new System.Drawing.Point(3, 349);
			this.imageLoader2.Name = "imageLoader2";
			this.imageLoader2.Size = new System.Drawing.Size(16, 16);
			this.imageLoader2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imageLoader2.TabIndex = 6;
			this.imageLoader2.TabStop = false;
			// 
			// imageLoader1
			// 
			this.imageLoader1.ImageLocationDefinition = "Images,tip";
			this.imageLoader1.Location = new System.Drawing.Point(3, 149);
			this.imageLoader1.Name = "imageLoader1";
			this.imageLoader1.Size = new System.Drawing.Size(16, 16);
			this.imageLoader1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.imageLoader1.TabIndex = 4;
			this.imageLoader1.TabStop = false;
			// 
			// lnkGroupEdit
			// 
			this.lnkGroupEdit.AutoSize = true;
			this.lnkGroupEdit.Location = new System.Drawing.Point(424, 151);
			this.lnkGroupEdit.Name = "lnkGroupEdit";
			this.lnkGroupEdit.Size = new System.Drawing.Size(29, 12);
			this.lnkGroupEdit.TabIndex = 8;
			this.lnkGroupEdit.TabStop = true;
			this.lnkGroupEdit.Text = "编辑";
			// 
			// lnkGroupDelete
			// 
			this.lnkGroupDelete.AutoSize = true;
			this.lnkGroupDelete.Location = new System.Drawing.Point(459, 151);
			this.lnkGroupDelete.Name = "lnkGroupDelete";
			this.lnkGroupDelete.Size = new System.Drawing.Size(29, 12);
			this.lnkGroupDelete.TabIndex = 8;
			this.lnkGroupDelete.TabStop = true;
			this.lnkGroupDelete.Text = "删除";
			// 
			// lnkGroupHostManage
			// 
			this.lnkGroupHostManage.AutoSize = true;
			this.lnkGroupHostManage.Location = new System.Drawing.Point(494, 151);
			this.lnkGroupHostManage.Name = "lnkGroupHostManage";
			this.lnkGroupHostManage.Size = new System.Drawing.Size(53, 12);
			this.lnkGroupHostManage.TabIndex = 8;
			this.lnkGroupHostManage.TabStop = true;
			this.lnkGroupHostManage.Text = "主机管理";
			// 
			// lnkMemoDelete
			// 
			this.lnkMemoDelete.AutoSize = true;
			this.lnkMemoDelete.Location = new System.Drawing.Point(459, 349);
			this.lnkMemoDelete.Name = "lnkMemoDelete";
			this.lnkMemoDelete.Size = new System.Drawing.Size(29, 12);
			this.lnkMemoDelete.TabIndex = 11;
			this.lnkMemoDelete.TabStop = true;
			this.lnkMemoDelete.Text = "删除";
			// 
			// lnkMemoEdit
			// 
			this.lnkMemoEdit.AutoSize = true;
			this.lnkMemoEdit.Location = new System.Drawing.Point(424, 349);
			this.lnkMemoEdit.Name = "lnkMemoEdit";
			this.lnkMemoEdit.Size = new System.Drawing.Size(29, 12);
			this.lnkMemoEdit.TabIndex = 10;
			this.lnkMemoEdit.TabStop = true;
			this.lnkMemoEdit.Text = "编辑";
			// 
			// lnkMemoClear
			// 
			this.lnkMemoClear.AutoSize = true;
			this.lnkMemoClear.Location = new System.Drawing.Point(494, 349);
			this.lnkMemoClear.Name = "lnkMemoClear";
			this.lnkMemoClear.Size = new System.Drawing.Size(53, 12);
			this.lnkMemoClear.TabIndex = 12;
			this.lnkMemoClear.TabStop = true;
			this.lnkMemoClear.Text = "清空备注";
			// 
			// HostConfigPanel
			// 
			this.Controls.Add(this.lnkMemoClear);
			this.Controls.Add(this.lnkMemoDelete);
			this.Controls.Add(this.lnkMemoEdit);
			this.Controls.Add(this.lnkGroupHostManage);
			this.Controls.Add(this.lnkGroupDelete);
			this.Controls.Add(this.lnkGroupEdit);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.imageLoader2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.imageLoader1);
			this.Controls.Add(this.lvMemo);
			this.Controls.Add(this.horizontalLine2);
			this.Controls.Add(this.lvUserGroup);
			this.Controls.Add(this.horizontalLine1);
			this.Name = "HostConfigPanel";
			this.Load += new System.EventHandler(this.HostConfigPanel_Load);
			((System.ComponentModel.ISupportInitialize)(this.imageLoader2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.imageLoader1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private HorizontalLine horizontalLine1;
		private System.Windows.Forms.ListView lvUserGroup;
		private HorizontalLine horizontalLine2;
		private System.Windows.Forms.ListView lvMemo;
		private IPMessagerNet.UI.EditorControls.ImageLoader imageLoader1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private IPMessagerNet.UI.EditorControls.ImageLoader imageLoader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.LinkLabel lnkGroupEdit;
		private System.Windows.Forms.LinkLabel lnkGroupDelete;
		private System.Windows.Forms.LinkLabel lnkGroupHostManage;
		private System.Windows.Forms.LinkLabel lnkMemoDelete;
		private System.Windows.Forms.LinkLabel lnkMemoEdit;
		private System.Windows.Forms.LinkLabel lnkMemoClear;
	}
}
