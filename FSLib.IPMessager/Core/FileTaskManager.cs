using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FSLib.IPMessager.Entity;
using System.Threading;
using System.Net;

namespace FSLib.IPMessager.Core
{
	/// <summary>
	/// 管理文件传输任务
	/// </summary>
	public class FileTaskManager : IDisposable
	{
		/// <summary>
		/// 创建 FileTaskManager class 的新实例
		/// </summary>
		internal FileTaskManager(Config cfg)
		{
			this.Config = cfg;

			InitSendManager();
			InitReceiveManager();
			InitTaskExpiresDetector();
		}

		#region 发送任务

		#region 属性

		/// <summary>
		/// 发送列表
		/// </summary>
		public Dictionary<ulong, FileTaskInfo> SendTask { get; set; }


		#endregion

		#region 私有变量

		/// <summary>
		/// 活动的文件发送任务
		/// </summary>
		List<FileTaskItem> _activeSendItems;

		/// <summary>
		/// 发送状态更新计时器
		/// </summary>
		System.Timers.Timer _sendStateUpdateTimer;

		#endregion

		#region 事件

		/// <summary>
		/// 发送任务已添加
		/// </summary>
		public event EventHandler<FileTaskEventArgs> SendTaskAdded;
		SendOrPostCallback _spcSendTaskAdded;

