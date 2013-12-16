using System;
using System.Drawing;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;
using IPMessagerNet._Embed;


namespace IPMessagerNet.UI.Controls
{
	public partial class HostView : UserControl
	{
		public HostView()
		{
			InitializeComponent();
			InitInterface();
			InitEvents();
		}

		#region 属性

		/// <summary>
		/// 当前活动主机
		/// </summary>
		public Host SelectedHost { get { return hostList.SelectedHost; } }

		/// <summary>
		/// 主机列表
		/// </summary>
		public Controls.HostTreeView.HostTreeView HostTreeView { get { return hostList; } }

		/// <summary>
		/// 是否在静音状态
		/// </summary>
		public bool IsInMuteMode
		{
			get
			{
				return tMute.Checked;
			}
			set
			{
				tMute.Checked = value;
			}
		}

		/// <summary>
		/// 是否处于浮动模式
		/// </summary>
		public bool IsInFloatMode
		{
			get
			{
				return mViewFloat.Checked;
			}
			set
			{
				mViewFloat.Checked = value;
			}
		}

		#endregion

		#region 事件

		/// <summary>
		/// 安静状态变化
		/// </summary>
		public event EventHandler MuteModeChanged;

		/// <summary>
		/// 触发安静状态变化事件
		/// </summary>
		/// <param name="e">参数</param>
		protected virtual void OnMuteModeChanged(EventArgs e) { if (MuteModeChanged != null)MuteModeChanged(this, new EventArgs()); }

		/// <summary>
		/// 浮动模式变化
		/// </summary>
		public event EventHandler FloatModeChanged;

		/// <summary>
		/// 触发浮动模式变化事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFloatModeChanged(EventArgs e) { if (FloatModeChanged != null)FloatModeChanged(this, e); }

		/// <summary>
		/// 离开状态信息变化
		/// </summary>
		public event EventHandler AbsenceMessageChanged;

		/// <summary>
		/// 触发离开状态信息变化事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnAbsenceMenssageChanged(EventArgs e)
		{
			if (AbsenceMessageChanged != null) AbsenceMessageChanged(this, e);
		}

		/// <summary>
		/// 在线状态变化
		/// </summary>
		public event EventHandler StatusChanged;

		/// <summary>
		/// 触发在线状态变化事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnStatusChanged(EventArgs e)
		{
			if (StatusChanged != null) StatusChanged(this, e);
		}

		/// <summary>
		/// 请求聊天
		/// </summary>
		public event EventHandler ChatActionRequired;

		protected virtual void OnChatActionRequired(EventArgs e)
		{
			if (ChatActionRequired != null) ChatActionRequired(this, e);
		}



		#endregion

		#region 界面处理

		/// <summary>
		/// 工具栏图标数组
		/// </summary>
		Image[] toolbarImageArray;

		/// <summary>
		/// 初始化界面
		/// </summary>
		void InitInterface()
		{
			//--------------------工具栏--------------------
			int tmp;
			toolbarImageArray = ImageHelper.SplitImage(Core.ProfileManager.GetThemeFilePath("Toolbar", "HostViewToolBar"), 16, 16, out tmp).ToArray();
			tmp = 0;
			ControlHelper.FillMenuButtonImage(tbHost, toolbarImageArray, ref tmp);

			//离开菜单
			BindAbsenceMessageMenu();
			if (Env.IPMClient.Config.IsInAbsenceMode)
			{
				tState.Image = toolbarImageArray[2];
				tState.ToolTipText = Env.IPMClient.Config.AbsenceMessage;
				mStateOnline.Checked = false;
				mStateAbsence.Checked = true;
			}
			else
			{
				mStateOnline.Checked = true;
			}

			//检测内置插件状态
			this.mBan.Enabled = Env.IPMClient.IsInnerServiceEnabled(FSLib.IPMessager.Services.InnerService.BlackListBlocker);

			//排序方式
			SwitchGroup(0, Env.ClientConfig.HostListViewConfig.FirstGroupType, false);
			SwitchGroup(1, Env.ClientConfig.HostListViewConfig.SecondGroupType, false);
			SwitchOrder(0, Env.ClientConfig.HostListViewConfig.FirstOrder, false);
			SwitchOrder(1, Env.ClientConfig.HostListViewConfig.SecondOrder, false);
		}

