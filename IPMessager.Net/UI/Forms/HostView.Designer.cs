namespace IPMessagerNet.UI.Forms
{
	partial class HostView
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostView));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			this.在线OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.离开AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
			this.第一分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.不启用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.按IP分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.按主机名分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.第二分组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.不启用ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.按IP分组ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.按主机组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.排序设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.按IP排序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.按状态排序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.按用户名排序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.第二排序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.按IP排序ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.按在线状态排序ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.按用户名排序ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.hostList = new IPMessagerNet.UI.Controls.HostTreeView.HostTreeView();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripDropDownButton();
			this.加入黑名单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.加入拨号列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.高级功能ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.代理群发ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.用户查询ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.远程命令ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButton5,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(284, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripDropDownButton1
			// 
			this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.在线OToolStripMenuItem,
            this.离开AToolStripMenuItem});
			this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
			this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
			this.toolStripDropDownButton1.Text = "状态";
			// 
			// 在线OToolStripMenuItem
			// 
			this.在线OToolStripMenuItem.Name = "在线OToolStripMenuItem";
			this.在线OToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.在线OToolStripMenuItem.Text = "在线(&O)";
			// 
			// 离开AToolStripMenuItem
			// 
			this.离开AToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripTextBox1});
			this.离开AToolStripMenuItem.Name = "离开AToolStripMenuItem";
			this.离开AToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.离开AToolStripMenuItem.Text = "离开(&A)";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
			// 
			// toolStripTextBox1
			// 
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
			this.toolStripTextBox1.Text = "输入自定义消息......";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "刷新";
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Text = "搜索";
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton4.Text = "屏蔽所有消息";
			// 
			// toolStripButton3
			// 
			this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.第一分组ToolStripMenuItem,
            this.第二分组ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.排序设置ToolStripMenuItem,
            this.第二排序ToolStripMenuItem});
			this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
			this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton3.Name = "toolStripButton3";
			this.toolStripButton3.Size = new System.Drawing.Size(29, 22);
			this.toolStripButton3.Text = "分组、排序设置";
			// 
			// 第一分组ToolStripMenuItem
			// 
			this.第一分组ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.不启用ToolStripMenuItem,
            this.按IP分组ToolStripMenuItem,
            this.按主机名分组ToolStripMenuItem});
			this.第一分组ToolStripMenuItem.Name = "第一分组ToolStripMenuItem";
			this.第一分组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.第一分组ToolStripMenuItem.Text = "第一分组";
			// 
			// 不启用ToolStripMenuItem
			// 
			this.不启用ToolStripMenuItem.Name = "不启用ToolStripMenuItem";
			this.不启用ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.不启用ToolStripMenuItem.Text = "不启用";
			// 
			// 按IP分组ToolStripMenuItem
			// 
			this.按IP分组ToolStripMenuItem.Name = "按IP分组ToolStripMenuItem";
			this.按IP分组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.按IP分组ToolStripMenuItem.Text = "按IP分组";
			// 
			// 按主机名分组ToolStripMenuItem
			// 
			this.按主机名分组ToolStripMenuItem.Name = "按主机名分组ToolStripMenuItem";
			this.按主机名分组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.按主机名分组ToolStripMenuItem.Text = "按主机名分组";
			// 
			// 第二分组ToolStripMenuItem
			// 
			this.第二分组ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.不启用ToolStripMenuItem1,
            this.按IP分组ToolStripMenuItem1,
            this.按主机组ToolStripMenuItem});
			this.第二分组ToolStripMenuItem.Name = "第二分组ToolStripMenuItem";
			this.第二分组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.第二分组ToolStripMenuItem.Text = "第二分组";
			// 
			// 不启用ToolStripMenuItem1
			// 
			this.不启用ToolStripMenuItem1.Name = "不启用ToolStripMenuItem1";
			this.不启用ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.不启用ToolStripMenuItem1.Text = "不启用";
			// 
			// 按IP分组ToolStripMenuItem1
			// 
			this.按IP分组ToolStripMenuItem1.Name = "按IP分组ToolStripMenuItem1";
			this.按IP分组ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
			this.按IP分组ToolStripMenuItem1.Text = "按IP分组";
			// 
			// 按主机组ToolStripMenuItem
			// 
			this.按主机组ToolStripMenuItem.Name = "按主机组ToolStripMenuItem";
			this.按主机组ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.按主机组ToolStripMenuItem.Text = "按主机组";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
			// 
			// 排序设置ToolStripMenuItem
			// 
			this.排序设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.按IP排序ToolStripMenuItem,
            this.按状态排序ToolStripMenuItem,
            this.按用户名排序ToolStripMenuItem});
			this.排序设置ToolStripMenuItem.Name = "排序设置ToolStripMenuItem";
			this.排序设置ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.排序设置ToolStripMenuItem.Text = "第一排序";
			// 
			// 按IP排序ToolStripMenuItem
			// 
			this.按IP排序ToolStripMenuItem.Name = "按IP排序ToolStripMenuItem";
			this.按IP排序ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.按IP排序ToolStripMenuItem.Text = "按IP排序";
			// 
			// 按状态排序ToolStripMenuItem
			// 
			this.按状态排序ToolStripMenuItem.Name = "按状态排序ToolStripMenuItem";
			this.按状态排序ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.按状态排序ToolStripMenuItem.Text = "按在线状态排序";
			// 
			// 按用户名排序ToolStripMenuItem
			// 
			this.按用户名排序ToolStripMenuItem.Name = "按用户名排序ToolStripMenuItem";
			this.按用户名排序ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.按用户名排序ToolStripMenuItem.Text = "按用户名排序";
			// 
			// 第二排序ToolStripMenuItem
			// 
			this.第二排序ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.按IP排序ToolStripMenuItem1,
            this.按在线状态排序ToolStripMenuItem,
            this.按用户名排序ToolStripMenuItem1});
			this.第二排序ToolStripMenuItem.Name = "第二排序ToolStripMenuItem";
			this.第二排序ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.第二排序ToolStripMenuItem.Text = "第二排序";
			// 
			// 按IP排序ToolStripMenuItem1
			// 
			this.按IP排序ToolStripMenuItem1.Name = "按IP排序ToolStripMenuItem1";
			this.按IP排序ToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
			this.按IP排序ToolStripMenuItem1.Text = "按IP排序";
			// 
			// 按在线状态排序ToolStripMenuItem
			// 
			this.按在线状态排序ToolStripMenuItem.Name = "按在线状态排序ToolStripMenuItem";
			this.按在线状态排序ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.按在线状态排序ToolStripMenuItem.Text = "按在线状态排序";
			// 
			// 按用户名排序ToolStripMenuItem1
			// 
			this.按用户名排序ToolStripMenuItem1.Name = "按用户名排序ToolStripMenuItem1";
			this.按用户名排序ToolStripMenuItem1.Size = new System.Drawing.Size(160, 22);
			this.按用户名排序ToolStripMenuItem1.Text = "按用户名排序";
			// 
			// hostList
			// 
			this.hostList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.hostList.Location = new System.Drawing.Point(2, 28);
			this.hostList.Name = "hostList";
			this.hostList.Size = new System.Drawing.Size(281, 229);
			this.hostList.TabIndex = 0;
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripSeparator2,
            this.toolStripMenuItem5,
            this.加入黑名单ToolStripMenuItem,
            this.加入拨号列表ToolStripMenuItem,
            this.toolStripMenuItem4,
            this.高级功能ToolStripMenuItem});
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(29, 22);
			this.toolStripButton5.Text = "主机操作";
			// 
			// 加入黑名单ToolStripMenuItem
			// 
			this.加入黑名单ToolStripMenuItem.Name = "加入黑名单ToolStripMenuItem";
			this.加入黑名单ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.加入黑名单ToolStripMenuItem.Text = "加入黑名单";
			// 
			// 加入拨号列表ToolStripMenuItem
			// 
			this.加入拨号列表ToolStripMenuItem.Name = "加入拨号列表ToolStripMenuItem";
			this.加入拨号列表ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.加入拨号列表ToolStripMenuItem.Text = "加入拨号列表";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem3.Text = "和TA对话";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(149, 6);
			// 
			// 高级功能ToolStripMenuItem
			// 
			this.高级功能ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.代理群发ToolStripMenuItem,
            this.用户查询ToolStripMenuItem,
            this.远程命令ToolStripMenuItem});
			this.高级功能ToolStripMenuItem.Name = "高级功能ToolStripMenuItem";
			this.高级功能ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.高级功能ToolStripMenuItem.Text = "高级功能";
			// 
			// 代理群发ToolStripMenuItem
			// 
			this.代理群发ToolStripMenuItem.Name = "代理群发ToolStripMenuItem";
			this.代理群发ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.代理群发ToolStripMenuItem.Text = "代理群发";
			// 
			// 用户查询ToolStripMenuItem
			// 
			this.用户查询ToolStripMenuItem.Name = "用户查询ToolStripMenuItem";
			this.用户查询ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.用户查询ToolStripMenuItem.Text = "用户查询";
			// 
			// 远程命令ToolStripMenuItem
			// 
			this.远程命令ToolStripMenuItem.Name = "远程命令ToolStripMenuItem";
			this.远程命令ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.远程命令ToolStripMenuItem.Text = "远程命令";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox2});
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem5.Text = "修改备注";
			// 
			// toolStripTextBox2
			// 
			this.toolStripTextBox2.Name = "toolStripTextBox2";
			this.toolStripTextBox2.Size = new System.Drawing.Size(100, 23);
			// 
			// HostView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.hostList);
			this.Name = "HostView";
			this.Text = "飞鸽传书.Net 在线主机";
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private IPMessagerNet.UI.Controls.HostTreeView.HostTreeView hostList;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
		private System.Windows.Forms.ToolStripMenuItem 在线OToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 离开AToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStripDropDownButton toolStripButton3;
		private System.Windows.Forms.ToolStripMenuItem 第一分组ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 不启用ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 按IP分组ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 按主机名分组ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 第二分组ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 不启用ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 按IP分组ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 按主机组ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 排序设置ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 按IP排序ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 按状态排序ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 按用户名排序ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 第二排序ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 按IP排序ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 按在线状态排序ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 按用户名排序ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripDropDownButton toolStripButton5;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem 加入黑名单ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 加入拨号列表ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
		private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
		private System.Windows.Forms.ToolStripMenuItem 高级功能ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 代理群发ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 用户查询ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 远程命令ToolStripMenuItem;
	}
}