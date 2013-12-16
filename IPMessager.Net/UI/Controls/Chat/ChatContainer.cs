using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;
using FSLib.IPMessager.Core;
using IPMessagerNet.API;


namespace IPMessagerNet.UI.Controls.Chat
{
	public class ChatContainer : TabControl
	{
		List<IChatService> chats;


		public ChatContainer()
		{
			this.Dock = DockStyle.Fill;
			this.DoubleClick += ChatContainer_DoubleClick;

			if (Env.ClientConfig == null) return;
			Init();
		}

		#region 初始化

		//初始化
		void Init()
		{
			chats = new List<IChatService>();

			InitSelfEvents();
		}

		//绑定内部事件
		void InitSelfEvents()
		{
			Env.IPMClient.OnlineHost.HostOffline += (s, e) => { RemoveHost(e.Host); };
			Env.IPMClient.OnlineHost.HostCleared += (s, e) => { ClearHost(); };
			Env.IPMClient.Commander.TextMessageReceived += (s, e) =>
			{
				if (e.IsHandled) return;
				TextMessageReceived(e.Host, e.Message);
				e.IsHandled = true;
			};
			Env.IPMClient.FileTaskManager.SendTaskAdded += (s, e) => SendTaskAdd(e);
			Env.IPMClient.FileTaskManager.TaskExpires += (s, e) =>
			{
				IChatService cs = this.OpenChatTab(e.Host, false);
				if (cs == null) new Dialogs.Notify.FileShare.SendTaskExpires(e.TaskInfo).ShowDialog(); //如果标签没有打开,就显示弹出窗口
				else cs.SendTaskExpires(e);	//否则在聊天窗口内显示
			};
			Env.IPMClient.FileTaskManager.SendItemStart += (s, e) => OpenChatTab(e.Host, false, f => f.SendTaskStart(e));
			Env.IPMClient.FileTaskManager.SendTaskFinished += (s, e) => OpenChatTab(e.Host, false, f =>
			{
				f.SendTaskFinish(e);
				//SOUND
				if (!Forms.FrameContainer.ContainerForm.IsMute && Env.ClientConfig.Sound.EnableFileSuccSound) Env.SoundManager.PlayFileSucc();
			});
			Env.IPMClient.FileTaskManager.ReceiveTaskAdded += (s, e) => OpenChatTab(e.Host, true, f => f.AddReceiveTask(e));
			Env.IPMClient.FileTaskManager.ReceiveTaskFinished += (s, e) => OpenChatTab(e.Host, false, f =>
			{
				f.ReceiveTaskFinish(e);
				//SOUND
				if (!Forms.FrameContainer.ContainerForm.IsMute && Env.ClientConfig.Sound.EnableFileSuccSound) Env.SoundManager.PlayFileSucc();
			});
			Env.IPMClient.FileTaskManager.ReceiveItemStart += (s, e) => OpenChatTab(e.Host, false, f => f.ReceiveTaskStart(e));
			Env.IPMClient.FileTaskManager.TaskItemProgressChanged += (s, e) => OpenChatTab(e.Host, false, f => f.TaskItemProgressChange(e));
			Env.IPMClient.FileTaskManager.TaskItemStateChanged += (s, e) => OpenChatTab(e.Host, false, f => f.TaskItemStateChange(e));
			Env.IPMClient.FileTaskManager.ReleaseFileRequired += (s, e) =>
			{
				if (!OpenChatTab(e.Host, false, f => f.SendTaskDiscard(e))) new Dialogs.Notify.FileShare.SendTaskRelease(e.TaskInfo).ShowDialog();
			};
			Env.IPMClient.FileTaskModule.FileSystemOperationError += (s, e) =>
			{
				OpenChatTab(e.Host, false, f => f.FileOperationError(e));
			};
			Env.IPMClient.FileTaskManager.FileReceiveTaskReDroped += (s, e) =>
			{
				AddReceiveTask(e.TaskInfo.RemoteHost, e.TaskInfo);
				e.IsHandled = true;
			};
			Env.IPMClient.FileTaskManager.FileReceived += FileTaskManager_FileReceived;
		}

