using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using IPMessagerNet._Embed;

namespace IPMessagerNet.UI.Comp
{
	class NotifyIconManager
	{
		/// <summary>
		/// 界面中的NotifyIcon
		/// </summary>
		public NotifyIcon NotifyIcon { get; set; }

		/// <summary>
		/// 文字标头
		/// </summary>
		string textPrefix;

		private bool _hasNewMessage;
		/// <summary>
		/// 是否有新信息
		/// </summary>
		public bool HasNewMessage
		{
			get
			{
				return _hasNewMessage;
			}
			set
			{
				_hasNewMessage = value;
				RefreshIcon();
			}
		}

		private bool _isAbsence;
		/// <summary>
		/// 是否是隐身模式
		/// </summary>
		public bool IsAbsence
		{
			get
			{
				return _isAbsence;
			}
			set
			{
				_isAbsence = value;
				(NotifyIcon.ContextMenuStrip.Items[2] as ToolStripMenuItem).Checked = !value;
				(NotifyIcon.ContextMenuStrip.Items[3] as ToolStripMenuItem).Checked = value;

				RefreshIcon();
			}
		}

		/// <summary>
		/// 获得或设置是否处于静音状态
		/// </summary>
		public bool IsMute
		{
			get
			{
				return (NotifyIcon.ContextMenuStrip.Items[4] as ToolStripMenuItem).Checked;
			}
			set
			{
				(NotifyIcon.ContextMenuStrip.Items[4] as ToolStripMenuItem).Checked = value;
				RefreshIcon();
			}
		}

		/// <summary>
		/// 当前状态
		/// </summary>
		public void RefreshIcon()
		{
			if (IsAbsence)
			{
				NotifyIcon.Icon = IcoAbsence;
				(NotifyIcon.ContextMenuStrip.Items[3] as ToolStripMenuItem).Checked = true;
			}
			else if (IsMute)
			{
				NotifyIcon.Icon = IcoMute;
				(NotifyIcon.ContextMenuStrip.Items[4] as ToolStripMenuItem).Checked = true;
			}
			else
			{
				NotifyIcon.Icon = IcoOnline;
				(NotifyIcon.ContextMenuStrip.Items[2] as ToolStripMenuItem).Checked = true;
			}

			NotifyIcon.Text = string.Concat(textPrefix, IsAbsence ? "离开 " : "在线 ", IsMute ? "安静模式 " : "", HasNewMessage ? " 【有未打开消息】" : "");
		}



		/// <summary>
		/// 创建一个新的 NotifyIconManager 对象.
		/// </summary>
		public NotifyIconManager(NotifyIcon notifyIcon)
		{
			NotifyIcon = notifyIcon;

			if (Env.ClientConfig != null)
			{
				InitResources();
				RefreshIcon();

				NotifyIcon.Visible = true;
			}

			textPrefix = string.Format("{0} / {1}{2}", Env.ClientConfig.IPMClientConfig.NickName, Env.ClientConfig.IPMClientConfig.GroupName, Environment.NewLine);

			//绑定事件
			Env.IPMClient.OnlineHost.HostOnline += OnlineHost_HostOnline;
			Env.IPMClient.OnlineHost.HostOffline += OnlineHost_HostOffline;
			Env.IPMClient.OnlineHost.HostCleared += OnlineHost_HostCleared;
			initTime = DateTime.Now.AddSeconds(10);
		}

		#region 气泡提示

		DateTime initTime;

		void OnlineHost_HostCleared(object sender, EventArgs e)
		{
			//重置初始化时间防止太多提示
			initTime = DateTime.Now.AddSeconds(10);
		}