		/// <summary>
		/// 刷新离开状态菜单
		/// </summary>
		public void BindAbsenceMessageMenu()
		{
			//清除已经有的菜单
			while (this.mStateAbsence.DropDownItems.Count > 2) this.mStateAbsence.DropDownItems.RemoveAt(0);

			foreach (var item in Env.ClientConfig.AbsenceMessage)
			{
				ToolStripMenuItem tsmi = new ToolStripMenuItem()
				{
					Text = item
				};
				this.mStateAbsence.DropDownItems.Insert(mStateAbsence.DropDownItems.Count - 2, tsmi);
			}
			EventHandler absModeClick = (s, e) => { SwitchToAbsence((s as ToolStripMenuItem).Text); };
			foreach (var item in this.mStateAbsence.DropDownItems)
			{
				if (item is ToolStripMenuItem) (item as ToolStripMenuItem).Click += absModeClick;
			}
		}
		#endregion


		#region UI事件处理

		/// <summary>
		/// 初始化事件
		/// </summary>
		void InitEvents()
		{
			InitMenuEvents();
			BindMenuEvents();

			this.Load += (s, e) => { Env.IPMClient.Online(); };

		}

		/// <summary>
		/// 处理菜单本身显示的操作
		/// </summary>
		void InitMenuEvents()
		{

		}

		/// <summary>
		/// 绑定菜单处理函数
		/// </summary>
		void BindMenuEvents()
		{
			//在线
			mStateOnline.Click += (s, e) => { SwitchToOnLine(); };
			//自定义离开消息
			mStateAbsenceMsg.KeyPress += (s, e) =>
			{
				if (e.KeyChar != '\r') return;
				tState.HideDropDown();
				if (string.IsNullOrEmpty(mStateAbsenceMsg.TextBox.Text)) return;
				string str = mStateAbsenceMsg.TextBox.Text;
				Env.ClientConfig.AbsenceMessage.Add(str);
				mStateAbsenceMsg.TextBox.Text = "";
				ToolStripMenuItem tsmi = new ToolStripMenuItem(str);
				tsmi.Click += (g, h) => { SwitchToAbsence((g as ToolStripMenuItem).Text); }; ;
				SwitchToAbsence(str);
				this.mStateAbsence.DropDownItems.Insert(mStateAbsence.DropDownItems.Count - 2, tsmi);
				e.Handled = true;
				OnAbsenceMenssageChanged(new EventArgs());
			};
			//浮动模式
			mViewFloat.CheckedChanged += (s, e) => { OnFloatModeChanged(e); };
			//刷新
			tRefresh.Click += (s, e) =>
			{
				Env.IPMClient.OnlineHost.Clear();
				Env.IPMClient.Online();
			};
			this.mChat.Click += (s, e) => hostList.OpenChatPage();		//打开聊天对话框
			this.mBan.Click += (s, e) => hostList.BanHost(true);			//屏蔽主机
			this.mDial.Click += (s, e) => hostList.DialUpHost(true);		//保持列表

			//排序分组模式
			Action<int, ToolStripMenuItem> menuGroupEventBinder = (d, s) =>
			{
				for (int i = 0; i < s.DropDownItems.Count; i++)
				{
					int m = i;
					(s.DropDownItems[i] as ToolStripMenuItem).Click += (e, f) => { SwitchGroup(d, (UI.Controls.HostTreeView.ListConfig.GroupType)m, true); };
				}
			};
			menuGroupEventBinder(0, this.mGroup1);
			menuGroupEventBinder(1, this.mGroup2);

			Action<int, ToolStripMenuItem> menuSortEventBinder = (d, s) =>
			{
				for (int i = 0; i < s.DropDownItems.Count; i++)
				{
					int m = i;
					(s.DropDownItems[i] as ToolStripMenuItem).Click += (e, f) => { SwitchOrder(d, (UI.Controls.HostTreeView.ListConfig.SortOrder)m, true); };
				}
			};
			menuSortEventBinder(0, this.mSort1);
			menuSortEventBinder(1, this.mSort2);
			//列表请求聊天
			hostList.ChatActionRequired += (s, e) => { OnChatActionRequired(e); };
			//静音状态
			tMute.CheckedChanged += (s, e) => { OnMuteModeChanged(e); };
		}