		/// <summary>
		/// 收到了文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FileTaskManager_FileReceived(object sender, FileReceivedEventArgs e)
		{
			e.File.TaskList.ForEach(s => s.FullPath = Env.ClientConfig.FunctionConfig.Share_LastSelectedPath);
			AddReceiveTask(e.Host, e.File);
		}


		/// <summary>
		/// 处理发送任务添加事件
		/// </summary>
		/// <param name="task"></param>
		void SendTaskAdd(FileTaskEventArgs task)
		{
			IChatService cs = this.OpenChatTab(task.Host);

			cs.AddSendTask(task);
		}

		/// <summary>
		/// 处理文件请求
		/// </summary>
		/// <param name="host"></param>
		/// <param name="task"></param>
		private void AddReceiveTask(Host host, FileTaskInfo task)
		{
			OpenChatTab(host, true, s =>
								{
									s.ReceiveFileRequired(task);
									if (Env.ClientConfig.ChatConfig.AutoChangeCurrentTabToNew)
									{
										this.SelectTab(s as TabPage);
									}
								});
			//SOUND
			if (!Forms.FrameContainer.ContainerForm.IsMute && Env.ClientConfig.Sound.EnableNewFileSound) Env.SoundManager.PlayNewFile();
		}

		/// <summary>
		/// 处理信息到达事件
		/// </summary>
		/// <param name="host"></param>
		/// <param name="msg"></param>
		void TextMessageReceived(Host host, FSLib.IPMessager.Entity.Message msg)
		{
			IChatService cs = this.OpenChatTab(host);
			cs.DropMessage(msg);
			if (Env.ClientConfig.ChatConfig.AutoChangeCurrentTabToNew)
			{
				this.SelectTab(cs as TabPage);
			}
			//SOUND
			if (!Forms.FrameContainer.ContainerForm.IsMute && Env.ClientConfig.Sound.EnableNewMsgSound) Env.SoundManager.PlayNewMsg();
		}

		/// <summary>
		/// 从当前会话中删除主机
		/// </summary>
		/// <param name="host">主机</param>
		void RemoveHost(Host host)
		{
			foreach (TabPage p in this.TabPages)
			{
				IChatService cs = p as IChatService;

				if (cs == null || cs.Host != host) continue;
				cs.RemoveHost();
				//从标签页中移除
				this.TabPages.Remove(p);
				//从集合中删除
				chats.Remove(cs);
				OnSessionCountChanged(new EventArgs());

				break;
			}
		}

		/// <summary>
		/// 将列表中的主机全部清除
		/// </summary>
		void ClearHost()
		{
			chats.ForEach(s => s.RemoveHost());
			chats.Clear();

			for (int i = this.TabPages.Count - 1; i >= 0; i--)
			{
				TabPage p = this.TabPages[i];

				//不是主机会话框则继续
				if (!(p is IChatService)) continue;

				//从标签页中移除
				CloseTab(p as IChatService);
			}
			OnSessionCountChanged(new EventArgs());
		}

		//关闭会话
		void ChatContainer_DoubleClick(object sender, EventArgs e)
		{
			if (this.SelectedTab == null) return;
			IChatService cs = this.SelectedTab as IChatService;
			if (cs == null) return;
			CloseTab(cs);

			OnSessionCountChanged(new EventArgs());
		}

		/// <summary>
		/// 关闭标签页
		/// </summary>
		/// <param name="cs"></param>
		void CloseTab(IChatService cs)
		{
			//反绑定事件
			cs.FileSendRequest -= cs_FileSendRequest;
			cs.TaskDiscardRequired -= cs_TaskDiscardRequired;
			cs.TaskAccepted -= cs_TaskAccepted;

			cs.RemoveHost();
			chats.Remove(cs);
			this.TabPages.Remove(cs as TabPage);

			//padding receive task
			if (cs.PaddingReceiveTask != null) cs.PaddingReceiveTask.ForEach(s => Env.IPMClient.Commander.SendReleaseFilesSignal(cs.Host, s.PackageID));
		}