		void OnlineHost_HostOffline(object sender, FSLib.IPMessager.Entity.OnlineHost.HostEventArgs e)
		{
			if (DateTime.Now < initTime || (IsMute && Env.ClientConfig.HostInfo.DisableHostTipInQuite)) return;

			//SOUND
			if (!IsMute && Env.ClientConfig.Sound.EnableOfflineSound) Env.SoundManager.PlayOffline();

			if (
				Env.HostConfig.HostOfflineTip == IPMessagerNet.Config.HostBallonTip.None
				||
				(Env.HostConfig.HostOfflineTip == IPMessagerNet.Config.HostBallonTip.Special && !Env.HostConfig.OfflineTip.Contains(e.Host.HostSub.Ipv4Address.Address.ToString()))
				) return;

			NotifyIcon.ShowBalloonTip(1000,
				String.Format("主机 {0} 下线", Core.HostInfoManager.GetHostDisyplayName(e.Host)),
				string.Format("主机：{0}\r\n用户：{1}\r\n组名：{2}\r\nIP地址：{3}", Core.HostInfoManager.GetHostDisyplayName(e.Host), e.Host.NickName, e.Host.GroupName, e.Host.HostSub.Ipv4Address.Address.ToString()),
				ToolTipIcon.Info);
		}

		void OnlineHost_HostOnline(object sender, FSLib.IPMessager.Entity.OnlineHost.HostEventArgs e)
		{
			if (DateTime.Now < initTime || (IsMute && Env.ClientConfig.HostInfo.DisableHostTipInQuite)) return;

			//SOUND
			if (!IsMute && Env.ClientConfig.Sound.EnableOnlineSound) Env.SoundManager.PlayOnline();

			if (
				Env.HostConfig.HostOnlineTip == IPMessagerNet.Config.HostBallonTip.None
				||
				(Env.HostConfig.HostOnlineTip == IPMessagerNet.Config.HostBallonTip.Special && !Env.HostConfig.OnlineTip.Contains(e.Host.HostSub.Ipv4Address.Address.ToString()))
				) return;

			NotifyIcon.ShowBalloonTip(1000,
				String.Format("主机 {0} 上线", Core.HostInfoManager.GetHostDisyplayName(e.Host)),
				string.Format("主机：{0}\r\n用户：{1}\r\n组名：{2}\r\nIP地址：{3}", Core.HostInfoManager.GetHostDisyplayName(e.Host), e.Host.NickName, e.Host.GroupName, e.Host.HostSub.Ipv4Address.Address.ToString()),
				ToolTipIcon.Info);
		}

		#endregion

		#region 界面操作

		//初始化使用的资源
		void InitResources()
		{
			IcoOnline = Core.ProfileManager.GetThemeIcon("NotifyIcon", "Online");
			IcoAbsence = Core.ProfileManager.GetThemeIcon("NotifyIcon", "Absence");
			IcoNewMessage = Core.ProfileManager.GetThemeIcon("NotifyIcon", "NewMessage");
			IcoMute = Core.ProfileManager.GetThemeIcon("NotifyIcon", "Mute");

			Icon[] iconArray = new Icon[] { IcoOnline, IcoAbsence, IcoNewMessage, IcoMute };

			//绑定菜单
			BindMenuToIcon();

			System.Windows.Forms.Timer t = new Timer(NotifyIcon.Container);
			t.Interval = 2000;

			t.Tick += (s, e) =>
			{
				int curIndex = Array.FindIndex(iconArray, (i) => i == NotifyIcon.Icon);
				int nextIndex = curIndex + 1;
				while (nextIndex != curIndex)
				{
					if (nextIndex >= iconArray.Length) nextIndex = 0;
					//循环查找下一个可显示图标
					if ((IsAbsence && nextIndex == 0) || (!IsAbsence && nextIndex == 1) || (!HasNewMessage && nextIndex == 2) || (!IsMute && nextIndex == 3))
					{
						nextIndex++;
						continue;
					}
					else
					{
						NotifyIcon.Icon = iconArray[nextIndex];
						break;
					}
				}
			};
			t.Enabled = true;

		}

		Icon IcoOnline, IcoAbsence, IcoNewMessage, IcoMute;

