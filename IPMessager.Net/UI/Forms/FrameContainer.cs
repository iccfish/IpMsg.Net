using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IPMessagerNet.UI.Forms
{
	public partial class FrameContainer : Base.DialogBase
	{
		#region 静态属性

		/// <summary>
		/// 静态主窗口
		/// </summary>
		public static FrameContainer ContainerForm { get; private set; }

		#endregion

		#region 公共属性

		/// <summary>
		/// Host列表
		/// </summary>
		public Controls.HostView HostView { get { return this.hostView; } }

		/// <summary>
		/// Host列表的树控件
		/// </summary>
		public Controls.HostTreeView.HostTreeView HostTreeView { get { return this.hostView.HostTreeView; } }

		/// <summary>
		/// 聊天区域
		/// </summary>
		public Controls.ChatArea ChatArea { get { return this.chat; } }

		#endregion

		public FrameContainer()
		{
			ContainerForm = this;
			Env.MainForm = this;

			InitializeComponent();
			if (Env.ClientConfig != null)
			{
				InitFrameContainer();
				InitTrayNotifyIcon();

				InitializeInterface();
				InitChatAreaEvents();
			};
		}


		#region 状态维护


		/// <summary>
		/// 获得或设置是否处于静音状态
		/// </summary>
		public bool IsMute
		{
			get
			{
				return hostView.IsInMuteMode;
			}
			set
			{
				if (hostView.IsInMuteMode == value) return;

				hostView.IsInMuteMode = value;
				notifyIconManager.IsMute = value;
			}
		}


		#endregion

		#region 主界面控制

		/// <summary>
		/// 初始化主窗口显示
		/// </summary>
		void InitFrameContainer()
		{
			this.ShowInTaskbar = true;

			FrameContainerConfig cfg = Env.ClientConfig.FrameContainerConfig;

			//还原状态
			if (cfg.WindowSize.Width > 0) this.Size = cfg.WindowSize;
			if (cfg.WindowState != -1) this.WindowState = (FormWindowState)cfg.WindowState;
			if (!cfg.Location.IsEmpty)
			{
				this.StartPosition = FormStartPosition.Manual;
				this.Location = cfg.Location;
			}
			if (cfg.HostListWidth > 0) this.mainContainer.SplitterDistance = cfg.HostListWidth;
			//if (cfg.ChatAreaWidth > 0) this.bodyContainer.SplitterDistance = cfg.ChatAreaWidth;

			//绑定属性，以便于记录状态
			this.LocationChanged += (s, e) => { if (this.WindowState != FormWindowState.Minimized)cfg.Location = this.Location; };
			this.SizeChanged += (s, e) =>
			{
				cfg.WindowState = (int)this.WindowState;
				if (this.WindowState == FormWindowState.Normal) cfg.WindowSize = this.Size;
			};
			this.mainContainer.SplitterMoved += (s, e) => { cfg.HostListWidth = this.mainContainer.SplitterDistance; };
			//this.bodyContainer.SplitterMoved += (s, e) => { cfg.ChatAreaWidth = this.bodyContainer.SplitterDistance; };
			this.smSystem_Exit.Click += (s, e) => ShutdownIPM();
			this.sm_System_Config.Click += (s, e) =>
			{
				new UI.Forms.SysConfig().ShowDialog();
			};


			//关闭到托盘
			this.FormClosing += (s, e) =>
			{
				if (Env.ClientConfig.FrameContainerConfig.MinimizeToTray && !isSystemOpClose)
				{
					e.Cancel = true;
					this.Visible = false;
				}
			};
		}

		/// <summary>
		/// 退出飞鸽传书
		/// </summary>
		public void ShutdownIPM()
		{
			if (isSystemOpClose == false)
			{
				isSystemOpClose = true;

				if (hostContainer != null) hostContainer.Close();
				//窗口挨个关闭,防止如果有模式窗体没有关闭的时候无法退出
				foreach (Form f in Application.OpenForms)
				{
					if (f != this) f.Close();
				}

				//关闭静态资源
				Env.Close();

				Application.Exit();
			}
		}

		/// <summary>
		/// 显示关于
		/// </summary>
		public static void ShowAbout()
		{
			new Dialogs.Notify.AboutIPM().ShowDialog();
		}

		/// <summary>
		/// 重写，以便于以隐藏模式启动
		/// </summary>
		/// <param name="value"></param>
		protected override void SetVisibleCore(bool value)
		{
			if (chat.SessionCount == 0 && hostView.IsInFloatMode) value = false;
			base.SetVisibleCore(value);
		}

		#endregion

		#region 主机列表部分界面的控制

		/// <summary>
		/// 浮动窗口
		/// </summary>
		ControlContainer hostContainer;

		/// <summary>
		/// 在线列表
		/// </summary>
		UI.Controls.HostView hostView;

		/// <summary>
		/// 初始化界面显示
		/// </summary>
		void InitializeInterface()
		{
			this.notifyIcon.Text = this.Text = Application.ProductName + " VER " + Application.ProductVersion;

			//初始化主机列表、设置事件
			hostView = new UI.Controls.HostView()
			{
				Dock = DockStyle.Fill,
				IsInFloatMode = Env.ClientConfig.FrameContainerConfig.HostListFloat
			};

			hostView.FloatModeChanged += (s, e) => { if (hostView.IsInFloatMode) { HostView_SwitchToFloat(); } else { HostView_SwitchToEmbedMode(); } };
			hostView.AbsenceMessageChanged += (s, e) => { notifyIconManager.RefreshAbsenceMessage(); };
			hostView.StatusChanged += (s, e) => { notifyIconManager.IsAbsence = Env.ClientConfig.IPMClientConfig.IsInAbsenceMode; };
			hostView.MuteModeChanged += (s, e) => { notifyIconManager.IsMute = hostView.IsInMuteMode; };

			//初始化状态
			notifyIconManager.IsMute = hostView.IsInMuteMode;
			notifyIconManager.IsAbsence = Env.ClientConfig.IPMClientConfig.IsInAbsenceMode;

			if (hostView.IsInFloatMode) HostView_SwitchToFloat();
			else HostView_SwitchToEmbedMode();


		}

		/// <summary>
		/// 切换到浮动模式
		/// </summary>
		void HostView_SwitchToFloat()
		{
			if (hostContainer == null)
			{
				hostContainer = new ControlContainer()
				{
					TopMost = Env.ClientConfig.FrameContainerConfig.HostListContainerTopMost,
					StartPosition = FormStartPosition.Manual,
					ShowInTaskbar = true
				};
				FrameContainerConfig cfg = Env.ClientConfig.FrameContainerConfig;

				//还原状态
				if (cfg.HostListContainerSize.Width > 0) hostContainer.Size = cfg.HostListContainerSize;
				if (cfg.HostListContainerWindowState != -1) hostContainer.WindowState = (FormWindowState)cfg.HostListContainerWindowState;
				if (!cfg.HostListContainerLocation.IsEmpty) hostContainer.Location = cfg.HostListContainerLocation;

				//绑定属性，以便于记录状态
				hostContainer.LocationChanged += (s, e) => { if (hostContainer.WindowState != FormWindowState.Minimized)cfg.HostListContainerLocation = hostContainer.Location; };
				hostContainer.SizeChanged += (s, e) =>
				{
					cfg.HostListContainerWindowState = (int)hostContainer.WindowState;
					if (hostContainer.WindowState != FormWindowState.Minimized) cfg.HostListContainerSize = hostContainer.Size;
				};
				hostContainer.TopMostChanged += (s, e) => { Env.ClientConfig.FrameContainerConfig.HostListContainerTopMost = hostContainer.TopMost; };
				hostContainer.FormClosing += (s, e) =>
				{
					if (!isSystemOpClose)
					{
						if (Env.ClientConfig.FrameContainerConfig.MinimizeToTray) { e.Cancel = true; hostContainer.Visible = false; }
						else { ShutdownIPM(); }
					}
					else
					{
						hostContainer.ClearControl();
					}
				};
			}
			hostContainer.EmbedControl(hostView);
			hostContainer.Show();
			this.mainContainer.Panel1Collapsed = true;
			Env.ClientConfig.FrameContainerConfig.HostListFloat = true;

			this.Visible = chat.SessionCount > 0;
		}

		/// <summary>
		/// 切换到嵌入模式
		/// </summary>
		void HostView_SwitchToEmbedMode()
		{
			if (hostContainer != null) hostContainer.Hide();
			hostView.Parent = this.mainContainer.Panel1;
			this.mainContainer.Panel1Collapsed = false;
			Env.ClientConfig.FrameContainerConfig.HostListFloat = false;
			this.Show();	//确认当前窗口可见
		}

		#endregion

		#region 中间聊天区域的界面控制

		//初始化这部分的事件
		void InitChatAreaEvents()
		{
			chat.SessionCountChanged += (s, e) =>
			{
				if (hostView.IsInFloatMode) this.Visible = chat.SessionCount > 0;
			};
			hostView.ChatActionRequired += (s, e) => { chat.ChatControl.OpenChatTab(hostView.SelectedHost); };
		}

		#endregion

		#region 托盘图标区域

		//托盘图标管理工具
		UI.Comp.NotifyIconManager notifyIconManager;
		//是否可以关闭
		bool isSystemOpClose = false;

		//初始化托盘图标
		void InitTrayNotifyIcon()
		{
			notifyIconManager = new IPMessagerNet.UI.Comp.NotifyIconManager(notifyIcon);

			//左键点击显示，右键点击菜单
			Action showForm = () =>
			{
				if (hostView.IsInFloatMode)
				{
					hostContainer.WindowState = FormWindowState.Normal;
					hostContainer.Visible = true;
				}
				else
				{
					this.ShowInTaskbar = true;
					this.Show();
				}
			};
			notifyIcon.MouseClick += (s, e) =>
			{
				if (e.Button == MouseButtons.Left) showForm();
			};
			notifyIconManager.AskQuit += (s, e) => { ShutdownIPM(); };
			notifyIconManager.MuteModeChanged += (s, e) => { IsMute = notifyIconManager.IsMute; };
			notifyIconManager.ShowInfomation += (s, e) => { ShowAbout(); };
			notifyIconManager.ShowMainForm += (s, e) => { showForm(); };
			notifyIconManager.StateChanged += (s, e) =>
			{
				if (e.IsInAbsenceMode)
				{
					hostView.SwitchToAbsence(e.AbsenceMessage);
				}
				else
				{
					hostView.SwitchToOnLine();
				}
			};
		}

		#endregion
	}
}