		#endregion

		#region 属性

		/// <summary>
		/// 返回当前会话的数目
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public int SessionCount { get { return chats == null ? 0 : chats.Count; } }

		/// <summary>
		/// 返回选定的聊天对象
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public IChatService SelectedChatPage { get { return SelectedTab as IChatService; } }


		#endregion

		#region 公共函数

		/// <summary>
		/// 打开页面标签
		/// </summary>
		/// <param name="host"></param>
		public IChatService OpenChatTab(Host host)
		{
			return OpenChatTab(host, true);
		}

		/// <summary>
		/// 打开页面标签
		/// </summary>
		public bool OpenChatTab(Host host, bool openNew, Action<IChatService> action)
		{
			IChatService cs = OpenChatTab(host, openNew);
			if (cs != null && action != null) action(cs);

			return cs != null;
		}

		/// <summary>
		/// 打开页面标签
		/// </summary>
		public bool OpenChatTab(Host host, Action<IChatService> action)
		{
			return OpenChatTab(host, true, action);
		}

		/// <summary>
		/// 打开页面标签
		/// </summary>
		/// <param name="host"></param>
		public IChatService OpenChatTab(Host host, bool createNew)
		{
			IChatService cs = chats.SingleOrDefault(s => s.Host == host);

			if (cs == null && createNew)
			{
				//初始化
				cs = new IEView.IETabPage();
				cs.BindHost(host);
				//绑定事件
				cs.FileSendRequest += cs_FileSendRequest;
				cs.TaskDiscardRequired += cs_TaskDiscardRequired;
				cs.TaskAccepted += cs_TaskAccepted;

				this.TabPages.Add(cs as TabPage);
				chats.Add(cs);
				OnSessionCountChanged(new EventArgs());
			}

			return cs;
		}

		void cs_TaskAccepted(object sender, FileTaskEventArgs e)
		{
			FileTaskInfo taskinfo = e.TaskInfo;
			e.TaskInfo.TaskList.ForEach(s =>
			{
				if (!s.IsFolder) s.FullPath = System.IO.Path.Combine(s.FullPath, s.Name);
			});

			Env.IPMClient.FileTaskManager.AddReceiveTask(e.TaskInfo);
		}

		void cs_TaskDiscardRequired(object sender, FileTaskEventArgs e)
		{
			Env.IPMClient.Commander.SendReleaseFilesSignal((sender as IChatService).Host, e.PackageID);
		}

		public void MessageSend(string content)
		{
			if (this.SelectedChatPage == null) return;

			this.SelectedChatPage.SendMessage(content);
		}

		#endregion


		#region 事件

		/// <summary>
		/// 会话数量变化
		/// </summary>
		public event EventHandler SessionCountChanged;


		protected virtual void OnSessionCountChanged(EventArgs e)
		{
			if (SessionCountChanged != null) SessionCountChanged(this, e);
		}

		#endregion

		#region 标签内部事件


		//捕捉文件发送请求
		void cs_FileSendRequest(object sender, EventArgs e)
		{
			IChatService tab = sender as IChatService;
			string[] list = tab.FileSendList.ToArray();
			tab.ClearFileSendList();

			//创建任务
			Dialogs.Notify.FetchFolderInfo fdi = new IPMessagerNet.UI.Dialogs.Notify.FetchFolderInfo()
			{
				FileList = list,
				CalculateFolder = Env.ClientConfig.FunctionConfig.Share_CalculateFolderSize
			};
			fdi.ShowDialog();

			Env.IPMClient.PerformSendFile(fdi.TaskItems, null, false, false, tab.Host);
		}


		#endregion

	}
}
