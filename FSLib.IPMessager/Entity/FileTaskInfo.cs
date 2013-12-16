using System;
using System.Collections.Generic;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 文件传输任务
	/// </summary>
	public class FileTaskInfo : IFileTaskInfo
	{
		/// <summary>
		/// 文件传输的方向
		/// </summary>
		public FileTransferDirection Direction { get; set; }

		/// <summary>
		/// 原始消息包的编号
		/// </summary>
		public ulong PackageID { get; set; }

		/// <summary>
		/// 发送文件时有效，等待发送的文件列表
		/// </summary>
		public List<FileTaskItem> TaskList { get; set; }

		/// <summary>
		/// 远程主机信息
		/// </summary>
		public Host RemoteHost { get; set; }

		/// <summary>
		/// 是否处于等待取消的状态
		/// </summary>
		public bool CancelPadding { get; set; }

		/// <summary>
		/// 任务创建的时间
		/// </summary>
		public DateTime CreateTime { get; set; }

		/// <summary>
		/// 是否是重新尝试接收的任务
		/// </summary>
		/// <value>
		/// 当任务接收失败后，接收管理器将会引发重新接收事件。自此，本属性将会设置为true
		/// </value>
		public bool IsRetry { get; set; }

		/// <summary>
		/// 创建一个新的 FileTaskInfo 对象.
		/// </summary>
		public FileTaskInfo(FileTransferDirection direction, ulong packageID, Host remoteHost)
		{
			Direction = direction;
			PackageID = packageID;
			RemoteHost = remoteHost;

			CreateTime = DateTime.Now;
			CancelPadding = false;

			TaskList = new List<FileTaskItem>();
		}
	}
}