		/// <summary>
		/// 触发发送任务已添加事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnSendTaskAdded(FileTaskEventArgs e)
		{
			if (SendTaskAdded == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				SendTaskAdded(this, e);
			}
			else
			{
				if (_spcSendTaskAdded == null) _spcSendTaskAdded = (s) => SendTaskAdded(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(_spcSendTaskAdded, e);
			}
		}

		/// <summary>
		/// 发送任务完成
		/// </summary>
		public event EventHandler<FileTaskEventArgs> SendTaskFinished;
		SendOrPostCallback spcSendTaskFinished;

		/// <summary>
		/// 触发发送任务完成事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnSendTaskFinished(FileTaskEventArgs e)
		{
			if (SendTaskFinished == null) return;

			if (!IPMClient.NeedPostMessage) SendTaskFinished(this, e);
			else
			{
				if (spcSendTaskFinished == null) spcSendTaskFinished = (s) => SendTaskFinished(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(spcSendTaskFinished, e);
			}
		}

		/// <summary>
		/// 开始发送任务
		/// </summary>
		public event EventHandler<FileTaskEventArgs> SendItemStart;
		SendOrPostCallback spcSendItemStart;

		/// <summary>
		/// 触发发送任务开始事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnSendItemStart(FileTaskEventArgs e)
		{
			if (SendItemStart == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				SendItemStart(this, e);
			}
			else
			{
				if (spcSendItemStart == null) spcSendItemStart = (s) => SendItemStart(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(spcSendItemStart, e);
			}
		}

		/// <summary>
		/// 传输状态变化
		/// </summary>
		public event EventHandler<FileTaskEventArgs> TaskItemStateChanged;
		SendOrPostCallback spcTaskItemStateChanged;

		/// <summary>
		/// 触发发送状态变化事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnTaskItemStateChanged(FileTaskEventArgs e)
		{
			if (TaskItemStateChanged == null) return;

			if (!IPMClient.NeedPostMessage) TaskItemStateChanged(this, e);
			else
			{
				if (spcTaskItemStateChanged == null) spcTaskItemStateChanged = (s) => TaskItemStateChanged(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(spcTaskItemStateChanged, e);
			}
		}

		/// <summary>
		/// 传输进度变化
		/// </summary>
		public event EventHandler<FileTaskEventArgs> TaskItemProgressChanged;
		SendOrPostCallback spcTaskItemProgressChanged;

		/// <summary>
		/// 触发发送进度变化事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnTaskItemProgressChanged(FileTaskEventArgs e)
		{
			if (TaskItemStateChanged == null) return;

			if (!IPMClient.NeedPostMessage) TaskItemProgressChanged(this, e);
			else
			{
				if (spcTaskItemProgressChanged == null) spcTaskItemProgressChanged = (s) => TaskItemProgressChanged(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(spcTaskItemProgressChanged, e);
			}
		}

		#endregion

		#region 公共方法

		/// <summary>
		/// 添加文件传输任务
		/// </summary>
		/// <param name="task">任务信息</param>
		public void AddSendTask(FileTaskInfo task)
		{
			if (task == null) throw new ArgumentNullException();
			if (task.RemoteHost == null || task.TaskList == null || task.TaskList.Count == 0 || task.PackageID == 0) throw new ArgumentException();

			if (SendTask.ContainsKey(task.PackageID)) return;

			//进行必要的初始化
			task.Direction = FileTransferDirection.Send;
			task.TaskList.ForEach(s => s.TaskInfo = task);	//设置集合关系

			SendTask.Add(task.PackageID, task);
			OnSendTaskAdded(new FileTaskEventArgs(task));
		}

		/// <summary>
		/// 查询任务信息
		/// </summary>
		/// <param name="packageId"></param>
		/// <param name="taskIndex"></param>
		/// <returns></returns>
		public FileTaskItem QuerySendTask(ulong packageId, int taskIndex, IPAddress addr)
		{
			if (!SendTask.ContainsKey(packageId) || taskIndex < 0)
				return null;

			lock (SendTask)
			{
				FileTaskInfo fti = SendTask[packageId];
				if (taskIndex >= fti.TaskList.Count || addr.ToString() != fti.RemoteHost.HostSub.Ipv4Address.Address.ToString()) return null;
				else
				{
					FileTaskItem fi = fti.TaskList[taskIndex];
					OnSendItemStart(new FileTaskEventArgs(fti, fi));

					return fi;
				}
			}
		}

		/// <summary>
		/// 标记一个任务项目状态，并检查任务是否完成
		/// </summary>
		/// <param name="item">任务条目</param>
		/// <param name="state">状态</param>
		internal void MarkSendTaskItemState(FileTaskItem item, FileTaskItemState state)
		{
			if (item.State == state) return;

			if (state == FileTaskItemState.Finished || state == FileTaskItemState.Failure)
			{
				//退出传输状态
				_activeSendItems.Remove(item);
				//强制更新状态
				OnTaskItemProgressChanged(new FileTaskEventArgs(item.TaskInfo, item));
				EnsureSendStateUpdateTimerState();
			}
			item.State = state;
			OnTaskItemStateChanged(new FileTaskEventArgs(item.TaskInfo, item));

			//是否需要更新状态
			if (state == FileTaskItemState.Processing)
			{
				_activeSendItems.Add(item);
				EnsureSendStateUpdateTimerState();
			}

			//如果是完成了，那就查找下这个任务是不是全部完成了，如果是的话，那就删掉任务
			if (state == FileTaskItemState.Finished && item.TaskInfo.TaskList.Count(s => s.State != FileTaskItemState.Finished) == 0)
			{
				OnSendTaskFinished(new FileTaskEventArgs(item.TaskInfo));
				//SendTask.Remove(item.TaskInfo.PackageID);
			}
		}

		#endregion

		#region 私有函数

		/// <summary>
		/// 初始化发送
		/// </summary>
		void InitSendManager()
		{
			SendTask = new Dictionary<ulong, FileTaskInfo>();
			_activeSendItems = new List<FileTaskItem>();

			_sendStateUpdateTimer = new System.Timers.Timer(1000)
			{
				AutoReset = true,
				Enabled = false
			};
			_sendStateUpdateTimer.Elapsed += (s, e) => { RaiseSendStatusUpdateEvent(); };
		}

		/// <summary>
		/// 触发发送状态更新事件
		/// </summary>
		void RaiseSendStatusUpdateEvent()
		{
			FileTaskEventArgs e = new FileTaskEventArgs(null);
			lock (_activeSendItems)
			{
				_activeSendItems.ForEach(s =>
				{
					e.TaskInfo = s.TaskInfo;
					e.TaskItem = s;
					OnTaskItemProgressChanged(e);
				});
			}
		}

		/// <summary>
		/// 确认发送定时器是否可用
		/// </summary>
		void EnsureSendStateUpdateTimerState()
		{
			_sendStateUpdateTimer.Enabled = _activeSendItems.Count > 0;
		}

		#endregion


		#endregion

		#region 接收任务

		#region 属性

		/// <summary>
		/// 接收列表
		/// </summary>
		/// <value>这个队列维护的是正在执行或等待执行的文件接收列表，已经完成的请求将会自动从列表中移除</value>
		public List<FileTaskInfo> ReceiveTask { get; set; }

		/// <summary>
		/// 配置类
		/// </summary>
		public Config Config
		{
			get;
			private set;
		}


		#endregion

		#region 私有变量


		/// <summary>
		/// 活动的文件发送任务
		/// </summary>
		List<FileTaskItem> activeReceiveItems;

		/// <summary>
		/// 发送状态更新计时器
		/// </summary>
		System.Timers.Timer receiveStateUpdateTimer;

		#endregion

		#region 事件

		/// <summary>
		/// 接收任务已添加
		/// </summary>
		public event EventHandler<FileTaskEventArgs> ReceiveTaskAdded;
		SendOrPostCallback spcReceiveTaskAdded;

		/// <summary>
		/// 触发接收任务已添加事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnReceiveTaskAdded(FileTaskEventArgs e)
		{
			if (ReceiveTaskAdded == null) return;

			if (!IPMClient.NeedPostMessage)
			{
				ReceiveTaskAdded(this, e);
			}
			else
			{
				if (spcReceiveTaskAdded == null) spcReceiveTaskAdded = (s) => ReceiveTaskAdded(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(spcReceiveTaskAdded, e);
			}
		}

		/// <summary>
		/// 接收任务完成
		/// </summary>
		public event EventHandler<FileTaskEventArgs> ReceiveTaskFinished;
		SendOrPostCallback spcReceiveTaskFinished;

		/// <summary>
		/// 触发接收任务完成事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnReceiveTaskFinished(FileTaskEventArgs e)
		{
			if (ReceiveTaskFinished == null) return;

			if (!IPMClient.NeedPostMessage) ReceiveTaskFinished(this, e);
			else
			{
				if (spcReceiveTaskFinished == null) spcReceiveTaskFinished = (s) => ReceiveTaskFinished(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(spcReceiveTaskFinished, e);
			}
		}

		/// <summary>
		/// 开始接收任务
		/// </summary>
		public event EventHandler<FileTaskEventArgs> ReceiveItemStart;
		SendOrPostCallback spcReceiveItemStart;

		/// <summary>
		/// 触发接收任务开始事件
		/// </summary>
		/// <param name="e">事件数据</param>
		protected virtual void OnReceiveItemStart(FileTaskEventArgs e)
		{
			if (ReceiveItemStart == null) return;

			if (!IPMClient.NeedPostMessage) ReceiveItemStart(this, e);
			else
			{
				if (spcReceiveItemStart == null) spcReceiveItemStart = (s) => ReceiveItemStart(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(spcReceiveItemStart, e);
			}
		}

		/// <summary>
		/// 请求接收文件
		/// </summary>
		public event EventHandler<FileReceiveRequiredEventArgs> FileReceiveRequired;

		/// <summary>
		/// 正在请求接收文件
		/// </summary>
		public event EventHandler<FileReceiveRequiredEventArgs> FileReceiveRequiring;

		/// <summary>
		/// 触发请求接收文件事件
		/// </summary>
		/// <param name="e">事件参数</param>
		void OnFileReceiveRequired(FileReceiveRequiredEventArgs e)
		{
			if (FileReceiveRequired != null) FileReceiveRequired(this, e);
		}

		/// <summary>
		/// 引发 <seealso cref="FSLib.IPMessager.Core.FileTaskManager.FileReceiveRequiring"/> 事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFileReceiveRequiring(FileReceiveRequiredEventArgs e)
		{
			if (FileReceiveRequiring != null) FileReceiveRequiring(this, e);
		}


		/// <summary>
		/// 接收失败的文件请求，请求重新接收
		/// <para>当上次文件接收的时候出现了异常，将会引发此请求，请求的任务列表包含了尚未成功接收的文件信息请求重新接收。</para>
		/// <para>如果已经处理，请将 <see cref="FSLib.IPMessager.Core.FileTaskManager.FileTaskEventArgs" /> 的 IsHandled 属性设为 true，否则将会引发<seealso cref="FSLib.IPMessager.Core.FileTaskManager.FileReceiveTaskReDroped"/> 事件。</para>
		/// </summary>
		public event EventHandler<FileTaskEventArgs> FileReceiveTaskReDroped;
		SendOrPostCallback callFileReceiveTaskReDroped;

		/// <summary>
		/// 引发 <seealso cref="FSLib.IPMessager.Core.FileTaskManager.FileReceiveTaskReDroped"/> 事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFileReceiveTaskReDroped(FileTaskEventArgs e)
		{
			if (FileReceiveTaskReDroped == null) return;

			if (!IPMClient.NeedPostMessage) FileReceiveTaskReDroped(this, e);
			else
			{
				if (callFileReceiveTaskReDroped == null) callFileReceiveTaskReDroped = s => FileReceiveTaskReDroped(this, s as FileTaskEventArgs);

				IPMClient.SendSynchronizeMessage(callFileReceiveTaskReDroped, e);
			}
		}


		/// <summary>
		/// 接收失败的文件请求，请求重新接收
		/// <para>当上次文件接收中出现异常而导致引发的 <seealso cref="FSLib.IPMessager.Core.FileTaskManager.FileReceiveTaskReDroped"/> 事件并没有被处理时，将会引发此事件，请求发送信息通知对方文件已经取消接收。</para>
		/// </summary>
		public event EventHandler<FileTaskEventArgs> FileReceiveTaskDiscarded;
		SendOrPostCallback callFileReceiveTaskDiscarded;

		/// <summary>
		/// 引发 <seealso cref="FSLib.IPMessager.Core.FileTaskManager.FileReceiveTaskDiscarded"/> 事件
		/// <para>当上次文件接收中出现异常而导致引发的 <seealso cref="FSLib.IPMessager.Core.FileTaskManager.FileReceiveTaskReDroped"/> 事件并没有被处理时，将会引发此事件，请求发送信息通知对方文件已经取消接收。</para>
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnFileReceiveTaskDiscarded(FileTaskEventArgs e)
		{
			if (FileReceiveTaskDiscarded == null) return;

			if (IPMClient.NeedPostMessage)
			{
				if (callFileReceiveTaskDiscarded == null) callFileReceiveTaskDiscarded = s => FileReceiveTaskDiscarded(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(callFileReceiveTaskDiscarded, e);
			}
			else
				FileReceiveTaskDiscarded(this, e);
		}

		/// <summary>
		/// 当文件请求收到时触发
		/// </summary>
		public event EventHandler<FileReceivedEventArgs> FileReceived;

		/// <summary>
		/// 引发 <see cref="FileReceived" /> 事件
		/// </summary>
		protected internal virtual void OnFileReceived(FileReceivedEventArgs ea)
		{
			if (FileReceived != null)
				FileReceived(this, ea);
		}


		#endregion

		#region 公共方法

		/// <summary>
		/// 添加文件传输任务
		/// </summary>
		/// <param name="task">任务信息</param>
		public void AddReceiveTask(FileTaskInfo task)
		{
			if (task == null) throw new ArgumentNullException();
			if (task.RemoteHost == null || task.TaskList == null || task.TaskList.Count == 0 || task.PackageID == 0) throw new ArgumentException();

			//进行必要的初始化
			task.Direction = FileTransferDirection.Receive;
			task.TaskList.ForEach(s => s.TaskInfo = task);	//设置集合关系

			if (this.ReceiveTask.Contains(task)) return;

			this.ReceiveTask.Add(task);
			this.OnReceiveTaskAdded(new FileTaskEventArgs(task));
			StartReceive();
		}

		/// <summary>
		/// 取消接收任务
		/// </summary>
		/// <param name="packageId">包编号</param>
		public void CancelReceiveTask(ulong packageId)
		{
			CancelReceiveTask(packageId, -1);
		}

		/// <summary>
		/// 取消接收任务
		/// </summary>
		/// <param name="packageId">包编号</param>
		/// <param name="index">索引</param>
		public void CancelReceiveTask(ulong packageId, int index)
		{
			FileTaskInfo task = ReceiveTask.Find(s => s.PackageID == packageId);
			if (task == null || task.CancelPadding) return;

			Action<FileTaskItem> itemMarker = s =>
			{
				if (s.State == FileTaskItemState.Canceled || s.State == FileTaskItemState.Failure || s.State == FileTaskItemState.Finished) return;
				MarkReceiveTaskItemState(s, FileTaskItemState.Canceling);
			};

			if (index == -1)
			{
				//全部取消
				activeReceiveItems.Where(s => s.TaskInfo == task).ToList().ForEach(itemMarker);
				task.TaskList.ForEach(itemMarker);
			}
			else if (index < task.TaskList.Count)
			{
				itemMarker(task.TaskList[index]);
			}
			task.CancelPadding = true;
		}

		/// <summary>
		/// 标记一个任务项目状态，并检查任务是否完成
		/// </summary>
		/// <param name="item">任务条目</param>
		/// <param name="state">状态</param>
		internal void MarkReceiveTaskItemState(FileTaskItem item, FileTaskItemState state)
		{
			if (item.State == state) return;

			if (state == FileTaskItemState.Finished || state == FileTaskItemState.Failure)
			{
				//退出传输状态
				this.activeReceiveItems.Remove(item);
				OnTaskItemProgressChanged(new FileTaskEventArgs(item.TaskInfo, item));
				this.EnsureReceiveStateUpdateTimerState();
			}
			item.State = state;
			OnTaskItemStateChanged(new FileTaskEventArgs(item.TaskInfo, item));

			//是否需要更新状态
			if (state == FileTaskItemState.Processing)
			{
				this.activeReceiveItems.Add(item);
				this.EnsureReceiveStateUpdateTimerState();
			}

			//如果是完成了，那就查找下这个任务是不是全部完成了，如果是的话，那就删掉任务
			if ((state == FileTaskItemState.Finished || state == FileTaskItemState.Failure))
			{
				//检测任务完成
				if (item.TaskInfo.TaskList.Count(s => s.State != FileTaskItemState.Finished && s.State != FileTaskItemState.Failure) == 0)
				{
					FileTaskInfo task = item.TaskInfo;

					this.OnReceiveTaskFinished(new FileTaskEventArgs(task));
					this.ReceiveTask.Remove(task);

					//检测是否有失败的，如果有就将请求重新发送以便重试
					for (int i = task.TaskList.Count - 1; i >= 0; i--)
					{
						if (task.TaskList[i].State != FileTaskItemState.Failure) task.TaskList.RemoveAt(i);	//如果是成功的，则移除
						else
						{
							task.TaskList[i].State = FileTaskItemState.Scheduled;							//重置任务的当前状态
							if (!task.TaskList[i].IsFolder) task.TaskList[i].FullPath = System.IO.Path.GetDirectoryName(task.TaskList[i].FullPath);
						}
					}
					if (task.TaskList.Count > 0)
					{
						var e = new FileTaskEventArgs(task);
						OnFileReceiveTaskReDroped(e);
						if (!e.IsHandled) OnFileReceiveTaskDiscarded(e);
					}
				}

				StartReceive();
			}
		}

		/// <summary>
		/// 检测任务是否可以启动
		/// </summary>
		void StartReceive()
		{
			if ((Config.TasksMultiReceiveCount > 0 && activeReceiveItems.Count >= Config.TasksMultiReceiveCount) || ReceiveTask.Count == 0) return;
			//RunCount-Fix thread limit issue
			int runCount = 0;

			foreach (FileTaskInfo t in ReceiveTask)
			{
				for (int j = 0; j < t.TaskList.Count; j++)
				{
					if (t.TaskList[j].State == FileTaskItemState.Scheduled)
					{
						FileTaskInfo task = t;
						FileTaskItem item = task.TaskList[j];

						//开始传输

						FileReceiveRequiredEventArgs e = new FileReceiveRequiredEventArgs(task, item);
						OnFileReceiveRequiring(e);
						if (!e.IsHandled) OnFileReceiveRequired(e);
						runCount++;

						//标记状态
						MarkReceiveTaskItemState(item, FileTaskItemState.Initializing);
						if (activeReceiveItems.Count + runCount >= Config.TasksMultiReceiveCount && Config.TasksMultiReceiveCount > 0) return;	//超过数目则退出
					}
				}
			}
		}

		#endregion

		#region 私有函数

		/// <summary>
		/// 初始化接收
		/// </summary>
		void InitReceiveManager()
		{
			this.ReceiveTask = new List<FileTaskInfo>();
			this.activeReceiveItems = new List<FileTaskItem>();

			this.receiveStateUpdateTimer = new System.Timers.Timer(1000)
			{
				AutoReset = true,
				Enabled = false
			};
			receiveStateUpdateTimer.Elapsed += (s, e) => { RaiseReceiveStatusUpdateEvent(); };
		}


		/// <summary>
		/// 触发接收状态更新事件
		/// </summary>
		void RaiseReceiveStatusUpdateEvent()
		{
			FileTaskEventArgs e = new FileTaskEventArgs(null);

			activeReceiveItems.ForEach(s =>
			{
				e.TaskInfo = s.TaskInfo;
				e.TaskItem = s;
				OnTaskItemProgressChanged(e);
			});
		}

		/// <summary>
		/// 确认定时器是否可用
		/// </summary>
		void EnsureReceiveStateUpdateTimerState()
		{
			this.receiveStateUpdateTimer.Enabled = this.activeReceiveItems.Count > 0;
		}


		#endregion

		#endregion

		#region 任务超时管理/调度

		System.Timers.Timer _taskExpiresTimer;

		/// <summary>
		/// 超时委托
		/// </summary>
		/// <param name="sender">事件发送方</param>
		/// <param name="e">事件数据</param>
		public delegate void TaskExpiresEventHandler(object sender, FileTaskEventArgs e);

		/// <summary>
		/// 任务超时
		/// </summary>
		public event TaskExpiresEventHandler TaskExpires;
		SendOrPostCallback _spcTaskExpires;

		/// <summary>
		/// 引发任务超时事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnTaskExpires(FileTaskEventArgs e)
		{
			if (TaskExpires == null) return;

			if (!IPMClient.NeedPostMessage) TaskExpires(this, e);
			else
			{
				if (_spcTaskExpires == null) _spcTaskExpires = (f) => TaskExpires(this, f as FileTaskEventArgs);
				IPMClient.SendASynchronizeMessage(_spcTaskExpires, e);
			}
		}

		/// <summary>
		/// 初始化任务超时调度
		/// </summary>
		void InitTaskExpiresDetector()
		{
			_taskExpiresTimer = new System.Timers.Timer();
			_taskExpiresTimer.Interval = 500;
			_taskExpiresTimer.Elapsed += (s, e) => DetectExpiresTasks();
			_taskExpiresTimer.Enabled = true;
		}

		/// <summary>
		/// 检测超时的任务
		/// </summary>
		void DetectExpiresTasks()
		{
			DateTime expiresTime = DateTime.Now.AddSeconds(-Config.TaskKeepTime);
			List<ulong> expList = null;

			lock (SendTask)
			{
				foreach (var item in SendTask.Keys)
				{
					FileTaskInfo task = SendTask[item];
					if (task.CreateTime > expiresTime
						||
						task.TaskList.Count(s => s.State != FileTaskItemState.Scheduled) > 0
						) continue;

					//任务已经超时
					if (expList == null) expList = new List<ulong>();
					expList.Add(item);
					OnTaskExpires(new FileTaskEventArgs(task));
				}

				if (expList != null) expList.ForEach(ul => SendTask.Remove(ul));
			}
		}


		SendOrPostCallback cbReleaseFileReuired;

		/// <summary>
		/// 引发请求释放文件事件
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnReleaseFileRequired(FileTaskEventArgs e)
		{
			if (ReleaseFileRequired == null) return;

			if (IPMClient.NeedPostMessage)
			{
				ReleaseFileRequired(this, e);
			}
			else
			{
				if (cbReleaseFileReuired == null) cbReleaseFileReuired = s => ReleaseFileRequired(this, s as FileTaskEventArgs);
				IPMClient.SendSynchronizeMessage(cbReleaseFileReuired, e);
			}
		}

		/// <summary>
		/// 请求释放事件
		/// </summary>
		public event EventHandler<FileTaskEventArgs> ReleaseFileRequired;

		/// <summary>
		/// 释放发送的文件请求
		/// </summary>
		/// <param name="pkgid"></param>
		/// <returns></returns>
		internal FileTaskInfo ReleaseFile(ulong pkgid)
		{
			if (!SendTask.ContainsKey(pkgid)) return null;

			FileTaskInfo task = SendTask[pkgid];
			SendTask.Remove(pkgid);

			OnReleaseFileRequired(new FileTaskEventArgs(task));
			return task;
		}


		#endregion


		#region IDisposable 成员

		bool isDisposing;

		public void Dispose()
		{
			if (!isDisposing)
			{
				isDisposing = true;

				_sendStateUpdateTimer.Enabled = false;
				_sendStateUpdateTimer = null;

				receiveStateUpdateTimer.Enabled = false;
				receiveStateUpdateTimer = null;

				_taskExpiresTimer.Enabled = false;
				_taskExpiresTimer = null;
			}
		}

		#endregion
	}
}
