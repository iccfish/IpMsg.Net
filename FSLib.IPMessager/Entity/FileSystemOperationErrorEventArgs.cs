using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 文件系统操作异常类别
	/// </summary>
	public enum FileSystemOperationType : int
	{
		/// <summary>
		/// 打开文件发送
		/// </summary>
		OpenFileToSend = 0,
		/// <summary>
		/// 读取目录
		/// </summary>
		QueryDirectory = 1,
		/// <summary>
		/// 创建目录
		/// </summary>
		CreateDirectory = 2,
		/// <summary>
		/// 创建文件
		/// </summary>
		CreateFile = 3,
		/// <summary>
		/// 写入数据
		/// </summary>
		WriteData = 4
	}

	/// <summary>
	/// 文件系统操作事件数据
	/// </summary>
	public class FileSystemOperationErrorEventArgs : EventArgs
	{
		/// <summary>
		/// 操作类型
		/// </summary>
		public FileSystemOperationType OperationType { get; set; }

		/// <summary>
		/// 操作的路径
		/// </summary>
		public string FullPath { get; set; }

		/// <summary>
		/// 对应的主机
		/// </summary>
		public Host Host { get; set; }

		/// <summary>
		/// Initializes a new instance of the FileSystemOperationError class.
		/// </summary>
		public FileSystemOperationErrorEventArgs(FileSystemOperationType operationType, string fullPath,Host host)
		{
			OperationType = operationType;
			FullPath = fullPath;
			this.Host = host;
		}
	}
}