		/// <summary>
		/// 切换排序模式
		/// </summary>
		/// <param name="orderLevel">排序级别</param>
		/// <param name="orderType">排序方式</param>
		/// <param name="refreshDisplay">是否刷新显示</param>
		public void SwitchOrder(int orderLevel, Controls.HostTreeView.ListConfig.SortOrder orderType, bool refreshDisplay)
		{
			if (orderLevel < 0 || orderLevel > 1) return;

			if (refreshDisplay && ((orderLevel == 0 && Env.ClientConfig.HostListViewConfig.FirstOrder == orderType)
				|| (orderLevel == 1 && Env.ClientConfig.HostListViewConfig.SecondOrder == orderType))) return;

			ToolStripMenuItem mnu = null;
			switch (orderLevel)
			{
				case 0:
					mnu = mSort1;
					Env.ClientConfig.HostListViewConfig.FirstOrder = orderType;
					break;
				case 1:
					mnu = mSort2;
					Env.ClientConfig.HostListViewConfig.SecondOrder = orderType;
					break;
			}
			//先清除所有勾选状态
			foreach (ToolStripMenuItem item in mnu.DropDownItems) item.Checked = false;
			//再选中
			(mnu.DropDownItems[(int)orderType] as ToolStripMenuItem).Checked = true;

			//切换显示模式
			if (refreshDisplay) RefreshList();
		}

		/// <summary>
		/// 切换分组模式
		/// </summary>
		/// <param name="groupLevel">排序级别</param>
		/// <param name="groupType">排序方式</param>
		/// <param name="refreshDisplay">是否刷新显示</param>
		public void SwitchGroup(int groupLevel, Controls.HostTreeView.ListConfig.GroupType groupType, bool refreshDisplay)
		{
			if (groupLevel < 0 || groupLevel > 1) return;

			if (refreshDisplay && ((groupLevel == 0 && Env.ClientConfig.HostListViewConfig.FirstGroupType == groupType)
				|| (groupLevel == 1 && Env.ClientConfig.HostListViewConfig.SecondGroupType == groupType))) return;

			ToolStripMenuItem mnu = null;
			switch (groupLevel)
			{
				case 0:
					mnu = this.mGroup1;
					Env.ClientConfig.HostListViewConfig.FirstGroupType = groupType;
					break;
				case 1:
					mnu = this.mGroup2;
					Env.ClientConfig.HostListViewConfig.SecondGroupType = groupType;
					break;
			}
			//先清除所有勾选状态
			foreach (ToolStripMenuItem item in mnu.DropDownItems) item.Checked = false;
			//再选中
			(mnu.DropDownItems[(int)groupType] as ToolStripMenuItem).Checked = true;

			//这里需要额外处理下
			if (groupLevel == 0) this.mGroup2.Enabled = groupType != IPMessagerNet.UI.Controls.HostTreeView.ListConfig.GroupType.None;	//切换第二分组是否可用，如果第一分组都没有用，那第二分组必然都不可用
			//只能有一个自定义
			if (groupType == IPMessagerNet.UI.Controls.HostTreeView.ListConfig.GroupType.UserDefine)
			{
				if (groupLevel == 0) this.mGroup2_User.Enabled = false;
				else if (groupLevel == 1) this.mGroup1_User.Enabled = false;
			}


			//切换显示模式
			if (refreshDisplay) RefreshList();
		}

		/// <summary>
		/// 切换到在线模式
		/// </summary>
		public void SwitchToOnLine()
		{
			if (!Env.IPMClient.Config.IsInAbsenceMode) return;
			Env.IPMClient.ChangeAbsenceMode(false, "");
			tState.Image = toolbarImageArray[1];
			tState.ToolTipText = "我在线上";
			mStateOnline.Checked = true;
			mStateAbsence.Checked = false;

			OnStatusChanged(new EventArgs());
		}

		/// <summary>
		/// 切换到离开模式
		/// </summary>
		public void SwitchToAbsence(string message)
		{
			if (Env.IPMClient.Config.IsInAbsenceMode) Env.IPMClient.ChangeAbsenceMode(false, "");
			Env.IPMClient.ChangeAbsenceMode(true, message);
			tState.Image = toolbarImageArray[2];
			tState.ToolTipText = message;
			mStateOnline.Checked = false;
			mStateAbsence.Checked = true;

			OnStatusChanged(new EventArgs());
		}

		/// <summary>
		/// 刷新显示列表
		/// </summary>
		public void RefreshList()
		{
			hostList.ClearHost();
			foreach (Host item in Env.IPMClient.OnlineHost.Values)
			{
				hostList.AddHost(item);
			}
		}

		#endregion



	}
}
