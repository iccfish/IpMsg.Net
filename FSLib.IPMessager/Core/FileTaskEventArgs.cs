using System;
using System.Collections.Generic;
using FSLib.IPMessager.Entity;

namespace FSLib.IPMessager.Core
{
	/// <summary>
	/// 任务参数信息
	/// </summary>
	public class FileTaskEventArgs : EventArgs
	{
		/// <summary>
		/// 关联的任务信息
		/// </summary>
		public FileTaskInfo TaskInfo { get; set; }

		/// <summary>
		/// 关联的任务条目
		/// </summary>
		public FileTaskItem TaskItem { get; set; }

		/// <summary>
		/// 是否已经处理了
		/// </summary>
		public bool IsHandled { get; set; }

		/// <summary>
		/// 远程主机信息
		/// </summary>
		public Host Host
		{
			get
			{
				return TaskInfo == null ? null : TaskInfo.RemoteHost;
			}
		}

		/// <summary>
		/// 包ID
		/// </summary>
		public ulong PackageID
		{
			get
			{
				return TaskInfo == null ? 0ul : TaskInfo.PackageID;
			}
		}

		/// <summary>
		/// 文件传输的方向
		/// </summary>
		public FileTransferDirection Direction
		{
			get
			{
				if (TaskInfo == null) throw new InvalidOperationException();
				else return TaskInfo.Direction;
			}
		}

		/// <summary>
		/// 发送文件时有效，等待发送的文件列表
		/// </summary>
		public List<FileTaskItem> TaskList
		{
			get
			{
				return TaskInfo == null ? null : TaskInfo.TaskList;
			}
		}



		/// <summary>
		/// 创建 FileTaskEventArgs class 的新实例
		/// </summary>
		public FileTaskEventArgs(FileTaskInfo taskInfo, FileTaskItem taskItem)
		{
			TaskInfo = taskInfo;
			TaskItem = taskItem;
			IsHandled = false;
		}

		/// <summary>
		/// 创建 FileTaskEventArgs class 的新实例
		/// </summary>
		public FileTaskEventArgs(FileTaskInfo taskInfo)
		{
			TaskInfo = taskInfo;
			IsHandled = false;
		}
	}
}
