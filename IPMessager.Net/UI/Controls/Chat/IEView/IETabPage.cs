using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FSLib.IPMessager.Entity;
using IPMessagerNet._Embed;
using Alias = FSLib.IPMessager.Entity;
using FSLib.IPMessager.Core;
using IPMessagerNet.API;
using IPMessagerNet.Utility;


namespace IPMessagerNet.UI.Controls.Chat.IEView
{
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]
	public class IETabPage : TabPage, IChatService
	{
		WebBrowserEx browser;

		System.Collections.ArrayList notReadyScriptList;

		public IETabPage()
		{
			notReadyScriptList = new System.Collections.ArrayList();
			InitializeWebBrowser();
			FileSendList = new List<string>();
		}

		#region 公开方法

		/// <summary>
		/// 绑定主机到TabPage
		/// </summary>
		/// <param name="host">主机对象</param>
		public void BindHost(Host host)
		{
			this.Host = host;

			host.AbsenceModeChanged += host_AbsenceModeChanged;
			host.NickNameChanged += host_NickNameChanged;
			host.AbsenceMessageChanged += host_AbsenceMessageChanged;

			this.Text = Core.HostInfoManager.GetHostDisyplayName(host);
		}

		/// <summary>
		/// 提交消息
		/// </summary>
		/// <param name="msg"></param>
		public void DropMessage(Alias.Message msg)
		{
			if (browser.ReadyState == WebBrowserReadyState.Complete)
			{
				FormatMessage(msg);

				object[] param = new object[] {
				msg.PackageNo.ToString(),
				msg.IsEncrypt?1:0,
				DateTime.Now.ToString(),
				msg.NormalMsg,
				msg.IsSecret?1:0,
				0,
				0,
				msg.IsAutoSendMessage?1:0,
				msg.AutoReplyTime==null?"":msg.AutoReplyTime.Value.ToString()
			};
				browser.Document.InvokeScript("messageReceied", param);
			}
			else
			{
				notReadyScriptList.Add(msg);
			}
		}

		/// <summary>
		/// 添加文件到发送列表
		/// </summary>
		/// <param name="isDirectory"></param>
		/// <param name="path"></param>
		/// <param name="size"></param>
		public void AddFileToSendList(bool isDirectory, string path, ulong size)
		{
			if (browser.IsReady())
			{
				browser.Document.InvokeScript("fileSendingPre", new object[] { isDirectory, path, size.ToSizeDescription() });
			}
			else
			{
				notReadyScriptList.Add(new object[] { isDirectory, path, size });
			}
		}


		public void SendMessage(string msg)
		{
			msg = System.Web.HttpUtility.HtmlEncode(msg).Replace("\n", "<br />");

			if (browser.ReadyState == WebBrowserReadyState.Complete)
			{
				object[] param = new object[] {
				Env.ClientConfig.IPMClientConfig.NickName,
				DateTime.Now.ToString(),
				msg
			};
				browser.Document.InvokeScript("messageSend", param);
			}
			else
			{
				notReadyScriptList.Add(msg);
			}
		}

		/// <summary>
		/// 反绑定主机时候调用
		/// </summary>
		public void RemoveHost()
		{
			Host.AbsenceModeChanged -= host_AbsenceModeChanged;
			Host.NickNameChanged -= host_NickNameChanged;
			Host.AbsenceMessageChanged -= host_AbsenceMessageChanged;
			browser.Dispose();
		}

		/// <summary>
		/// 添加文件到发送列表
		/// </summary>
		/// <param name="path"></param>
		public void AddFileToSendList(string path)
		{
			if (FileSendList.Contains(path)) return;
			if (System.IO.File.Exists(path))
			{
				FileSendList.Add(path);
				System.IO.FileInfo fi = new System.IO.FileInfo(path);
				AddFileToSendList(false, System.IO.Path.GetFileName(path), (ulong)fi.Length);
			}
			else if (System.IO.Directory.Exists(path))
			{
				FileSendList.Add(path);
				AddFileToSendList(true, System.IO.Path.GetFileName(path), 0ul);
			}

		}


		/// <summary>
		/// 格式化消息文本
		/// </summary>
		/// <param name="msg"></param>
		public void FormatMessage(Alias.Message msg)
		{
			if (FSLib.IPMessager.Define.Consts.Check(msg.Options, FSLib.IPMessager.Define.Consts.Cmd_Send_Option.Content_Html))
			{
				//HTML文本
			}
			else if (FSLib.IPMessager.Define.Consts.Check(msg.Options, FSLib.IPMessager.Define.Consts.Cmd_Send_Option.Content_Html))
			{
				//RTF文本,貌似无法显示吧..咋办?
			}
			else
			{
				//文本,格式化it
				msg.NormalMsg = System.Web.HttpUtility.HtmlEncode(msg.NormalMsg).Replace("\n", "<br />");
			}
		}

		#endregion

		#region 函数重载



		#endregion

		#region 属性
		/// <summary>
		/// 关联的主机
		/// </summary>
		public Host Host { get; private set; }


		#endregion

		#region 浏览器交互

		void host_AbsenceMessageChanged(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(Host.AbsenceMessage)) return;
			browser.Document.InvokeScript("absenceModeChange", new object[] { Host.IsInAbsenceMode ? 1 : 0, Host.AbsenceMessage });
		}

		void host_NickNameChanged(object sender, EventArgs e)
		{
			browser.Document.InvokeScript("nickNameChange", new object[] { Host.NickName, Host.GroupName });
		}

		void host_AbsenceModeChanged(object sender, EventArgs e)
		{
			browser.Document.InvokeScript("absenceModeChange", new object[] { Host.IsInAbsenceMode ? 1 : 0, Host.AbsenceMessage });
		}

		/// <summary>
		/// 初始化浏览器
		/// </summary>
		private void InitializeWebBrowser()
		{
			browser = new WebBrowserEx() { Dock = DockStyle.Fill };
			this.Controls.Add(browser);
			BindBrowserContextMenu();

			bool isBrowserInited = false;
			browser.DocumentCompleted += (s, e) =>
			{
				if (!isBrowserInited && browser.ReadyState == WebBrowserReadyState.Complete)
				{
					isBrowserInited = true;

					foreach (object item in notReadyScriptList)
					{
						if (item is string) SendMessage(item as string);
						else if (item is Alias.Message) DropMessage(item as Alias.Message);
						else if (item is object[])
						{
							object[] arr = item as object[];
							AddFileToSendList((bool)arr[0], arr[1] as string, (ulong)arr[2]);
						}
						else if (item is KeyValuePair<int, FileTaskEventArgs>)
						{
							KeyValuePair<int, FileTaskEventArgs> ea = (KeyValuePair<int, FileTaskEventArgs>)item;
							switch (ea.Key)
							{
								case 0: AddSendTask(ea.Value); break;		//0-添加发送任务
								case 1: AddReceiveTask(ea.Value); break;	//1-添加接收任务
								case 2: TaskItemProgressChange(ea.Value); break;		//2-任务进度变化
								case 3: TaskItemStateChange(ea.Value); break;			//3-任务状态变化
								case 4: SendTaskFinish(ea.Value); break;		//4-发送任务完成
								case 5: SendTaskStart(ea.Value); break;			//5-发送任务开始
								case 6: SendTaskExpires(ea.Value); break;		//6-发送任务过期
								case 7: ReceiveTaskFinish(ea.Value); break;		//7-接收任务完成
								case 8: ReceiveTaskStart(ea.Value); break;		//8-接收任务开始
							}
						}
						else if (item is FileSystemOperationErrorEventArgs) FileOperationError(item as FileSystemOperationErrorEventArgs);
						else if (item is FileTaskInfo) ReceiveFileRequired(item as FileTaskInfo);
					}
				}
			};
			browser.DragFile += (s, e) => Array.ForEach(e.Files, AddFileToSendList);

			//初始化浏览器
			browser.Navigate(String.Format(@"{0}Chat\IEView\main.html", Core.ProfileManager.GetThemeFolder()));
			browser.ObjectForScripting = this;

			//浏览器拖放文件
			//browser.AllowNavigation = false;	//禁止浏览
			browser.WebBrowserShortcutsEnabled = false;	//禁止快捷键
			browser.EnableFileDrag = true;

			//browser.ScriptErrorsSuppressed = true;	//禁止错误？
		}


		void BindBrowserContextMenu()
		{
			ContextMenuStrip ctx = new ContextMenuStrip();
			browser.IsWebBrowserContextMenuEnabled = false;
			browser.ContextMenuStrip = ctx;

			//-
			ToolStripMenuItem menuCopy = new ToolStripMenuItem("复制(&C)");
			menuCopy.Click += menuCopy_Click;

			ToolStripMenuItem menuClear = new ToolStripMenuItem("清空(&R)");
			menuClear.Click += menuClear_Click;

			ToolStripMenuItem menuConfig = new ToolStripMenuItem("设置(&S)...");
			menuConfig.Click += menuConfig_Click;

			ctx.Items.AddRange(new ToolStripMenuItem[] { menuCopy, menuClear, menuConfig });
		}

		//配置
		void menuConfig_Click(object sender, EventArgs e)
		{

		}

		//复制浏览器中选择的文字
		void menuClear_Click(object sender, EventArgs e)
		{
			browser.InvokeScript("clearChatHistory");
			//清理发送的列表
			FileSendList.Clear();
		}

		//复制浏览器中选择的文字
		void menuCopy_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 获得主机信息
		/// </summary>
		/// <returns></returns>
		public string GetHostInfo()
		{
			return string.Format("{{username:'{0}',ip:'{1}',group:'{2}',isabsence:{3},statemsg:'{4}',enc:{5}}}", Utility.Helper.ConvertJsString(Host.NickName), Host.HostSub.Ipv4Address.ToString(), Utility.Helper.ConvertJsString(Host.GroupName), Host.IsInAbsenceMode ? 1 : 0, Utility.Helper.ConvertJsString(Host.AbsenceMessage), Host.SupportEncrypt ? 1 : 0);
		}

		/// <summary>
		/// 发送确认打开消息
		/// </summary>
		/// <param name="pid"></param>
		public void SendOpenSignal(string pid)
		{
			Env.IPMClient.Commander.SendMessageOpenedSignal(Host, ulong.Parse(pid));
		}

		/// <summary>
		/// 转到地址
		/// </summary>
		/// <param name="url"></param>
		public void GoUrl(string url)
		{
			try
			{
				System.Diagnostics.Process.Start(url);
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// 清空发送队列
		/// </summary>
		public void ClearFileSendQueue()
		{
			FileSendList.Clear();
		}

		/// <summary>
		/// 发送文件
		/// </summary>
		public void SubmitFileSend()
		{
			OnFileSendRequest();
		}

		//设置是否保存在同目录
		public void SelectSetSaveFolder(bool sameLocation)
		{
			Env.ClientConfig.FunctionConfig.Share_UseSameLocationToSave = sameLocation;
		}

		SaveFileDialog _saveFileDialog;
		FolderBrowserDialog _folderBrowserDialog;

		//选择保存文件的路径
		public string SelectSavePath(string title, string currentPath)
		{
			if (_saveFileDialog == null) _saveFileDialog = new SaveFileDialog();
			if (!string.IsNullOrEmpty(currentPath) && System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(currentPath))) _saveFileDialog.FileName = currentPath;

			if (_saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				Env.ClientConfig.FunctionConfig.Share_LastSelectedPath = System.IO.Path.GetDirectoryName(_saveFileDialog.FileName);
				return _saveFileDialog.FileName;
			}
			else return "";
		}

		//选择保存文件夹的地方
		public string SelectSaveFolder(string description, string currentPath, string pkgid, string idx)
		{
			if (_folderBrowserDialog == null) _folderBrowserDialog = new FolderBrowserDialog();
			_folderBrowserDialog.Description = description;
			if (!string.IsNullOrEmpty(currentPath) && System.IO.Directory.Exists(currentPath)) _folderBrowserDialog.SelectedPath = currentPath;

			if (_folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				Env.ClientConfig.FunctionConfig.Share_LastSelectedPath = _folderBrowserDialog.SelectedPath;

				//
				ulong pid = ulong.Parse(pkgid);
				int index = int.Parse(idx);
				FileTaskInfo task = PaddingReceiveTask.SingleOrDefault(m => m.PackageID == pid);
				if (task == null || task.TaskList.Count <= index || index < 0) return "";
				task.TaskList[index].FullPath = _folderBrowserDialog.SelectedPath;
				if (Env.ClientConfig.FunctionConfig.Share_UseSameLocationToSave) task.TaskList.ForEach(s => s.FullPath = _folderBrowserDialog.SelectedPath);


				return _folderBrowserDialog.SelectedPath;
			}
			else return "";
		}

		#endregion





		#region 传输任务相关接口实现


		public void ClearFileSendList()
		{
			browser.Document.InvokeScript("clearFileSendPreDiv");
		}

		public event EventHandler FileSendRequest;


		protected virtual void OnFileSendRequest()
		{
			if (FileSendRequest != null) FileSendRequest(this, new EventArgs());
		}

		public List<string> FileSendList { get; private set; }

		public void TaskItemStateChange(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(3, e));
				return;
			}

			if (e.Direction == FileTransferDirection.Send)
			{
				browser.InvokeScript("fileSendingStateChange", e.TaskItem.TaskInfo.PackageID.ToString(), e.TaskItem.Index, e.TaskItem.CurrentName, e.TaskItem.Name, (int)e.TaskItem.State);
			}
			else
			{
				browser.InvokeScript("fileRecvStateChange", e.TaskItem.TaskInfo.PackageID.ToString(), e.TaskItem.Index, e.TaskItem.CurrentName, e.TaskItem.Name, (int)e.TaskItem.State);
			}
		}

		public void TaskItemProgressChange(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(2, e));
				return;
			}

			if (e.Direction == FileTransferDirection.Send)
			{
				browser.InvokeScript("fileSendingProgressChange", e.TaskItem.ToJsonInfoWithProgress());
			}
			else
			{
				browser.InvokeScript("fileRecvProgressChange", e.TaskItem.ToJsonInfoWithProgress());
			}
		}

		public void AddSendTask(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(0, e));
				return;
			}

			browser.Document.InvokeScript("fileSendingRequest", new object[] { e.TaskInfo.ToJsonInfo() });
		}

		public void AddReceiveTask(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(1, e));
				return;
			}

			browser.Document.InvokeScript("fileRecvRequest", new object[] { e.TaskInfo.ToJsonInfo() });
		}

		public void SendTaskExpires(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(6, e));
				return;
			}

			browser.InvokeScript("fileSendingExpired", e.PackageID.ToString(), e.TaskInfo.CreateTime.ToString());
		}

		public void SendTaskStart(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(5, e));
				return;
			}

			browser.InvokeScript("fileSendingStateChange", e.TaskItem.TaskInfo.PackageID.ToString(), e.TaskItem.Index, e.TaskItem.CurrentName, 1);
		}

		public void SendTaskFinish(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(4, e));
				return;
			}

			browser.InvokeScript("fileSendingTaskFinished", e.TaskInfo.PackageID.ToString());
		}

		public void ReceiveTaskFinish(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(7, e));
				return;
			}
			browser.InvokeScript("fileRecvTaskFinished", e.TaskInfo.PackageID.ToString());
		}

		public void ReceiveTaskStart(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(new KeyValuePair<int, FileTaskEventArgs>(8, e));
				return;
			}
			browser.InvokeScript("fileRecvStateChange", e.TaskItem.TaskInfo.PackageID.ToString(), e.TaskItem.Index, e.TaskItem.CurrentName, 1);
		}

		public void FileOperationError(FileSystemOperationErrorEventArgs e)
		{
			if (!browser.IsReady())
			{
				notReadyScriptList.Add(e);
				return;
			}

			browser.InvokeScript("fileOpError", (int)e.OperationType, e.FullPath);
		}



		public void SendTaskDiscard(FSLib.IPMessager.Core.FileTaskEventArgs e)
		{
			System.Text.StringBuilder sb = new StringBuilder();
			e.TaskInfo.TaskList.ForEach(s => sb.Append(s.Name + "；"));
			browser.InvokeScript("fileSendingDiscard", e.PackageID.ToString(), sb.ToString());
		}


		public void ReceiveFileRequired(FileTaskInfo task)
		{
			if (task == null) return;

			if (!browser.IsReady())
			{
				notReadyScriptList.Add(task);
				return;
			}

			if (PaddingReceiveTask == null) PaddingReceiveTask = new List<FileTaskInfo>();
			if (PaddingReceiveTask.FindIndex(s => s.PackageID == task.PackageID) == -1) PaddingReceiveTask.Add(task);	//FIX:重试接收重复包编号错误

			browser.InvokeScript("receiveFileRequired", task.ToJsonInfo(), Env.ClientConfig.FunctionConfig.Share_UseSameLocationToSave);
		}

		public void CancelFileReceive(string taskid)
		{
			if (PaddingReceiveTask == null) return;

			ulong pkgid = ulong.Parse(taskid);

			FileTaskInfo task = PaddingReceiveTask.SingleOrDefault(m => m.PackageID == pkgid);
			if (task == null) return;

			PaddingReceiveTask.Remove(task);
			OnTaskDiscardRequired(new FileTaskEventArgs(task));
		}

		public void SubmitFileReceive(string taskid)
		{
			if (PaddingReceiveTask == null) return;

			ulong pkgid = ulong.Parse(taskid);

			FileTaskInfo task = PaddingReceiveTask.SingleOrDefault(m => m.PackageID == pkgid);
			if (task == null) return;

			PaddingReceiveTask.Remove(task);
			OnTaskAccepted(new FileTaskEventArgs(task));
		}

		public List<FileTaskInfo> PaddingReceiveTask { get; private set; }


		public event EventHandler<FileTaskEventArgs> TaskDiscardRequired;
		public event EventHandler<FileTaskEventArgs> TaskAccepted;

		#region OnTaskDiscardRequired
		/// <summary>
		/// Triggers the TaskDiscardRequired event.
		/// </summary>
		public virtual void OnTaskDiscardRequired(FileTaskEventArgs ea)
		{
			if (TaskDiscardRequired != null)
				TaskDiscardRequired(this, ea);
		}
		#endregion

		#region OnTaskAccepted
		/// <summary>
		/// Triggers the TaskAccepted event.
		/// </summary>
		public virtual void OnTaskAccepted(FileTaskEventArgs ea)
		{
			if (TaskAccepted != null)
				TaskAccepted(null/*this*/, ea);
		}
		#endregion
		#endregion
	}
}
