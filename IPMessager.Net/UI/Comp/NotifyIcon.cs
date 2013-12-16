using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace IPMessagerNet.UI.Comp
{
	class NotifyIconManager
	{
		public enum Status : int
		{
			Online,
			Mute,
			NewMessage,
			Absence
		}

		/// <summary>
		/// 界面中的NotifyIcon
		/// </summary>
		public NotifyIcon NotifyIcon { get; set; }

		private Status _currentStatus;
		/// <summary>
		/// 当前状态
		/// </summary>
		public Status CurrentStatus
		{
			get
			{
				return _currentStatus;
			}
			set
			{
				_currentStatus = value;
				switch (_currentStatus)
				{
					case Status.Online:
						NotifyIcon.Icon = IcoOnline;
						break;
					case Status.Mute:
						NotifyIcon.Icon = IcoMute;
						break;
					case Status.Absence:
						NotifyIcon.Icon = IcoAbsence;
						break;
					case Status.NewMessage:
						NotifyIcon.Icon = IcoNewMessage;
						break;
				}
			}
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
				CurrentStatus = Status.Online;

				NotifyIcon.Visible = true;
			}
		}

		//初始化使用的资源
		void InitResources()
		{
			IcoOnline = Core.ProfileManager.GetThemeIcon("NotifyIcon", "Online");
			IcoAbsence = Core.ProfileManager.GetThemeIcon("NotifyIcon", "Absence");
			IcoNewMessage = Core.ProfileManager.GetThemeIcon("NotifyIcon", "NewMessage");
			IcoMute = Core.ProfileManager.GetThemeIcon("NotifyIcon", "Mute");

			System.Windows.Forms.Timer t = new Timer(NotifyIcon.Container);
			t.Interval = 1000;

			t.Tick += (s, e) =>
			{
				if (CurrentStatus != Status.NewMessage) return;
				if (NotifyIcon.Icon == IcoNewMessage)
				{
					switch (CurrentStatus)
					{
						case Status.Online:
							NotifyIcon.Icon = IcoOnline;
							break;
						case Status.Mute:
							NotifyIcon.Icon = IcoMute;
							break;
						case Status.Absence:
							NotifyIcon.Icon = IcoAbsence;
							break;
					}
				}
				else
					NotifyIcon.Icon = IcoNewMessage;
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

			ToolStripMenuItem showMainForm = new ToolStripMenuItem(Core.ProfileManager.GetThemePicturePath("Icon", "MainForm"), "显示主窗口");
			showMainForm.Click += (s, e) => { OnShowMainForm(); };
			ctxmenu.Items.Add(showMainForm);
		}

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

		#endregion
	}
}
