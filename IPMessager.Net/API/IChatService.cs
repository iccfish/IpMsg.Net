using System;
using System.Collections.Generic;

using FSLib.IPMessager.Entity;

namespace IPMessagerNet.API
{
	public interface IChatService
	{
		/// <summary>
		/// 绑定主机到TabPage
		/// </summary>
		/// <param name="host">主机对象</param>
		void BindHost(Host host);

		/// <summary>
		/// 删除主机
		/// </summary>
		void RemoveHost();

		/// <summary>
		/// 关联的主机
		/// </summary>
		Host Host { get; }

		/// <summary>
		/// 将未处理的信息提交
		/// </summary>
		/// <param name="msg"></param>
		void DropMessage(Message msg);

		/// <summary>
		/// 将未处理的信息提交
		/// </summary>
		/// <param name="msg"></param>
		void SendMessage(string msg);

		/// <summary>
		/// 添加文件到发送列表
		/// </summary>
		/// <param name="path"></param>
		void AddFileToSendList(string path);

		/// <summary>
		/// 清空文件发送列表
		/// </summary>
		void ClearFileSendList();

		/// <summary>
		/// 文件请求发送
		/// </summary>
		event EventHandler FileSendRequest;

		/// <summary>
		/// 文件请求发送的列表
		/// </summary>
		List<string> FileSendList { get; }

		#region 文件传输相关

		/// <summary>
		/// 请求接收文件
		/// </summary>
		/// <param name="task"></param>
		void ReceiveFileRequired(FileTaskInfo task);

		/// <summary>
		/// 添加传输任务
		/// </summary>
		/// <param name="e"></param>
		void AddSendTask(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 添加接收任务
		/// </summary>
		/// <param name="e"></param>
		void AddReceiveTask(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 发送任务超时
		/// </summary>
		/// <param name="e"></param>
		void SendTaskExpires(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 发送任务开始
		/// </summary>
		/// <param name="e"></param>
		void SendTaskStart(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 发送任务完成
		/// </summary>
		/// <param name="e"></param>
		void SendTaskFinish(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 接收任务完成
		/// </summary>
		/// <param name="e"></param>
		void ReceiveTaskFinish(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 接收任务开始
		/// </summary>
		/// <param name="e"></param>
		void ReceiveTaskStart(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 文件系统错误
		/// </summary>
		/// <param name="e"></param>
		void FileOperationError(FSLib.IPMessager.Entity.FileSystemOperationErrorEventArgs e);

		/// <summary>
		/// 传输任务状态变化
		/// </summary>
		/// <param name="task"></param>
		/// <param name="item"></param>
		void TaskItemStateChange(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 传输任务进度变化
		/// </summary>
		/// <param name="task"></param>
		/// <param name="item"></param>
		void TaskItemProgressChange(FSLib.IPMessager.Core.FileTaskEventArgs e);

		/// <summary>
		/// 发送被拒绝
		/// </summary>
		void SendTaskDiscard(FSLib.IPMessager.Core.FileTaskEventArgs e);


		/// <summary>
		/// 等待接收的任务
		/// </summary>
		List<FileTaskInfo> PaddingReceiveTask { get; }

		/// <summary>
		/// 请求释放文件
		/// </summary>
		event EventHandler<FSLib.IPMessager.Core.FileTaskEventArgs> TaskDiscardRequired;

		/// <summary>
		/// 接受发送文件
		/// </summary>
		event EventHandler<FSLib.IPMessager.Core.FileTaskEventArgs> TaskAccepted;

		#endregion
	}
}
