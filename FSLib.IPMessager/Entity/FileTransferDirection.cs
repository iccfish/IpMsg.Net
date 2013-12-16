using System;

namespace FSLib.IPMessager.Entity
{
	/// <summary>
	/// 文件传输的方向
	/// </summary>
	public enum FileTransferDirection : int
	{
		/// <summary>
		/// 发送文件
		/// </summary>
		Send,
		/// <summary>
		/// 接收文件
		/// </summary>
		Receive
	}
}
