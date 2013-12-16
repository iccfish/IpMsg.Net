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
	/// 请求接收文件事件-用文件管理器和TCP线程调度信息
	/// </summary>
	public class FileReceiveRequiredEventArgs : EventArgs
	{
		/// <summary>
		/// 任务信息
		/// </summary>
		public FileTaskInfo Task { get; set; }

		/// <summary>
		/// 任务项目
		/// </summary>
		public FileTaskItem Item { get; set; }

		/// <summary>
		/// 是否已经处理
		/// </summary>
		public bool IsHandled { get; set; }

		/// <summary>
		/// 创建一个新的 FileReceiveEventArgs 对象.
		/// </summary>
		public FileReceiveRequiredEventArgs(FileTaskInfo task, FileTaskItem item)
		{
			Task = task;
			Item = item;
			IsHandled = false;
		}
	}
}