		/// <summary>
		/// 绑定菜单到图标
		/// </summary>
		void BindMenuToIcon()
		{
			ContextMenuStrip ctxmenu = new ContextMenuStrip(this.NotifyIcon.Container);
			int columnCount;
			List<Image> imgs = ImageHelper.SplitImage(Core.ProfileManager.GetThemePicture("Toolbar", "NotifyIconManager"), 16, 16, out columnCount);

			Func<Image, string, EventHandler, ToolStripMenuItem> buildMenu = (a, b, c) =>
			{
				ToolStripMenuItem temp = new ToolStripMenuItem(b, a);
				if (c != null) temp.Click += c;
				return temp;
			};

			ToolStripMenuItem showMainForm = new ToolStripMenuItem("显示主窗口(&S)", imgs[0]);
			showMainForm.Font = new Font(showMainForm.Font, FontStyle.Bold);
			showMainForm.Click += (s, e) => { OnShowMainForm(); };
			ctxmenu.Items.Add(showMainForm);

			ctxmenu.Items.Add(new ToolStripSeparator());	//分隔条
			ctxmenu.Items.Add(buildMenu(imgs[1], "我在线上", new EventHandler((s, e) => { OnStateChanged(new StateChangedEventArgs() { AbsenceMessage = "", IsInAbsenceMode = false }); })));

			ctxmenu.Items.Add(buildMenu(imgs[2], "离开", null));

			ToolStripMenuItem t = buildMenu(imgs[3], "安静状态", null);
			t.CheckedChanged += (s, e) => { OnMuteModeChanged(new EventArgs()); };
			t.CheckOnClick = true;
			ctxmenu.Items.Add(t);

			ctxmenu.Items.Add(new ToolStripSeparator());
			ctxmenu.Items.Add(buildMenu(imgs[4], "关于飞鸽传书.Net(&A)", new EventHandler((s, e) => { OnShowInfomation(new EventArgs()); })));
			ctxmenu.Items.Add(buildMenu(imgs[5], "退出(&X)", new EventHandler((s, e) => { OnAskQuit(new EventArgs()); })));

			NotifyIcon.ContextMenuStrip = ctxmenu;

			//离开子菜单
			RefreshAbsenceMessage();
		}

		public void RefreshAbsenceMessage()
		{
			ToolStripMenuItem menu = NotifyIcon.ContextMenuStrip.Items[3] as ToolStripMenuItem;
			menu.DropDownItems.Clear();

			Env.ClientConfig.AbsenceMessage.ForEach((s) =>
			{
				ToolStripMenuItem t = new ToolStripMenuItem(s);
				t.Click += (e, f) => { OnStateChanged(new StateChangedEventArgs() { IsInAbsenceMode = true, AbsenceMessage = (e as ToolStripMenuItem).Text }); };
				menu.DropDownItems.Add(t);
			});
		}

		#endregion

		#region 事件

		/// <summary>
		/// 要求显示主窗口
		/// </summary>
		public event EventHandler ShowMainForm;

		/// <summary>
		/// 触发要求显示主窗口事件
		/// </summary>
		protected virtual void OnShowMainForm()
		{
			if (ShowMainForm != null) ShowMainForm(this, new EventArgs());
		}

		public class StateChangedEventArgs : EventArgs
		{
			/// <summary>
			/// 是否是离开状态
			/// </summary>
			public bool IsInAbsenceMode { get; set; }

			/// <summary>
			/// 离开状态信息
			/// </summary>
			public string AbsenceMessage { get; set; }
		}

		/// <summary>
		/// 在线状态变化
		/// </summary>
		public event EventHandler<StateChangedEventArgs> StateChanged;

		protected virtual void OnStateChanged(StateChangedEventArgs e)
		{
			if (StateChanged != null) StateChanged(this, e);
		}

		/// <summary>
		/// 请求显示关于信息
		/// </summary>
		public event EventHandler ShowInfomation;

		/// <summary>
		/// 触发请求显示关于信息事件
		/// </summary>
		protected virtual void OnShowInfomation(EventArgs e)
		{
			if (ShowInfomation != null) ShowInfomation(this, e);
		}

		/// <summary>
		/// 请求退出事件
		/// </summary>
		public event EventHandler AskQuit;

		/// <summary>
		/// 触发请求退出事件
		/// </summary>
		/// <param name="e">事件参数</param>
		protected virtual void OnAskQuit(EventArgs e)
		{
			if (AskQuit != null) AskQuit(this, e);
		}


		/// <summary>
		/// 静音模式切换
		/// </summary>
		public event EventHandler MuteModeChanged;

		/// <summary>
		/// 触发静音模式切换事件
		/// </summary>
		/// <param name="e">参数</param>
		protected virtual void OnMuteModeChanged(EventArgs e)
		{
			if (MuteModeChanged != null) MuteModeChanged(this, e);
		}

		#endregion
	}
}
