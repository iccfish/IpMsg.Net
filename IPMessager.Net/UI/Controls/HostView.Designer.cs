namespace IPMessagerNet.UI.Controls
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HostView));
			this.tbHost = new System.Windows.Forms.ToolStrip();
			this.tState = new System.Windows.Forms.ToolStripDropDownButton();
			this.mStateOnline = new System.Windows.Forms.ToolStripMenuItem();
			this.mStateAbsence = new System.Windows.Forms.ToolStripMenuItem();
			this.tolStateAbsSep = new System.Windows.Forms.ToolStripSeparator();
			this.mStateAbsenceMsg = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
			this.mViewFloat = new System.Windows.Forms.ToolStripMenuItem();
			this.tSepState = new System.Windows.Forms.ToolStripSeparator();
			this.tRefresh = new System.Windows.Forms.ToolStripButton();
			this.tChat = new System.Windows.Forms.ToolStripDropDownButton();
			this.mChat = new System.Windows.Forms.ToolStripMenuItem();
			this.mSepChat = new System.Windows.Forms.ToolStripSeparator();
			this.mGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.mBan = new System.Windows.Forms.ToolStripMenuItem();
			this.mDial = new System.Windows.Forms.ToolStripMenuItem();
			this.tSearch = new System.Windows.Forms.ToolStripButton();
			this.tGroupSort = new System.Windows.Forms.ToolStripDropDownButton();
			this.mGroup1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup1_None = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup1_User = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup1_IP = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup1_Host = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup2 = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup2_None = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup2_User = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup2_IP = new System.Windows.Forms.ToolStripMenuItem();
			this.mGroup2_Host = new System.Windows.Forms.ToolStripMenuItem();
			this.mSepGroup = new System.Windows.Forms.ToolStripSeparator();
			this.mSort1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSort1_IP = new System.Windows.Forms.ToolStripMenuItem();
			this.mSort1_State = new System.Windows.Forms.ToolStripMenuItem();
			this.mSort1_User = new System.Windows.Forms.ToolStripMenuItem();
			this.mSort2 = new System.Windows.Forms.ToolStripMenuItem();
			this.mSort2_IP = new System.Windows.Forms.ToolStripMenuItem();
			this.mSort2_State = new System.Windows.Forms.ToolStripMenuItem();
			this.mSort2_User = new System.Windows.Forms.ToolStripMenuItem();
			this.tSepFunc = new System.Windows.Forms.ToolStripSeparator();
			this.tMute = new System.Windows.Forms.ToolStripButton();
			this.hostList = new IPMessagerNet.UI.Controls.HostTreeView.HostTreeView();
			this.tbHost.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbHost
			// 
			this.tbHost.CanOverflow = false;
			this.tbHost.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tbHost.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tState,
            this.tSepState,
            this.tRefresh,
            this.tChat,
            this.tSearch,
            this.tGroupSort,
            this.tSepFunc,
            this.tMute});
			this.tbHost.Location = new System.Drawing.Point(0, 0);
			this.tbHost.Name = "tbHost";
			this.tbHost.Size = new System.Drawing.Size(284, 25);
			this.tbHost.TabIndex = 1;
			this.tbHost.Text = "toolStrip1";
			// 
			// tState
			// 
			this.tState.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tState.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mStateOnline,
            this.mStateAbsence,
            this.toolStripMenuItem8,
            this.mViewFloat});
			this.tState.Image = ((System.Drawing.Image)(resources.GetObject("tState.Image")));
			this.tState.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tState.Name = "tState";
			this.tState.Size = new System.Drawing.Size(29, 22);
			this.tState.Text = "状态";
			// 
			// mStateOnline
			// 
			this.mStateOnline.Name = "mStateOnline";
			this.mStateOnline.Size = new System.Drawing.Size(148, 22);
			this.mStateOnline.Text = "在线(&O)";
			// 
			// mStateAbsence
			// 
			this.mStateAbsence.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tolStateAbsSep,
            this.mStateAbsenceMsg});
			this.mStateAbsence.Name = "mStateAbsence";
			this.mStateAbsence.Size = new System.Drawing.Size(148, 22);
			this.mStateAbsence.Text = "离开(&A)";
			// 
			// tolStateAbsSep
			// 
			this.tolStateAbsSep.Name = "tolStateAbsSep";
			this.tolStateAbsSep.Size = new System.Drawing.Size(157, 6);
			// 
			// mStateAbsenceMsg
			// 
			this.mStateAbsenceMsg.Name = "mStateAbsenceMsg";
			this.mStateAbsenceMsg.Size = new System.Drawing.Size(100, 23);
			this.mStateAbsenceMsg.Text = "输入自定义消息......";
			// 
			// toolStripMenuItem8
			// 
			this.toolStripMenuItem8.Name = "toolStripMenuItem8";
			this.toolStripMenuItem8.Size = new System.Drawing.Size(145, 6);
			// 
			// mViewFloat
			// 
			this.mViewFloat.CheckOnClick = true;
			this.mViewFloat.Name = "mViewFloat";
			this.mViewFloat.Size = new System.Drawing.Size(148, 22);
			this.mViewFloat.Text = "主机列表浮动";
			// 
			// tSepState
			// 
			this.tSepState.Name = "tSepState";
			this.tSepState.Size = new System.Drawing.Size(6, 25);
			// 
			// tRefresh
			// 
			this.tRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tRefresh.Image")));
			this.tRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tRefresh.Name = "tRefresh";
			this.tRefresh.Size = new System.Drawing.Size(23, 22);
			this.tRefresh.Text = "刷新";
			// 
			// tChat
			// 
			this.tChat.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tChat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mChat,
            this.mSepChat,
            this.mGroup,
            this.mBan,
            this.mDial});
			this.tChat.Image = ((System.Drawing.Image)(resources.GetObject("tChat.Image")));
			this.tChat.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tChat.Name = "tChat";
			this.tChat.Size = new System.Drawing.Size(29, 22);
			this.tChat.Text = "主机操作";
			// 
			// mChat
			// 
			this.mChat.Name = "mChat";
			this.mChat.Size = new System.Drawing.Size(181, 22);
			this.mChat.Text = "和TA对话";
			// 
			// mSepChat
			// 
			this.mSepChat.Name = "mSepChat";
			this.mSepChat.Size = new System.Drawing.Size(178, 6);
			// 
			// mGroup
			// 
			this.mGroup.Name = "mGroup";
			this.mGroup.Size = new System.Drawing.Size(181, 22);
			this.mGroup.Text = "修改备注/分组";
			// 
			// mBan
			// 
			this.mBan.Name = "mBan";
			this.mBan.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.mBan.Size = new System.Drawing.Size(181, 22);
			this.mBan.Text = "加入黑名单";
			// 
			// mDial
			// 
			this.mDial.Name = "mDial";
			this.mDial.Size = new System.Drawing.Size(181, 22);
			this.mDial.Text = "加入拨号列表";
			// 
			// tSearch
			// 
			this.tSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tSearch.Image = ((System.Drawing.Image)(resources.GetObject("tSearch.Image")));
			this.tSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSearch.Name = "tSearch";
			this.tSearch.Size = new System.Drawing.Size(23, 22);
			this.tSearch.Text = "搜索";
			// 
			// tGroupSort
			// 
			this.tGroupSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tGroupSort.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGroup1,
            this.mGroup2,
            this.mSepGroup,
            this.mSort1,
            this.mSort2});
			this.tGroupSort.Image = ((System.Drawing.Image)(resources.GetObject("tGroupSort.Image")));
			this.tGroupSort.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tGroupSort.Name = "tGroupSort";
			this.tGroupSort.Size = new System.Drawing.Size(29, 22);
			this.tGroupSort.Text = "分组、排序设置";
			// 
			// mGroup1
			// 
			this.mGroup1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGroup1_None,
            this.mGroup1_User,
            this.mGroup1_IP,
            this.mGroup1_Host});
			this.mGroup1.Name = "mGroup1";
			this.mGroup1.Size = new System.Drawing.Size(124, 22);
			this.mGroup1.Text = "第一分组";
			// 
			// mGroup1_None
			// 
			this.mGroup1_None.Name = "mGroup1_None";
			this.mGroup1_None.Size = new System.Drawing.Size(136, 22);
			this.mGroup1_None.Text = "不启用";
			// 
			// mGroup1_User
			// 
			this.mGroup1_User.Name = "mGroup1_User";
			this.mGroup1_User.Size = new System.Drawing.Size(136, 22);
			this.mGroup1_User.Text = "自定义分组";
			// 
			// mGroup1_IP
			// 
			this.mGroup1_IP.Name = "mGroup1_IP";
			this.mGroup1_IP.Size = new System.Drawing.Size(136, 22);
			this.mGroup1_IP.Text = "按IP分组";
			// 
			// mGroup1_Host
			// 
			this.mGroup1_Host.Name = "mGroup1_Host";
			this.mGroup1_Host.Size = new System.Drawing.Size(136, 22);
			this.mGroup1_Host.Text = "按主机分组";
			// 
			// mGroup2
			// 
			this.mGroup2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mGroup2_None,
            this.mGroup2_User,
            this.mGroup2_IP,
            this.mGroup2_Host});
			this.mGroup2.Name = "mGroup2";
			this.mGroup2.Size = new System.Drawing.Size(124, 22);
			this.mGroup2.Text = "第二分组";
			// 
			// mGroup2_None
			// 
			this.mGroup2_None.Name = "mGroup2_None";
			this.mGroup2_None.Size = new System.Drawing.Size(136, 22);
			this.mGroup2_None.Text = "不启用";
			// 
			// mGroup2_User
			// 
			this.mGroup2_User.Name = "mGroup2_User";
			this.mGroup2_User.Size = new System.Drawing.Size(136, 22);
			this.mGroup2_User.Text = "自定义分组";
			// 
			// mGroup2_IP
			// 
			this.mGroup2_IP.Name = "mGroup2_IP";
			this.mGroup2_IP.Size = new System.Drawing.Size(136, 22);
			this.mGroup2_IP.Text = "按IP分组";
			// 
			// mGroup2_Host
			// 
			this.mGroup2_Host.Name = "mGroup2_Host";
			this.mGroup2_Host.Size = new System.Drawing.Size(136, 22);
			this.mGroup2_Host.Text = "按主机组";
			// 
			// mSepGroup
			// 
			this.mSepGroup.Name = "mSepGroup";
			this.mSepGroup.Size = new System.Drawing.Size(121, 6);
			// 
			// mSort1
			// 
			this.mSort1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSort1_IP,
            this.mSort1_State,
            this.mSort1_User});
			this.mSort1.Name = "mSort1";
			this.mSort1.Size = new System.Drawing.Size(124, 22);
			this.mSort1.Text = "第一排序";
			// 
			// mSort1_IP
			// 
			this.mSort1_IP.Name = "mSort1_IP";
			this.mSort1_IP.Size = new System.Drawing.Size(160, 22);
			this.mSort1_IP.Text = "按IP排序";
			// 
			// mSort1_State
			// 
			this.mSort1_State.Name = "mSort1_State";
			this.mSort1_State.Size = new System.Drawing.Size(160, 22);
			this.mSort1_State.Text = "按在线状态排序";
			// 
			// mSort1_User
			// 
			this.mSort1_User.Name = "mSort1_User";
			this.mSort1_User.Size = new System.Drawing.Size(160, 22);
			this.mSort1_User.Text = "按用户名排序";
			// 
			// mSort2
			// 
			this.mSort2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mSort2_IP,
            this.mSort2_State,
            this.mSort2_User});
			this.mSort2.Name = "mSort2";
			this.mSort2.Size = new System.Drawing.Size(124, 22);
			this.mSort2.Text = "第二排序";
			// 
			// mSort2_IP
			// 
			this.mSort2_IP.Name = "mSort2_IP";
			this.mSort2_IP.Size = new System.Drawing.Size(160, 22);
			this.mSort2_IP.Text = "按IP排序";
			// 
			// mSort2_State
			// 
			this.mSort2_State.Name = "mSort2_State";
			this.mSort2_State.Size = new System.Drawing.Size(160, 22);
			this.mSort2_State.Text = "按在线状态排序";
			// 
			// mSort2_User
			// 
			this.mSort2_User.Name = "mSort2_User";
			this.mSort2_User.Size = new System.Drawing.Size(160, 22);
			this.mSort2_User.Text = "按用户名排序";
			// 
			// tSepFunc
			// 
			this.tSepFunc.Name = "tSepFunc";
			this.tSepFunc.Size = new System.Drawing.Size(6, 25);
			// 
			// tMute
			// 
			this.tMute.CheckOnClick = true;
			this.tMute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tMute.Image = ((System.Drawing.Image)(resources.GetObject("tMute.Image")));
			this.tMute.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tMute.Name = "tMute";
			this.tMute.Size = new System.Drawing.Size(23, 22);
			this.tMute.Text = "屏蔽所有消息";
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
			// HostView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tbHost);
			this.Controls.Add(this.hostList);
			this.Name = "HostView";
			this.Size = new System.Drawing.Size(284, 262);
			this.tbHost.ResumeLayout(false);
			this.tbHost.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private IPMessagerNet.UI.Controls.HostTreeView.HostTreeView hostList;
		private System.Windows.Forms.ToolStrip tbHost;
		private System.Windows.Forms.ToolStripDropDownButton tState;
		private System.Windows.Forms.ToolStripMenuItem mStateOnline;
		private System.Windows.Forms.ToolStripMenuItem mStateAbsence;
		private System.Windows.Forms.ToolStripSeparator tolStateAbsSep;
		private System.Windows.Forms.ToolStripTextBox mStateAbsenceMsg;
		private System.Windows.Forms.ToolStripSeparator tSepState;
		private System.Windows.Forms.ToolStripButton tRefresh;
		private System.Windows.Forms.ToolStripButton tSearch;
		private System.Windows.Forms.ToolStripButton tMute;
		private System.Windows.Forms.ToolStripDropDownButton tGroupSort;
		private System.Windows.Forms.ToolStripMenuItem mGroup1;
		private System.Windows.Forms.ToolStripMenuItem mGroup1_None;
		private System.Windows.Forms.ToolStripMenuItem mGroup1_IP;
		private System.Windows.Forms.ToolStripMenuItem mGroup1_Host;
		private System.Windows.Forms.ToolStripMenuItem mGroup2;
		private System.Windows.Forms.ToolStripMenuItem mGroup2_None;
		private System.Windows.Forms.ToolStripMenuItem mGroup2_IP;
		private System.Windows.Forms.ToolStripMenuItem mGroup2_Host;
		private System.Windows.Forms.ToolStripSeparator mSepGroup;
		private System.Windows.Forms.ToolStripMenuItem mSort1;
		private System.Windows.Forms.ToolStripMenuItem mSort1_IP;
		private System.Windows.Forms.ToolStripMenuItem mSort1_State;
		private System.Windows.Forms.ToolStripMenuItem mSort1_User;
		private System.Windows.Forms.ToolStripMenuItem mSort2;
		private System.Windows.Forms.ToolStripMenuItem mSort2_IP;
		private System.Windows.Forms.ToolStripMenuItem mSort2_State;
		private System.Windows.Forms.ToolStripMenuItem mSort2_User;
		private System.Windows.Forms.ToolStripDropDownButton tChat;
		private System.Windows.Forms.ToolStripMenuItem mChat;
		private System.Windows.Forms.ToolStripSeparator mSepChat;
		private System.Windows.Forms.ToolStripMenuItem mBan;
		private System.Windows.Forms.ToolStripMenuItem mDial;
		private System.Windows.Forms.ToolStripMenuItem mGroup;
		private System.Windows.Forms.ToolStripMenuItem mGroup1_User;
		private System.Windows.Forms.ToolStripMenuItem mGroup2_User;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem mViewFloat;
		private System.Windows.Forms.ToolStripSeparator tSepFunc;
	}
}