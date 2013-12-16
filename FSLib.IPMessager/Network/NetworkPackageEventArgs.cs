using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using FSLib.IPMessager.Entity;

namespace FSLib.IPMessager.Network
{
	/// <summary>
	/// 消息包信息
	/// </summary>
	public class NetworkPackageEventArgs : EventArgs
	{

		/// <summary>
		/// 构造函数
		/// </summary>
		public NetworkPackageEventArgs()
		{
			IPEndPoint = null;
		}
		/// <summary>
		/// 远程主机的IP端口
		/// </summary>
		public IPEndPoint IPEndPoint { get; set; }

		/// <summary>
		/// 收到或发送的数据
		/// </summary>
		public byte[] Data { get; set; }

	}
}
